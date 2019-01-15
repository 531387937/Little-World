using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exit : StateMachineBehaviour {

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Walk", false);
        animator.SetBool("Jump", false);
        animator.SetBool("Collect", false);
    }
}
