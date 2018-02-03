using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ArmRotation :NetworkBehaviour{

    // Use this for initialization
    public VirtualJoyStick joyStick;
    public Transform Arm;

    public float FireRate = 10;
    public float Damage = 10;
    public LayerMask WhatToHit;
    

    public GameObject BulletTrailPrefa;
    public GameObject Gun;
    public Transform firePoint;
    //public VirtualJoyStick JoyStick;
    public Transform fPoint;


   [SyncVar] public String pName="NoobNoob";
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


        if (PlayerColor==Color.blue)
            this.transform.position = new Vector2(-18f + UnityEngine.Random.Range(0, 8f), 28.45f);
        if (PlayerColor == Color.red)
            this.transform.position = new Vector2(49.9f + UnityEngine.Random.Range(0, 8f), 28.45f);

        //firePoint = transform.Find("Arm/Gun/firepoint");
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

        //float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

        rotZ = Mathf.Atan2(joyStick.Vertical(), joyStick.Horizontal()) * Mathf.Rad2Deg;
        Arm.rotation = Quaternion.Euler(0f, 0f, rotZ);
        fPos = fPoint.position;
        fx = fPoint.position.x;
        fy = fPoint.position.y;


        {
            if ((Math.Abs(joyStick.Vertical()) > 0.6 || Math.Abs(joyStick.Horizontal()) > 0.6) && Time.time > TimeToFire && joyStick.isTouched)
            {   
                TimeToFire = Time.time + 1/FireRate ;
                // Debug.Log("X");
                // Debug.Log(isLocalPlayer);
                CmdFiring();
                CmdFire(fPos, new Vector2(joyStick.Horizontal(), (joyStick.Vertical())));

                //GameObject bullet = (GameObject)Instantiate(BulletTrailPrefa, fPos, Quaternion.Euler(0f, 0f, rotZ));
                //bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(4000, 0));
                //isFiring = false;
            }
         }




    }


    [Command]
    void CmdFire(Vector2 fPos,Vector2 HV)
    {
        RaycastHit2D hit= Physics2D.Raycast(fPos,HV , 1000,WhatToHit);
        Debug.DrawLine(fPos, (fPos - HV) *1000,Color.red);
        if (hit.collider.gameObject.tag == "Player")
        {
            Debug.Log("InCollisonRaycast ,collider:"+hit.collider.name);
            PlayerHealth pH = hit.collider.gameObject.GetComponent<PlayerHealth>();
            pH.TakeDamage(10);
        }
        // Debug.Log("In CmdFire");
 
        //bullet.GetComponent<Rigidbody2D>().velocity = bullet.transform.right * 80;
        //= bullet.transform.right * 80;
        //Spawn()
        //bullet.GetComponent<Rigidbody2D>().AddForce(Vector3.forward *  40);

        //NetworkServer.Spawn(bullet);
       // Destroy(bullet, 2.0f);

    }

    public override void OnStartLocalPlayer()
    {
        //this.gameObject.layer = 6+TeamNo*2;
        Camera.main.GetComponent<Camera2DFollow>().setTarget(gameObject.transform);

    }

    [Command]
    void CmdFiring()
    {
        //rotZ = Mathf.Atan2(joyStick.Vertical(), joyStick.Horizontal()) * Mathf.Rad2Deg;
        isFiring = true;
    }

    


}



