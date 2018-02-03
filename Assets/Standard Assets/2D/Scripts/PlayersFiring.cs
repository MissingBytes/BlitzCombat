using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayersFiring : NetworkBehaviour {
    GameObject[] Players;
    float Firerate=2;
    public GameObject BulletTrailPrefab;
    float TimeToFire = 0;
    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        RpcFire();
    }

    [ClientRpc]
    void RpcFire()
    {
        Players = GameObject.FindGameObjectsWithTag("Player");
        //Debug.Log("Players " + Players + ":" + Players.Length);
        for (int i = 0; i < Players.Length; i++)
        {
            //Debug.Log("IsFiring:" + Players[i].GetComponent<Movt>().isFiring + i);
            if (Players[i].GetComponent<Movt>().isFiring == true)
            {
                 //TimeToFire = Time.time + 1 / Firerate;

                //Players[i].GetComponent<ArmRotation>().Arm.rotation = Quaternion.Euler(0f, 0f, Players[i].GetComponent<ArmRotation>().rotZ);
                //Debug.Log("Bullet spawned for Player" +i +" "+ Players[i].GetComponent<Movt>().rotZ);

                
                GameObject bullet = (GameObject)Instantiate(BulletTrailPrefab, Players[i].transform.Find("Arm/Gun/firepoint").position, Players[i].transform.Find("Arm").rotation);
                bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(2000, 0));

                Players[i].GetComponent<Movt>().isFiring = false;
                Destroy(bullet, 2);
            }

        }
    }
}
