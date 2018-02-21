using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseBuff : MonoBehaviour {

    float CloseTimer ;
	// Use this for initialization
	void Start () {
        CloseTimer = Time.time;
    }
	
	// Update is called once per frame
	void Update () {
       /* if (Time.time - CloseTimer > 5)
        {
            HidePanel();
        }*/

    }

    public void HidePanel()
    {   
        gameObject.SetActive(false);
    }

    public void ShowPanel()
    {
        CloseTimer = Time.time;
        gameObject.SetActive(true);
       
    }
}
