using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class RotateArm: NetworkBehaviour
{

    // Use this for initialization
    public VirtualJoyStick joyStick;
    public Transform Arm;

    public float FireRate = 10;
    public float Damage = 10;
    public LayerMask WhatToHit;
    [SerializeField] Vector3 Offset = new Vector3(0f, 0f, 0);

    public GameObject BulletTrailPrefa;
    public GameObject Gun;
    public Transform firePoint;
    //public VirtualJoyStick JoyStick;
    public Transform fPoint;


    [SyncVar] public String pName = "NoobNoob";
    public int TeamNo;
    [SyncVar] public Color PlayerColor;

    float TimeToFire = 0;

    public Vector2 fPos;
    //public List<bool> isFiring = new List<bool>();
    public float rotZ;
    [SyncVar] public bool isFiring = false;
    [SyncVar] public float fx;
    [SyncVar] public float fy;


    void Start()
    {
        Arm = transform.Find("Arm");
        fPoint = transform.Find("Arm/Gun/firepoint");


    }
    // Update is called once per frame
    void Update()
    {

        //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //difference.Normalize();

        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotZ = Mathf.Atan2(joyStick.Vertical(), joyStick.Horizontal()) * Mathf.Rad2Deg;
        Arm.rotation = Quaternion.Euler(0f, 0f, rotZ);


    }


    

    public override void OnStartLocalPlayer()
    {
        //this.gameObject.layer = 6+TeamNo*2;
        Camera.main.GetComponent<Camera2DFollow>().setTarget(gameObject.transform);

    }





}



