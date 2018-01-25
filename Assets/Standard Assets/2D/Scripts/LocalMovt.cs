using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnitySampleAssets._2D;

public class LocalMovt :NetworkBehaviour {
    public Transform player;
    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        player = transform.Find("Canvas");
        player.gameObject.SetActive(true);
        Debug.Log("Controls enabled");

        //GetComponent<MeshRenderer>().material.color = Color.red;
    }
    void Start () {
      

	}
	
}
