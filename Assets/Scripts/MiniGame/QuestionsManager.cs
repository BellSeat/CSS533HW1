using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Question
{
    public string question;
    public string a;
    public string b;
    public string c;
    public string d;
    public string correct;
}

public class QuestionsManager : MonoBehaviour
{
    private List<Question> questions;

    void Awake()
    {
        LoadQuestions();
    }

    void LoadQuestions()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "questions.json");
        if (File.Exists(filePath))
        {
            string dataAsJson = File.ReadAllText(filePath);
            questions = JsonUtility.FromJson<QuestionData>("{\"questions\":" + dataAsJson + "}").questions;
            Debug.Log(dataAsJson);
        }
        else
        {
            Debug.LogError("Cannot load questions!");
        }
    }

    public Question GetRandomQuestion()
    {
        if (questions != null && questions.Count > 0)
        {
            int randomIndex = Random.Range(0, questions.Count);
            return questions[randomIndex];
        }
        return null;
    }

    public string GetQuestionDescription(Question question)
    {
        return question.question;
    }

    public string[] GetChoices(Question question)
    {
        return new string[] { question.a, question.b, question.c, question.d };
    }

    public int GetCorrectChoiceIndex(Question question)
    {
        switch (question.correct)
        {
            case "a": return 0;
            case "b": return 1;
            case "c": return 2;
            case "d": return 3;
            default: return 0;
        }
    }
}

[System.Serializable]
public class QuestionData
{
    public List<Question> questions;
}
