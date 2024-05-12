using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField] private bool invisible = false;
    [SerializeField] private GameObject PopUpMenu;
    [SerializeField] private int luckyNumber;
    // Start is called before the first frame update
    public void Start()
    {
       PopUpMenu = Resources.Load("Prefab/UI/PopUpMenu") as GameObject;
        luckyNumber = Random.Range(1, 101);
       // Assert.IsNotNull(PopUpMenu);
    }


    // Update is called once per frame
    public void setInvisible(bool invisible, float timer) {
        this.invisible = invisible;
        if (invisible == true) { 
            transform.GetComponent<SpriteRenderer>().enabled = false;
            return;
        }
        transform.GetComponent<SpriteRenderer>().enabled = !invisible;
        StartCoroutine(setInvisible(timer));
    }

    IEnumerator setInvisible(float timer)
    {
        invisible = true;
        yield return new WaitForSeconds(timer);
        transform.GetComponent<SpriteRenderer>().enabled = !invisible;
    }

    private void OnMouseDown()
    {
        if (!invisible)
        {
            Debug.Log("Player is invisible");
        }
        else
        {
            Debug.Log("Player is visible");
            GameObject popUpMenu = Instantiate(PopUpMenu, Vector3.zero, Quaternion.identity);

            popUpMenu.transform.SetParent(GameObject.Find("Canvas").transform);
            popUpMenu.GetComponent<PopUpMenuUI>().setEnemy(this);
            popUpMenu.GetComponent<PopUpMenuUI>().setEnemyName(transform.name);
            popUpMenu.transform.localPosition = Vector2.zero;
        }
    }
    
    public void setLuckyNumber(int number)
    {
        luckyNumber = number;
    }

    public int getLuckyNumber()
    {
        return luckyNumber;
    }
}
