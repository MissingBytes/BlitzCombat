using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Grenade : NetworkBehaviour {

    public float delay = 1.5f;
    public float radius = 10;
    public float explosionForce = 100;
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
            CmdExplode();
            hasExploded = true;
        }
	}

    
    void CmdExplode()
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
             
                        pH.TakeDamage(70);
                        
                    }
                //rb.AddForce(new Vector3 (100,100,0)-transform.position);
                Debug.Log("ForceADded");
            }
        }
        Destroy(gameObject);
    }
  


}


