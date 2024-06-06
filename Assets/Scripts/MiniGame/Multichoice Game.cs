using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class MultichoiceGame : MonoBehaviour
{

    [System.Serializable]
    public class Question
    {
        public string question;
        public string choiceA;
        public string choiceB;
        public string choiceC;
        public string choiceD;
        public string correctAnswer;
    }

    [System.Serializable]
    public class QuestionList
    {
        public List<Question> questions;
    }

    
    [SerializeField] private GameObject GuessANumberGame, MutiChoiceGame,/* VoiceGame,*/ parent;
    


    [SerializeField] private bool[] Choice = new bool[4];
    [SerializeField] private bool[] isChoiceActive = new bool[4];
    [SerializeField] private int duration;
    [SerializeField] private int correctChoice, maxActiveAnswer;
    [SerializeField] private CountDown countDown;
    [SerializeField] private Button closeButton;
    [SerializeField] private Button[] choicesButton = new Button[4];
    [SerializeField] private GameObject resultPage, popUpMessage;
    [SerializeField] private TextMeshProUGUI gameDescription;
    [SerializeField] private TextMeshProUGUI[] choiceTexts = new TextMeshProUGUI[4];

    void Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "questions.json");
        
        duration = 30;
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

        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            QuestionList questionList = JsonUtility.FromJson<QuestionList>(dataAsJson);
            if (questionList.questions.Count > 0)
            {
                System.Random rand = new System.Random();
                int randomIndex = rand.Next(questionList.questions.Count);
                Question randomQuestion = questionList.questions[randomIndex];

                Debug.Log("Question: " + randomQuestion.question);
                Debug.Log("A: " + randomQuestion.choiceA);
                Debug.Log("B: " + randomQuestion.choiceB);
                Debug.Log("C: " + randomQuestion.choiceC);
                Debug.Log("D: " + randomQuestion.choiceD);
                Debug.Log("Correct Answer: " + randomQuestion.correctAnswer);

                char correctAnswerChar = randomQuestion.correctAnswer[0];
                int correctAnswerInt = correctAnswerChar - 'A';
                Debug.Log("Correct Answer (as int): " + correctAnswerInt);
                
                this.SetGameDescription(randomQuestion.question);
                this.SetChoiceText(0, randomQuestion.choiceA);
                this.SetChoiceText(1, randomQuestion.choiceB);
                this.SetChoiceText(2, randomQuestion.choiceC);
                this.SetChoiceText(3, randomQuestion.choiceD);
                this.setAnswer(correctAnswerInt);
                
            }
        }
        else
        {
            Debug.LogError("Cannot find file!");
        }
        

        //int randomAnswer = Random.Range(0, 4);
        //setAnswer(randomAnswer);

        countDown.setTimer(duration);
        StartCoroutine(liveAndDie());
    }

    public void setAnswer(int answer)
    {
        if (answer < 0 || answer >= 4)
        {
            correctChoice = Random.Range(0, 4);
        }
        correctChoice = answer;
        for (int i = 0; i < 4; i++)
        {
            Choice[i] = i == correctChoice;
        }
        Debug.Log("Correct Answer is " + correctChoice);
    }

    void setMaxActiveAnswer(int maxActive)
    {
        maxActiveAnswer = maxActive;
    }

    void checkAnswer(int answer)
    {
        Debug.Log("Index is " + answer);
        if (Choice[answer])
        {
            GameObject result = Instantiate(resultPage);
            result.transform.SetParent(GameObject.Find("Canvas").transform, false);
            result.transform.localPosition = Vector2.zero;
            Debug.Log("Correct Answer");
        }
        else
        {
            Debug.Log("Wrong Answer");
            GameObject message = Instantiate(popUpMessage);
            message.transform.SetParent(GameObject.Find("Canvas").transform, false);
            message.GetComponent<PopUpMessage>().setMessage("Wrong Answer");
            Debug.Log("Create popupMessage");
        }
        Destroy(this.gameObject);
    }

    void CloseButtonClicked()
    {
        Debug.Log("Close Button Clicked");
        Destroy(this.gameObject);
    }

    void ChoiceButtonClicked(int index)
    {
        Debug.Log("Index when click is " + index);
        checkAnswer(index);
    }

    IEnumerator liveAndDie()
    {
        yield return new WaitForSeconds(duration);
        GameObject message = Instantiate(popUpMessage);
        message.transform.SetParent(GameObject.Find("Canvas").transform, false);
        message.GetComponent<PopUpMessage>().setMessage("Time is up");
        yield return null;
        Destroy(this.gameObject);
    }

    // New methods to set the text of game description and choices
    public void SetGameDescription(string description)
    {
        gameDescription.text = description;
    }

    public void SetChoiceText(int index, string text)
    {
        if (index >= 0 && index < choiceTexts.Length)
        {
            choiceTexts[index].text = text;
        }
    }
}
