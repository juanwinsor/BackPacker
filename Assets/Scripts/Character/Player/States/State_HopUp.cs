using UnityEngine;
using System.Collections;

public class State_HopUp : StateMachineBehaviour
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
        
        if(m_MoveTimer > m_IKTransitionTime)
        {
            //-- switch ik targets
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
