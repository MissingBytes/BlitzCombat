using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnCollisionEvent : MonoBehaviour {


    // Use this for initialization
    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Collided with" + col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            GameObject hit = col.gameObject;
            PlayerHealth pH = hit.GetComponent<PlayerHealth>();
            // Destroy(gameObject);
            //pH.TakeDamage(10);
        }
        //if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            // Debug.Log("Bullet Destroyed");
        }
    }


   


    void Start () {
      
    }
	
	// Update is called once per frame
	void Update () {
        //OnCollisionEnter();
	}
}
