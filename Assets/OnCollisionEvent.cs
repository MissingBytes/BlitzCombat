using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEvent : MonoBehaviour {
    
    
    // Use this for initialization
    void OnCollisionEnter2D(Collision2D col)
    {
        GameObject hit = col.gameObject;
        PlayerHealth pH = hit.GetComponent<PlayerHealth>();
        if (col.gameObject.tag == "Player")
        {
            Destroy(gameObject);
            pH.TakeDamage(10);
        }
        if (col.gameObject.tag == "Box" || col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            Debug.Log("Bullet Destroyed");
        }
    }
   


    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        //OnCollisionEnter();
	}
}
