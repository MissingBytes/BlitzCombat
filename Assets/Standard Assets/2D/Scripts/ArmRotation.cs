using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ArmRotation :NetworkBehaviour{

    // Use this for initialization
    public VirtualJoyStick joyStick;
    [SyncVar]Transform Arm;

    float FireRate = 3;
    public float Damage = 10;
    public LayerMask WhatToHit;
    [SerializeField] Vector3 Offset= new Vector3(0f, 0f,0);

    public GameObject BulletTrailPrefa;
    public GameObject Gun;
    [SyncVar] public Transform firePoint;
    //public VirtualJoyStick JoyStick;
    public Transform fPoint;

   [SyncVar] public String pName="NoobNoob";
    public int TeamNo;
   [SyncVar] public Color PlayerColor;

    float TimeToFire = 0;
    public float rotZ;
    public Vector3 fPos;


    void Start()
    {
        if(PlayerColor==Color.blue)
            this.transform.position = new Vector2(-18f + UnityEngine.Random.Range(0, 8f), 28.45f);
        if (PlayerColor == Color.red)
            this.transform.position = new Vector2(49.9f + UnityEngine.Random.Range(0, 8f), 28.45f);

        firePoint = transform.Find("Arm/Gun/firepoint");
        if (firePoint == null)
        { Debug.Log("NOT found"); }

        this.GetComponentInChildren<TextMesh>().text = pName;

        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
            r.material.color = PlayerColor;


   }
    // Update is called once per frame
    void Update () {
        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //difference.Normalize();
        Arm = transform.Find("Arm");
         fPoint= transform.Find("Arm/Gun/firepoint");
        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        rotZ = Mathf.Atan2(joyStick.Vertical(), joyStick.Horizontal()) * Mathf.Rad2Deg;

        Arm.rotation = Quaternion.Euler(0f, 0f, rotZ);
        fPos = fPoint.position;

         {
            if ((Math.Abs(joyStick.Vertical()) > 0.6 || Math.Abs(joyStick.Horizontal()) > 0.6) && Time.time > TimeToFire)
            {
                TimeToFire = Time.time + 1/FireRate ;
               // Debug.Log("X");
               // Debug.Log(isLocalPlayer);
          
                CmdFire(fPos,rotZ);
            }
         }

    }


    [Command]
    void CmdFire(Vector3 fPos,float rotZ)
    {   

       // Debug.Log("In CmdFire");
        GameObject bullet = (GameObject)Instantiate(BulletTrailPrefa, fPos, Quaternion.Euler(0f, 0f, rotZ));
        //bullet.gameObject.layer = 7 + TeamNo * 2;
        bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 40;
        //bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.forward *  40);
        NetworkServer.Spawn(bullet);
        Destroy(bullet, 2.0f);
    }

    public override void OnStartLocalPlayer()
    {
        //this.gameObject.layer = 6+TeamNo*2;
        Camera.main.GetComponent<Camera2DFollow>().setTarget(gameObject.transform);

    }


}



