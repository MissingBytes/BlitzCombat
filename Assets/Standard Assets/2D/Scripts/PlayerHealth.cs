using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Experimental.UIElements;

public class PlayerHealth : NetworkBehaviour
{

    public static int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")] public int currentHealth = maxHealth;
    public RectTransform healthRemaining;
    public RectTransform TotalHealthImg;
    float widthRatio;
    Movt GetColor;
    Color PC4Respawn;
    public ChooseBuff cb;
    public GameObject ChooseBuffPanel;

    [SyncVar]float factor = 2;
    // Use this for initialization
    void Start()
    {
       // maxHealth = 200;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(int amount)
    {
        if (GetComponent<Movt>().PlayerCharacter == "Sentinel")
            amount = amount / 2;
        currentHealth -= amount;
        Debug.Log("Health:" + currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = maxHealth;
            RpcRespawn();
            
            //cb.ShowPanel();
            
            Debug.Log("Dead");
        }

        widthRatio = maxHealth / TotalHealthImg.rect.width;
        //Debug.Log(widthRatio);
    }

    void OnChangeHealth(int Health)
    {
        healthRemaining.sizeDelta = new Vector2(Health*factor, healthRemaining.sizeDelta.y);
    }

    [ClientRpc]
    void RpcRespawn()
    {
        ChooseBuffPanel.SetActive(true);
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

    [Command]
    public void CmdChangeHP()
    {
        maxHealth = 250;
        factor = 0.8f;
        currentHealth = maxHealth;
        
        Debug.Log("Guardian");
    }

    public void CallChangeHP()
    {
        CmdChangeHP();
    }


}
