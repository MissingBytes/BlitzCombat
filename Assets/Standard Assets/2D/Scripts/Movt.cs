using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Movt : NetworkBehaviour
{
    public VirtualJoyStick joyStickRight;
    public VirtualJoyStickL joyStickLeft;
    public float speed=10;             //Floating point variable to store the player's movement speed.
    public Transform Arm;
    private Rigidbody2D rb2d;       //Store a reference to the Rigidbody2D component required to use 2D Physics.
    [SyncVar]public float rotZ;

    public float FireRate = 3;
    public float Damage = 10;
    public LayerMask WhatToHit;

    public GameObject BulletTrailPrefa;
    public GameObject Gun;
    public Transform fPoint;
    //public VirtualJoyStick JoyStick;
    public TextMesh PlayerName;
    [SyncVar] public String pName = "NoobNoob";
    public int TeamNo;
    [SyncVar] public Color PlayerColor;

    float TimeToFire = 0;

    public Vector2 fPos;
    //public List<bool> isFiring = new List<bool>();
    [SyncVar] public bool isFiring = false;
    // Use this for initialization
    void Start()
    {
       
        Arm = this.transform.Find("Arm");
        fPoint = transform.Find("Arm/Gun/firepoint");

        if (PlayerColor == Color.blue)
            this.transform.position = new Vector2(-18f + UnityEngine.Random.Range(0, 8f), 28.45f);
        if (PlayerColor == Color.red)
            this.transform.position = new Vector2(49.9f + UnityEngine.Random.Range(0, 8f), 28.45f);

        PlayerName.text = pName;

        Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
            r.material.color = PlayerColor;
            
    }

    //FixedUpdate is called at a fixed interval and is independent of frame rate. Put physics code here.
    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;
            //Store the current horizontal input in the float moveHorizontal.
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rotZ = Mathf.Atan2(joyStickRight.Vertical(), joyStickRight.Horizontal()) * Mathf.Rad2Deg;
        Arm.rotation = Quaternion.Euler(0, 0, rotZ);

        GetComponent<Rigidbody2D>().velocity = new Vector2(joyStickLeft.Horizontal() * speed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, joyStickLeft.Vertical()*20));

        fPos = fPoint.position;
        if ((Math.Abs(joyStickRight.Vertical()) > 0.6 || Math.Abs(joyStickRight.Horizontal()) > 0.6) && Time.time > TimeToFire && joyStickRight.isTouched)
        {
            TimeToFire = Time.time + 1 / FireRate;
            CmdFiring();
            CmdFire(fPos, new Vector2(joyStickRight.Horizontal(), (joyStickRight.Vertical())));
       
        }

    }

    [Command]
    void CmdFire(Vector2 fPos, Vector2 HV)
    {
        RaycastHit2D hit = Physics2D.Raycast(fPos, HV, 1000, WhatToHit);
        Debug.DrawLine(fPos, (fPos - HV) * 1000, Color.red);
        Debug.Log("Hit" + hit.collider.gameObject.name);
        if (hit.collider.gameObject.tag == "Player")
        {
            Debug.Log("InCollisonRaycast ,collider:" + hit.collider.name);
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

    [Command]
    void CmdFiring()
    {
        //rotZ = Mathf.Atan2(joyStickRight.Vertical(), joyStickRight.Horizontal()) * Mathf.Rad2Deg;
        isFiring = true;
    }

    public override void OnStartLocalPlayer()
    {
        //this.gameObject.layer = 6+TeamNo*2;
        Camera.main.GetComponent<Camera2DFollow>().setTarget(gameObject.transform);

    }
}
