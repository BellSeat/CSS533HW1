using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MultichoiceGame : MonoBehaviour
{
    [SerializeField] private bool[] Choice = new bool[4];
    [SerializeField] private bool[] isChoiceActive = new bool[4];
    [SerializeField] private int duration;
    [SerializeField] private int correctChoice, maxActiveAnswer;
    [SerializeField] private CountDown countDown;
    [SerializeField] private Button  closeButton;
    [SerializeField] private Button[] choicesButton = new Button[4];
    [SerializeField] private GameObject resultPage,popUpMessage;
    // Start is called before the first frame update
    void Start()
    {
        duration = 20;
        resultPage = Resources.Load<GameObject>("Prefab/UI/ResultMenu") as GameObject;
        popUpMessage = Resources.Load<GameObject>("Prefab/UI/popUpMessage") as GameObject;
        closeButton.onClick.AddListener(CloseButtonClicked);
        for (int i = 0; i < 4; ++i)
        {
            Debug.Log("Button " + i);
            Choice[i] = false;
            int buttonIndex = i;
            choicesButton[i].onClick.AddListener(delegate { 
               
                ChoiceButtonClicked(buttonIndex); 
            });
        }
        int randomAnswer = Random.Range(0, 4);
        setAnswer(randomAnswer);
        countDown.setTimer(duration);
        StartCoroutine(liveAndDie());
    }

    // Update is called once per frame

    // set the answer for the game
    void setAnswer(int answer)
    {
        if (answer < 0 || answer >= 4) { 
            // if answer is out of range, set answer to random
            correctChoice = Random.Range(0, 4);
        }
        correctChoice = answer;
        for (int i = 0; i < 4; i++)
        {
            if (i == correctChoice)
            {
                Choice[i] = true;
            }
            else
            {
                Choice[i] = false;
            }
        }
        Debug.Log("Correct Answer is " + correctChoice);
    }

    // it will not be use for current version
    void setMaxActiveAnswer(int maxActive)
    {
        maxActiveAnswer = maxActive;

    }

    // check if the answer is correct
    // if correct, return result page,
    // if wrong close the gameobject

    void checkAnswer(int answer)
    {
        Debug.Log("Index is " + answer);
        if (Choice[answer])
        {
            
            // show result page
            GameObject result = Instantiate(resultPage);
            result.transform.SetParent(GameObject.Find("Canvas").transform, false);
            result.transform.localPosition = Vector2.zero;
            Debug.Log("Correct Answer");
        }
        else
        {
             Debug.Log("Wrong Answer");
            // show pop up message
            GameObject message = Instantiate(popUpMessage);
            message.transform.SetParent(GameObject.Find("Canvas").transform, false);
            message.GetComponent<PopUpMessage>().setMessage("Wrong Answer");
            Debug.Log("Create popupMessage");
        }

        // close the game
        Destroy(this.gameObject);
    }

    // action for button clicked
    // close button clicked
    void CloseButtonClicked()
    {
        Debug.Log("Close Button Clicked");
        Destroy(this.gameObject);
    }

    // when the button click return the button index  to check answer
    void ChoiceButtonClicked(int index)
    {
        Debug.Log("Index when click is " + index);
        checkAnswer(index);
    }

    // set choice game live time

    IEnumerator liveAndDie()
    {

        yield return new WaitForSeconds(duration);

        // show pop up message
        GameObject message = Instantiate(popUpMessage);
        message.transform.SetParent(GameObject.Find("Canvas").transform, false);
        message.GetComponent<PopUpMessage>().setMessage("Time is up");

        yield return null;
        Destroy(this.gameObject);
    }

}
