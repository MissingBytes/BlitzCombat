using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    Animator animator;


    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		
	}

    public void IdleAnimation()
    {
        SetAnimationParameterMutalExclusive(animator, "idle");
    }

    public void RunForwardAnimation()
    {
        SetAnimationParameterMutalExclusive(animator, "run");
    }

    public void RunBackwardAnimation()
    {
        SetAnimationParameterMutalExclusive(animator, "runBack");
    }


    private void SetAnimationParameterMutalExclusive(Animator animator, string animation) {
       foreach(AnimatorControllerParameter parameter in animator.parameters) {
          animator.SetBool(parameter.name, parameter.name == animation);
       }
    }﻿
}
