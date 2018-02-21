using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Networking;

public class PlayerCharacterRender : NetworkBehaviour {

    public Sprite spAssassin;
    public Sprite spDamage;
    public Sprite spSentinel;

    public RuntimeAnimatorController AnimAssassin;
    public RuntimeAnimatorController AnimDamage;
    public RuntimeAnimatorController AnimSentinel;

    Animator animator;

    public GameObject body;
    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update () {
		
	}


    public void RenderSprite(string Character)
    {
        body.SetActive(false);
        if (Character == "Assassin")
        {
            GetComponent<SpriteRenderer>().sprite = spAssassin;
            animator.runtimeAnimatorController = AnimAssassin;
        }
        else if (Character == "Damage")
        {
            GetComponent<SpriteRenderer>().sprite = spDamage;
            animator.runtimeAnimatorController = AnimDamage;
        }
        if (Character == "Sentinel")
        { GetComponent<SpriteRenderer>().sprite = spSentinel;
          animator.runtimeAnimatorController = AnimSentinel;
        }
    }


}
