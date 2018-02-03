using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class PlayerHealth : NetworkBehaviour
{

    public static int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthRemaining;
    public RectTransform TotalHealthImg;
    float widthRatio;
    Movt GetColor;
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
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcRespawn();
            Debug.Log("Dead");
        }




        widthRatio = maxHealth / TotalHealthImg.rect.width;
        Debug.Log(widthRatio);
    }

    void OnChangeHealth(int Health)
    {
        healthRemaining.sizeDelta = new Vector2(Health*2, healthRemaining.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
  {
        //Debug.Log(GetColor.PlayerColor+"Respawning");
        GetColor = this.GetComponent<Movt>();
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
