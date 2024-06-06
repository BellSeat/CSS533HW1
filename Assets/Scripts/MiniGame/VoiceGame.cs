using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VoiceGame : MonoBehaviour
{
    private AudioClip microphoneClip;
    private string microphoneName;
    public int sampleWindow = 128; // Number of samples to analyze, adjust as needed
    public float loudnessMultiplier = 1.0f; // Scaling factor to adjust the loudness
    public RawImage balloon; // Reference to the RawImage representing the balloon
    public float maxBalloonSize = 10.0f; // Maximum size of the balloon
    public float balloonLeakRate = 0.005f; // Rate at which the balloon leaks air
    public float minBalloonSize = 1.0f; // Minimum size of the balloon before it stops leaking

    public TMP_Text gameDescription; // Reference to the game description text
    public TMP_Text scoreText; // Reference to the score text
    public Button submitButton, closeButton; // Reference to the submit button

    private Coroutine checkLoudnessCoroutine;
    private bool balloonPopped = false;
    GameObject resultPage;

    void Start()
    {
        resultPage = Resources.Load<GameObject>("Prefab/UI/ResultMenu") as GameObject;

        // Check if there is at least one microphone available
        if (Microphone.devices.Length > 0)
        {
            microphoneName = Microphone.devices[0]; // Use the first available microphone
            microphoneClip = Microphone.Start(microphoneName, true, 1, 44100); // Start recording from the microphone
        }
        else
        {
            Debug.LogError("No microphone detected!");
        }

        // Start the coroutine to check loudness at a fixed rate
        checkLoudnessCoroutine = StartCoroutine(CheckLoudness());

        // Add listener to the submit button
        submitButton.onClick.AddListener(OnSubmit);

        // Initially hide the score text
        scoreText.gameObject.SetActive(false);

        closeButton.onClick.AddListener(OnClose);
    }

    IEnumerator CheckLoudness()
    {
        while (true)
        {
            if (!balloonPopped && balloon.rectTransform.localScale.x >= maxBalloonSize)
            {
                PopBalloon();
            }

            if (Microphone.IsRecording(microphoneName))
            {
                float loudness = GetLoudnessFromMicrophone() * loudnessMultiplier;

                if (loudness > 1.0f)
                {
                    UpdateBalloonSize(2.0f);
                }
                else if (loudness > 0.5f)
                {
                    UpdateBalloonSize(1.0f);
                }
                else if (loudness > 0.2f)
                {
                    UpdateBalloonSize(0.4f);
                }
                else if (loudness > 0.1f)
                {
                    UpdateBalloonSize(0.2f);
                }
                else
                {
                    // Leak air when not blowing
                    LeakBalloon();
                }
            }

            yield return new WaitForSeconds(1.0f / 10); // Check input 10 times per second
        }
    }

    float GetLoudnessFromMicrophone()
    {
        // Get data from microphone
        float[] data = new float[sampleWindow];
        int position = Microphone.GetPosition(microphoneName) - sampleWindow + 1;
        if (position < 0)
        {
            return 0;
        }
        microphoneClip.GetData(data, position);

        // Compute loudness
        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += data[i] * data[i];
        }
        return Mathf.Sqrt(sum / sampleWindow);
    }

    void UpdateBalloonSize(float increaseAmount)
    {
        // Adjust the balloon size based on loudness
        float newSize = balloon.rectTransform.localScale.x + increaseAmount;
        balloon.rectTransform.localScale = new Vector3(newSize, newSize, 1);
    }

    void LeakBalloon()
    {
        // Decrease the balloon size when not blowing
        float currentSize = balloon.rectTransform.localScale.x;
        if (currentSize > minBalloonSize)
        {
            float newSize = Mathf.Clamp(currentSize - balloonLeakRate, minBalloonSize, maxBalloonSize);
            balloon.rectTransform.localScale = new Vector3(newSize, newSize, 1);
        }
    }

    void PopBalloon()
    {
        // Destroy the balloon when it reaches the maximum size
        Debug.Log("Balloon popped!");
        balloon.gameObject.SetActive(false);
        balloonPopped = true;
        OnSubmit();
    }

    void OnSubmit()
    {
        // Hide game description and balloon
        gameDescription.gameObject.SetActive(false);
        if (balloon != null)
        {
            balloon.gameObject.SetActive(false);
        }

        // Display the score text
        scoreText.gameObject.SetActive(true);
        if (balloonPopped || balloon == null)
        {
            scoreText.text = "Final score: 0";
        }
        else
        {
            float finalSize = balloon.rectTransform.localScale.x;
            scoreText.text = "Final score: " + finalSize.ToString("F2");
        }

        submitButton.gameObject.SetActive(false);
    }

    void OnClose()
    {
        if (!balloonPopped)
        {
            GameObject result = Instantiate(resultPage);
            result.transform.parent = GameObject.Find("Canvas").transform;
            result.transform.localPosition = Vector2.zero;
        }
        Destroy(this.gameObject);
    }
        
}
