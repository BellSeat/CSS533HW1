using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This Class is checking if any object is invisible in the Camera view area
// If the object is invisible, then the object will be visible
public class QuickScanner : MonoBehaviour
{
    [SerializeField]private Collider2D[] colliders;

    // Start is called before the first frame update
    void Start()
    {
        Camera camera = Camera.main;

        // Get all the colliders in the camera view area
        colliders = Physics2D.OverlapAreaAll(camera.ViewportToWorldPoint(new Vector3(0, 0, camera.nearClipPlane)), camera.ViewportToWorldPoint(new Vector3(1, 1, camera.nearClipPlane)));

        
        // re-order the colliders from the left to right and from the top to bottom
        System.Array.Sort(colliders, CompareByXThenY);
       
        // set the object visible
        StartCoroutine(setVisible());

    }

    public void OnDestroy()
    {
        // Destroy the object
        Destroy(gameObject, 2);
    
    }

    // if the object is invisible, then set the object visible, one by one pre 2 seconds
    IEnumerator setVisible() {

        int countChild = colliders.Length;
        for (int i = 0; i < countChild; i++) {
            yield return new WaitForSeconds(1);
             
            colliders[i].gameObject.GetComponent<PlayerBehavior>().setInvisible(false, 4f);
            // Debug.Log("Set " + colliders[i].gameObject.name + " visible");
            //yield return new WaitForSeconds(1);
            // wait for 2 seconds

        }
        yield return null;
        OnDestroy();
    }

    private static int CompareByXThenY(Collider2D x, Collider2D y)
    {
        int result = x.transform.position.x.CompareTo(y.transform.position.x);
        if (result == 0)
        {
            result = x.transform.position.y.CompareTo(y.transform.position.y);
        }
        return result;
    }
}
