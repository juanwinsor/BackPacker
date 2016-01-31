using UnityEngine;
using System.Collections;

public class State_Idle : StateMachineBehaviour
{
    private PlayerController m_PlayerController = null;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if( m_PlayerController == null )
        {
            m_PlayerController = animator.gameObject.GetComponent<PlayerController>();
        }
        
        m_PlayerController.isBusy = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_PlayerController.isBusy = true;
    }
}
