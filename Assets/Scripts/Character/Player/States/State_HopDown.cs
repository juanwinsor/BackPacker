using UnityEngine;
using System.Collections;

public class State_HopDown : StateMachineBehaviour
{
    private float m_IKTransitionTime = 0.0f;
    private float m_MoveTimer = 0.0f;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_IKTransitionTime = animator.GetFloat("IKTransitionTime");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MoveTimer += Time.deltaTime;

        //-- switch ik targets
        if (m_MoveTimer > m_IKTransitionTime)
        {

        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
