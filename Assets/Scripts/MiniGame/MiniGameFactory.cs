using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MiniGameFactory : MonoBehaviour
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
    

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Canvas/MiniGame Template");
        GuessANumberGame = Resources.Load<GameObject>("Prefab/MiniGame/Guess A Number") as GameObject;
        MutiChoiceGame = Resources.Load<GameObject>("Prefab/MiniGame/Multipul Choice Game") as GameObject;
        
        //VoiceGame = Resources.Load<GameObject>("Prefab/MiniGame/Voice") as GameObject;
    }

    public void CrateARandGame() { 
        /*
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            Debug.Log("Create Guess A Number Game");
            GameObject game = Instantiate(GuessANumberGame);
            game.transform.SetParent(parent.transform, false);

        }
        /* else if (rand == 1){
            Debug.Log("Create Voice Game");
            GameObject game = Instantiate(VoiceGame);
            game.transform.SetParent(parent.transform, false);
        }
        else
        {*/
        Debug.Log("Create Muti Choice Game");
        GameObject game = Instantiate(MutiChoiceGame);
        game.transform.SetParent(parent.transform, false);

        

        //}
    }


}
