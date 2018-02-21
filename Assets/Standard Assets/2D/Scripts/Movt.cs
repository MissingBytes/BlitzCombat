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
    public Transform Arm;
    [SyncVar] public float ArmRotationInZ;

    public float FireRate = 3;
    public float Damage = 10;
    public LayerMask WhatToHit;

    public Transform fPoint;
    public TextMesh PlayerName;
    [SyncVar] public String pName = "NoobNoob";
    public int TeamNo;
    [SyncVar] public Color PlayerColor;

    public float speed = 10;
    public float Flyspeed = 15;

    float TimeToFire = 0;

    public Vector2 fPos;
    [SyncVar] public bool isFiring = false;
    public PlayerAnimator pa;
    bool facingRight = true;
    SpriteRenderer mySpriteRenderer;
    public Image HealthLeftColor;
    public GameObject Meelebutton;

    [SyncVar] public string PlayerCharacter;
    void Start()
    {
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        Arm = this.transform.Find("Arm");
        fPoint = transform.Find("Arm/Gun/firepoint");
        if (PlayerColor == Color.blue)
            this.transform.position = new Vector2(-18f + UnityEngine.Random.Range(0, 8f), 28.45f);
        if (PlayerColor == Color.red)
            this.transform.position = new Vector2(49.9f + UnityEngine.Random.Range(0, 8f), 28.45f);

        PlayerName.text = pName;
        HealthLeftColor.color = PlayerColor;
        /*Renderer[] rends = GetComponentsInChildren<Renderer>();
        foreach (Renderer r in rends)
            r.material.color = PlayerColor;
          */
    }

    void FixedUpdate()
    {
        if (!isLocalPlayer)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        ArmRotationInZ = Mathf.Atan2(joyStickRight.Vertical(), joyStickRight.Horizontal()) * Mathf.Rad2Deg;
        Arm.rotation = Quaternion.Euler(0, 0, ArmRotationInZ);

        PlayerFacing(ArmRotationInZ);

        GetComponent<Rigidbody2D>().velocity = new Vector2(joyStickLeft.Horizontal() * speed, GetComponent<Rigidbody2D>().velocity.y);

        GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, joyStickLeft.Vertical() * Flyspeed));

        Debug.Log("Fly speed" + joyStickLeft.Vertical() * Flyspeed);



    }

    private void Update()
    {
        CallAnimation(joyStickLeft.Horizontal(), joyStickLeft.Vertical());
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
        isFiring = true;
    }

    public override void OnStartLocalPlayer()
    {
        Camera.main.GetComponent<Camera2DFollow>().setTarget(gameObject.transform);
    }

    public void Assassin()
    {
        speed = 10;
        Flyspeed = 30;
        FireRate = 2;
        PlayerCharacter = "Assassin";
        Meelebutton.SetActive(false);
        Debug.Log("Character:"+PlayerCharacter);
    }

    public void DamageBuff()
    {
        speed = 6;
        Flyspeed = 20;
        FireRate = 10;
        PlayerCharacter = "Sentinel";
        Meelebutton.SetActive(false);
        Debug.Log("Character:" + PlayerCharacter);

    }

    public void SentinelBuff()
    {
        speed = 5;
        Flyspeed = 17;
        FireRate = 4;
        PlayerCharacter = "Sentinel";
        Meelebutton.SetActive(false);
        Debug.Log("Character:" + PlayerCharacter);

    }

    void CallAnimation(float horizontal, float vertical)
    {
        if (horizontal == 0)
            pa.IdleAnimation();
        else if ((horizontal > 0 && facingRight) || (horizontal < 0 && !facingRight))
            pa.RunForwardAnimation();
        else if ((horizontal < 0 && facingRight) || (horizontal > 0 && !facingRight))
            pa.RunBackwardAnimation();

    }

    void FlipSprite()
    {
        facingRight = !facingRight;  
        mySpriteRenderer.flipX = !facingRight;

    }

    void PlayerFacing(float RotationInZ)
    {
        if ((RotationInZ > 90 && RotationInZ < 180) || (RotationInZ > -180 && RotationInZ < -90))
            if (facingRight)
            {   FlipSprite();
                //GunFlip();
            }
        if (RotationInZ < 90 && RotationInZ > -90)
            if (!facingRight)
            {
                FlipSprite();
                //GunFlip();
            }
    }

    void GunFlip()
    {
        Vector3 theScale = Arm.transform.localScale;
        theScale.y *= -1;
        Arm.transform.localScale = theScale;
    }




}
