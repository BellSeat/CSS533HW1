using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;
using NUnit.Framework.Constraints;
public class PopUpMenuUI : MonoBehaviour
{
    [SerializeField] private TMP_Text enemyName;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button stealButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private PlayerBehavior enemy;
    [SerializeField] private GameObject resultPage;
    public string prefix = "What do you want to do ";


    // Start is called before the first frame update
    void Start()
    {
        Assert.IsNotNull(enemyName, "Enemy Name not found");
        Assert.IsNotNull(attackButton, "Attack Button not found");
        Assert.IsNotNull(stealButton, "Steal Button not found");

        attackButton.onClick.AddListener(AttackButtonClicked);
        stealButton.onClick.AddListener(StealButtonClicked);
        closeButton.onClick.AddListener(CloseButtonClicked);
        setEnemyName(enemy.name);

        resultPage = Resources.Load<GameObject>("Prefab/UI/ResultMenu") as GameObject;
    }

    // Update is called once per frame


    public void AttackButtonClicked()
    {
        Debug.Log("Attack Button Clicked");
        enemy.setInvisible(true, 10);
        GameObject result = Instantiate(resultPage);
        result.transform.parent = GameObject.Find("Canvas").transform;
        result.transform.localPosition = Vector2.zero;

        Destroy(this.gameObject);
    }

    public void StealButtonClicked()
    {
        Debug.Log("Steal Button Clicked");
        enemy.setInvisible(false, 10);
        GameObject result = Instantiate(resultPage);
        result.transform.parent = GameObject.Find("Canvas").transform;
        result.transform.localPosition = Vector2.zero;

        Destroy(this.gameObject);
    }

    public void CloseButtonClicked()
    {
        Debug.Log("Close Button Clicked");
        Destroy(this.gameObject);
    }

    public void setEnemyName(string name)
    {
        
        enemyName.text = prefix + enemy.name + " ?";
    }

    public void setEnemy(PlayerBehavior enemy)
    {
        this.enemy = enemy;
    }
}
