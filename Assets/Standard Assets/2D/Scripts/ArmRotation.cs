using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ArmRotation :NetworkBehaviour{

    // Use this for initialization
    public VirtualJoyStick joyStick;
    [SyncVar]Transform Arm;

    public float FireRate = 10;
    public float Damage = 10;
    public LayerMask WhatToHit;
    [SerializeField] Vector3 Offset= new Vector3(0f, 0f,0);

    public GameObject BulletTrailPrefa;
    public GameObject Gun;
    [SyncVar] public Transform firePoint;
    public VirtualJoyStick JoyStick;
    Transform fPoint;

    float TimeToFire = 0;


    void Start()
    {
        firePoint = transform.Find("Arm/Gun/firepoint");
        if (firePoint == null)
        { Debug.Log("NOT found"); }

    }
    // Update is called once per frame
    void Update () {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //difference.Normalize();

        Arm = transform.Find("Arm");
         fPoint= transform.Find("Arm/Gun/firepoint");
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        float rotZ = Mathf.Atan2(joyStick.Vertical(), joyStick.Horizontal()) * Mathf.Rad2Deg;

        Arm.rotation = Quaternion.Euler(0f, 0f, rotZ);

    // if (isLocalPlayer)
         {
            if ((Math.Abs(joyStick.Vertical()) > 0.6 || Math.Abs(joyStick.Horizontal()) > 0.6) && Time.time > TimeToFire)
            {
                TimeToFire = Time.time + 1 / FireRate;
                Debug.Log("X");
                //this.GetComponent<NetworkIdentity>().AssignClientAuthority(this.GetComponent<NetworkIdentity>().connectionToClient);
                Debug.Log(isLocalPlayer);
                //t.rotation = Quaternion.Euler(0f, 0f, rotZ);
                CmdFire(rotZ);
            }
         }

    }


    [Command]
    void CmdFire(float rotZ)
    {   

        Debug.Log("In CmdFire");
        GameObject bullet = (GameObject)Instantiate(BulletTrailPrefa, Arm.position- Offset, Quaternion.Euler(0f, 0f, rotZ));
        //Physics.IgnoreCollision(bullet.GetComponent<Collider>(), GetComponent<Collider>());
        // Debug.Log("rotaion" + firePoint.rotation);
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



