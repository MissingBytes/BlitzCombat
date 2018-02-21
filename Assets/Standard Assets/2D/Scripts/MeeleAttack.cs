using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MeeleAttack : NetworkBehaviour {

    public float TimeToAttack=0;
    public float AttackRate=1;
    public Transform Arm;

    bool attacking = false;
    public Collider2D attackTrigger;

    // Use this for initialization
    void Start() {
        //attackTrigger = GetComponent<Collider2D>();
        attackTrigger.enabled = false;
    }

    // Update is called once per frame
    void Update() {
        if (Time.time > TimeToAttack && attacking)
        {

            attackTrigger.enabled = false;
            Debug.Log("DisabledAttacking");
            attacking = false;
        }

    }

    public void Attacking()
    {
        if (Time.time > TimeToAttack)
        {

            attacking = true;
            TimeToAttack = Time.time + 1 / AttackRate;
            attackTrigger.enabled = true;
            Debug.Log("TrueAttacking");
        }
        Debug.Log("Attacking:");
       

    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" )
        {
            PlayerHealth pH = col.gameObject.GetComponent<PlayerHealth>();
            pH.TakeDamage(40);
            attackTrigger.enabled = false;
        }
    }


}
