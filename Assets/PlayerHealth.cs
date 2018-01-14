using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerHealth : MonoBehaviour {

    public static int maxHealth = 1000;
    public int currentHealth = maxHealth;
    public RectTransform healthRemaining;
    public RectTransform TotalHealthImg;
    float widthRatio;
    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void TakeDamage(int amount)
    {
        
        Debug.Log(currentHealth);
        if(currentHealth<=0)
        {
            currentHealth = 0;
            Debug.Log("Dead");
        }
        else
        currentHealth -= amount;

        widthRatio = maxHealth/TotalHealthImg.rect.width;
        healthRemaining.sizeDelta = new Vector2(currentHealth/widthRatio, healthRemaining.sizeDelta.y);
    }
}
