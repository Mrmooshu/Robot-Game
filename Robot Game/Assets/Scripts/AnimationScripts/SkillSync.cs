using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSync : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.Find("BackArm").GetComponent<Animator>().Play("Skill");
        animator.transform.parent.Find("FrontArm").GetComponent<Animator>().Play("Skill");
        animator.transform.parent.Find("LowerBody").GetComponent<Animator>().Play("Skill");
        animator.transform.parent.Find("UpperBody").GetComponent<Animator>().Play("Skill");
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.parent.Find("BackArm").GetComponent<Animator>().Play("Idle");
        animator.transform.parent.Find("FrontArm").GetComponent<Animator>().Play("Idle");
        animator.transform.parent.Find("LowerBody").GetComponent<Animator>().Play("Idle");
        animator.transform.parent.Find("UpperBody").GetComponent<Animator>().Play("Idle");
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
