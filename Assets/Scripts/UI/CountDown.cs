using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
public class CountDown : MonoBehaviour
{
    [SerializeField] private TMP_Text countDownText;
    [SerializeField] private float countDownTime;
    [SerializeField] private Slider Slider;

    // Start is called before the first frame update
    public void Start()
    {
        if (countDownText == null)
        {
            Debug.LogError("Count Down Text not found");
        }
    }

    public void setTimer(float timer)
    {
        Slider.maxValue = timer;
        countDownTime = timer;
        StartCoroutine(startCountDown());
    }

    public IEnumerator startCountDown()
    {
        while (countDownTime > 0)
        {
            yield return new WaitForSeconds(1);
            countDownTime--;
            int minutes = Mathf.FloorToInt(countDownTime / 60);
            int seconds = Mathf.FloorToInt(countDownTime % 60);

            string remainingTime = string.Format("{0:00}:{1:00}", minutes, seconds);
            countDownText.text = remainingTime;
            Slider.value = countDownTime;
        }
    }
}
