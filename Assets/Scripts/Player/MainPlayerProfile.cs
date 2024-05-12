using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPlayerProfile : MonoBehaviour
{
    [SerializeField] private string playerName;
    [SerializeField] private int playerLevel;
    [SerializeField] private int playerScore;
    [SerializeField] private int playerEXP;

    bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        downloadUserProfileFromServer();
    }

    private void OnGUI()
    {
        if(isPaused)
        {
            GUI.Label(new Rect(10, 10, 100, 20), "Game is paused");
            uploadUserProfileToServer();
        }
    }

    public void OnApplicationPause(bool pause)
    {
          isPaused = pause;
    }

    private void OnApplicationFocus(bool hasFocus){

        isPaused = !hasFocus;
        downloadUserProfileFromServer();


    }

    private void uploadUserProfileToServer()
    {
        // upload player profile to server

    }

    private void downloadUserProfileFromServer()
    {
        // download player profile from server
    }

    public void setPlayerName(string name)
    {
        playerName = name;
    }

    public void setPlayerLevel(int level)
    {
        playerLevel = level;
    }

    public void setPlayerScore(int score)
    {
        playerScore = score;
    }

    public void setPlayerEXP(int exp)
    {
        playerEXP = exp;
    }

    public string getPlayerName()
    {
        return playerName==null? "Sean" : playerName;
    }

    public int getPlayerLevel()
    {
        return playerLevel;
    }

    public int getPlayerScore()
    {
        return playerScore;
    }

    public int getPlayerEXP()
    {
        return playerEXP;
    }
    public void addPlayerEXP(int exp) { 
        
        playerEXP += exp;
        caculateLevel();
        uploadUserProfileToServer();
    }

    public void addPlayerScore(int score)
    {
        playerScore += score;
        uploadUserProfileToServer();
    }

    private void caculateLevel() {
        if (this.playerEXP >= 100)
        {
            int add = playerEXP / 100;
            playerLevel+=add;
            playerEXP = playerEXP % 100;
        }
    }
}
