﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{

    public static int maxHealth = 250;
    [SyncVar(hook = "OnChangeHealth")] public int currentHealth = 250;
    public RectTransform healthRemaining;
    public RectTransform TotalHealthImg;
    float widthRatio;
    ArmRotation GetColor;
    Color PC4Respawn;
   
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        

        Debug.Log("Health:"+currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 250;
            RpcRespawn();
            Debug.Log("Dead");
        }
        else
            currentHealth -= amount;

        widthRatio = maxHealth / TotalHealthImg.rect.width;
        Debug.Log(widthRatio);
    }

    void OnChangeHealth(int Health)
    {
        healthRemaining.sizeDelta = new Vector2(Health/1.25f, healthRemaining.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
  {
        //Debug.Log(GetColor.PlayerColor+"Respawning");
        GetColor = this.GetComponent<ArmRotation>();
        PC4Respawn = GetColor.PlayerColor;
        if(isLocalPlayer)
        {
            if (PC4Respawn == Color.blue)
                    this.transform.position = new Vector2(-18f + UnityEngine.Random.Range(0, 8f), 28.45f);
            if (PC4Respawn == Color.red)
                this.transform.position = new Vector2(49.9f + UnityEngine.Random.Range(0, 8f), 28.45f);
        }
    }
}
