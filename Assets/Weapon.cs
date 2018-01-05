using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {
    public float FireRate = 10;
    public float Damage = 10;
    public LayerMask WhatToHit;

    public Transform BulletTrailPrefab;

    public VirtualJoyStick JoyStick;

    float TimeToFire = 0;
    public Transform firePoint;
    // Use this for initialization
    void Start() {
        firePoint = transform.Find("firepoint");
        if (firePoint == null)
        { Debug.Log("NOT found"); }

    }

    // Update is called once per frame
    void Update() {
        //Shoot();
        if (FireRate == 0)
        {
            //if (Input.GetButtonDown("Fire1"))
            if(Math.Abs(JoyStick.Vertical())>0.6 || Math.Abs(JoyStick.Horizontal())>0.6)
            {
                Shoot();
            }
        }
        else {
            if (   (Math.Abs(JoyStick.Vertical()) > 0.6 || Math.Abs(JoyStick.Horizontal()) > 0.6) && Time.time > TimeToFire) {
                TimeToFire = Time.time + 1 / FireRate;
                Shoot();
            }
        }
    }

    void Shoot() {
        Debug.Log("Firing");
        Vector2 mouse = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
        Vector2 firepointpos = new Vector2(firePoint.position.x, firePoint.position.y);

        RaycastHit2D hit= Physics2D.Raycast(firepointpos, mouse - firepointpos, 100, WhatToHit);

        Effect();
        Debug.DrawLine(firepointpos, mouse - firepointpos, Color.cyan);
        if (hit.collider != null) {
            Debug.DrawLine(firepointpos, mouse - firepointpos, Color.red);

        }
    }

    void Effect() {
   
        Instantiate(BulletTrailPrefab, firePoint.position, firePoint.rotation);
    }
}
