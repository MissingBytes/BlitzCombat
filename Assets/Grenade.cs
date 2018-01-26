using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

    public float delay = 3f;
    public float radius = 7;
    public float explosionForce = -300;
    public GameObject ExplosionEffect;
    public Collider2D[] ExplosinonColliders;
    float countdown;
    bool hasExploded = false;
	// Use this for initialization
	void Start () {
        countdown = delay;
	}
	
	// Update is called once per frame
	void Update () {

        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
	}

    void Explode()
    {

        Instantiate(ExplosionEffect, transform.position, transform.rotation);

        ExplosinonColliders = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (Collider2D col in ExplosinonColliders)
        {
            Rigidbody2D rb = col.GetComponentInParent<Rigidbody2D>();
            if (rb != null)
            {
                for (int i=0;i<100;i++)
                    rb.AddExplosionForce(explosionForce, transform.position, radius);
                Debug.Log("Explodes");


                if (col.gameObject.tag == "Player")
                    {
                    //GameObject hit = ;
                    PlayerHealth pH = col.gameObject.GetComponent<PlayerHealth>();
                    Debug.Log("GrenadeHits");
             
                        pH.TakeDamage(30);
                        
                    }
                //rb.AddForce(new Vector3 (100,100,0)-transform.position);
                Debug.Log("ForceADded");
            }
        }



        Destroy(gameObject);
    }
  


}


