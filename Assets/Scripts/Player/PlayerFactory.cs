using NUnit.Framework.Internal.Commands;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFactory : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject playerList;
    void Start()
    {
        playerPrefab = Resources.Load("Prefab/Player") as GameObject;
        
        // find a GameObject list playerlist in the scene
        playerList = GameObject.Find("PlayerList");

        for (int i = 0; i <= 5; i++) { 
            createPlayer();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if press key "P" create player
        if (Input.GetKeyUp(KeyCode.P))
        {
            createPlayer();

        }
    }

    void createPlayer()
    {
        GameObject player = Instantiate(playerPrefab);
        player.name = getPlayerName();
        player.AddComponent<PlayerMove>();
        player.transform.position = getPlayerPosition();
        PlayerBehavior temp = player.AddComponent<PlayerBehavior>();
        temp.setInvisible(true,0);
        // set player to a random position
        player.transform.position = new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        player.transform.parent = playerList.transform;
    }

    string getPlayerName()
    {
        return "Player";
    }

    Vector3 getPlayerPosition()
    {
        return new Vector3(0, 0, 0);
    }

}
