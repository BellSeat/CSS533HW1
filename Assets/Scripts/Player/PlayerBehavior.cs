using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]private bool invisible = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void setInvisible(bool invisible, float timer) {
        transform.GetComponent<SpriteRenderer>().enabled = !invisible;
        StartCoroutine(setInvisible(timer));
    }

    IEnumerator setInvisible(float timer)
    {
        invisible = true;
        yield return new WaitForSeconds(timer);
        transform.GetComponent<SpriteRenderer>().enabled = !invisible;
    }
}
