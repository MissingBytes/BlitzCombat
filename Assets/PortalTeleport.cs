using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour {

    public GameObject PortalOut;
    // Use this for initialization
    void Start()
    {
        char PortalNo = gameObject.name[gameObject.name.Length - 1];
        string outPortal = "PortalOut" + PortalNo;
        PortalOut = GameObject.Find(outPortal);
    }
	// Update is called once per frame
	void Update () {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = PortalOut.transform.position;
    }
}
