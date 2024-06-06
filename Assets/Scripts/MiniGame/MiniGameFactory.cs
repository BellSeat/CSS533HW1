using System.Collections.Generic;
using UnityEngine;

public class MiniGameFactory : MonoBehaviour
{
    public PlayerDataManager playerDataManager;
    [SerializeField] private GameObject GuessANumberGame, MultiChoiceGame, VoiceGame, parent;

    // Start is called before the first frame update
    void Start()
    {
        parent = GameObject.Find("Canvas/MiniGame Template");
        GuessANumberGame = Resources.Load<GameObject>("Prefab/MiniGame/Guess A Number") as GameObject;
        MultiChoiceGame = Resources.Load<GameObject>("Prefab/MiniGame/Multipul Choice Game") as GameObject;
        VoiceGame = Resources.Load<GameObject>("Prefab/MiniGame/Voice Game") as GameObject;

        playerDataManager = GameObject.Find("MainPlayer").GetComponent<PlayerDataManager>();
    }

    public void CreateARandGame()
    {
        List<GameObject> availableGames = new List<GameObject>();

        if (playerDataManager.CheckGameOn(0))
        {
            availableGames.Add(MultiChoiceGame);
        }

        if (playerDataManager.CheckGameOn(1))
        {
            availableGames.Add(GuessANumberGame);
        }

        if (playerDataManager.CheckGameOn(2))
        {
            availableGames.Add(VoiceGame);
        }

        if (availableGames.Count > 0)
        {
            int rand = Random.Range(0, availableGames.Count);
            GameObject selectedGame = availableGames[rand];
            Debug.Log("Create Game: " + selectedGame.name);
            GameObject game = Instantiate(selectedGame);
            game.transform.SetParent(parent.transform, false);
        }
        else
        {
            Debug.LogWarning("No available games to create!");
        }
    }

    public void CreateAGame(int gameNum)
    {
        List<GameObject> availableGames = new List<GameObject>();

        if (playerDataManager.CheckGameOn(0))
        {
            availableGames.Add(MultiChoiceGame);
        }

        if (playerDataManager.CheckGameOn(1))
        {
            availableGames.Add(GuessANumberGame);
        }

        if (playerDataManager.CheckGameOn(2))
        {
            availableGames.Add(VoiceGame);
        }

        GameObject selectedGame = availableGames[gameNum];
        Debug.Log("Create Game: " + selectedGame.name);
        GameObject game = Instantiate(selectedGame);
        game.transform.SetParent(parent.transform, false);
    }
}
