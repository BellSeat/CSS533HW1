using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameFactory : MonoBehaviour
{
    [SerializeField] private GameObject GuessANumberGame, MutiChoiceGame, parent;
    

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Canvas/MiniGame Template");
        GuessANumberGame = Resources.Load<GameObject>("Prefab/MiniGame/Guess A Number") as GameObject;
        MutiChoiceGame = Resources.Load<GameObject>("Prefab/MiniGame/Multipul Choice Game") as GameObject;
    }

    public void CrateARandGame() { 
    
        int rand = Random.Range(0, 2);
        if (rand == 0)
        {
            Debug.Log("Create Guess A Number Game");
            GameObject game = Instantiate(GuessANumberGame);
            game.transform.SetParent(parent.transform, false);

        }
        else
        {
            Debug.Log("Create Muti Choice Game");
            GameObject game = Instantiate(MutiChoiceGame);
            game.transform.SetParent(parent.transform, false);

        }
    }


}
