using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Weapon : NetworkBehaviour {
    public float FireRate = 10;
    public float Damage = 10;
    public LayerMask WhatToHit;


    public GameObject BulletTrailPrefa;
    public GameObject Gun;
    public Transform firePoint;
    public VirtualJoyStick JoyStick;

    float TimeToFire = 0;

    // Use this for initialization
    void Start() {
        firePoint = transform.Find("firepoint");
        if (firePoint == null)
        { Debug.Log("NOT found"); }

    }

    // Update is called once per frame
    void Update()
    {
        //Shoot();



        /*      if (FireRate == 0)
              {
                  //if (Input.GetButtonDown("Fire1"))
                  if(Math.Abs(JoyStick.Vertical())>0.6 || Math.Abs(JoyStick.Horizontal())>0.6)
                  {
                      CmdFire();
                  }
              }
              else {*/
      // if (isLocalPlayer)
        {
            if ((Math.Abs(JoyStick.Vertical()) > 0.6 || Math.Abs(JoyStick.Horizontal()) > 0.6) && Time.time > TimeToFire)
            {
                TimeToFire = Time.time + 1 / FireRate;
                //Debug.Log("X");
                //this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                //Debug.Log(isLocalPlayer);
                
                CmdFire();
            }
        }
    
    }


    [Command]
    void CmdFire()
    {
        // Create the Bullet from the Bullet Prefab
        //Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
        Debug.Log("In CmdFire");
        GameObject bullet = (GameObject)Instantiate(BulletTrailPrefa , firePoint.position , firePoint.rotation);

        // Add velocity to the bullet
        //bullit.rigidbody.AddForce(Vector3.forward * 200.0f, ForceMode.Impulse);
       // Debug.Log(bullet.transform.forward);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 20;
        
        // Spawn the bullet on the Clients
        NetworkServer.Spawn(bullet);
        // Destroy the bullet after 2 seconds
         Destroy(bullet, 2.0f);
    }

   
}



