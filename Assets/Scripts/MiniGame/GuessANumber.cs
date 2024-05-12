using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

public class GuessANumber : MonoBehaviour
{
    [SerializeField] private Button closeButton, submitButton;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TMP_Text GameTitle, GameDescription, GameResult;
    [SerializeField] private int ansewer;
    [SerializeField] private PlayerBehavior enemy;
    [SerializeField] private GameObject resultPage;
    [SerializeField] private CountDown countDown;

    // Start is called before the first frame update
    public void Start()
    {
        resultPage = Resources.Load<GameObject>("Prefab/UI/ResultMenu") as GameObject;

       
        //Assert.IsNotNull(resultPage, "Result Page not found");
        //Assert.IsNotNull(closeButton, "Close Button not found");
        //Assert.IsNotNull(submitButton, "Submit Button not found");
        //Assert.IsNotNull(inputField, "Input Field not found");
        //Assert.IsNotNull(GameTitle, "Game Title not found");
        //Assert.IsNotNull(GameDescription, "Game Description not found");
        //Assert.IsNotNull(GameResult, "Game Result not found");

        // set answer by enemy luck number
        // is player is null, set answer by random
        if (enemy == null)
        {
            ansewer = Random.Range(1, 101);
        }
        else
        {
            ansewer = enemy.getLuckyNumber();
        }
        Debug.Log("Answer is " + ansewer);
        setGameTitle("Guess A Number");
        setGameDescription("Guess a number between 1 and 100");

       
        
        closeButton.onClick.AddListener(CloseButtonClicked);
        
        submitButton.onClick.AddListener(SubmitButtonClicked);

        StartCoroutine(closeInSecond(20));
        
    }


    public void SubmitButtonClicked()
    {
        Debug.Log("Submit Button Clicked");
        int guess = int.Parse(inputField.text);
        ifTheAnswer(guess);
    }
    public void CloseButtonClicked()
    {
        Debug.Log("Close Button Clicked");
        Destroy(this.gameObject);
    }

    public void setEnemy(PlayerBehavior newEnemy) {
        if (newEnemy == null) {
            return;
        }
        else
        {
            enemy = newEnemy;
        }
    }

    public void setGameTitle(string title)
    {
        if (title != null && title.Length != 0 && title != "")
        {
            this.GameTitle.text = title;
            return;
        }
        this.GameTitle.text = "Guess A Number"; 
    }

    public void setGameDescription(string description)
    {
        if (description != null && description.Length != 0 && description != "")
        {
            this.GameDescription.text = description;
            return;
        }
        this.GameDescription.text = "Guess a number between 1 and 100";
    }

    public void setResult(int guess) {

        if (guess == ansewer)
        {
            GameResult.color = Color.green;
            GameResult.text = "You Win!";
            closeButton.onClick.RemoveAllListeners();
            submitButton.onClick.RemoveAllListeners();
            
        }
        else if (guess > ansewer) {
            GameResult.color = Color.red;
            GameResult.text = "Try Again! The answer is lower than " + guess;
            
        }
        else if (guess < ansewer)
        {
            GameResult.color = Color.red;
            GameResult.text = "Try Again! The answer is higher than " + guess;
        }
    }

    public void ifTheAnswer(int guess) { 
        setResult(guess);
        if (guess == ansewer) {
            StartCoroutine(showThenClose(0.5f));
        }
    }
    public IEnumerator closeInSecond(float seconds)
    {
        countDown.setTimer(seconds);
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
        
    }

    public IEnumerator showThenClose(float seconds) { 
        
        yield return new WaitForSeconds(seconds);
        Debug.Log("Attack Button Clicked");
        GameObject result = Instantiate(resultPage);
        result.transform.parent = GameObject.Find("Canvas").transform;
        result.transform.localPosition = Vector2.zero;
        Destroy(this.gameObject);

    }


}

