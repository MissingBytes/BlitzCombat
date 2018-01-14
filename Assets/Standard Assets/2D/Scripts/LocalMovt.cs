using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
//using UnitySampleAssets._2D;

public class LocalMovt :NetworkBehaviour {
    Transform player;
    // Use this for initialization
    public override void OnStartLocalPlayer()
    {
        player = transform.Find("Canvas");
        player.gameObject.SetActive(true);

        //GetComponent<MeshRenderer>().material.color = Color.red;
    }
    void Start () {
        /*if (isLocalPlayer)
        {
            GameObject Player=GetComponent<Player1>
            //GetComponent<Platformer2DUserControl>().enabled = true;
            //GetComponentInChildren<Canvas>().enabled = true;
           
        }*/

	}
	
}
