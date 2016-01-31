using UnityEngine;
using System.Collections;

public class State_HopRight : StateMachineBehaviour
{
    private float m_IKTransitionTime = 0.0f;
    private float m_MoveTime = 0.0f;
    private float m_MoveTimer = 0.0f;
    private float m_MoveDistance = 0.0f;
    private bool m_IKSwitched = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_IKTransitionTime = animator.GetFloat("IKTransitionTime");
        m_MoveTime = animator.GetFloat("MoveTime");
        m_MoveDistance = animator.GetFloat("MoveDistance");

        Vector3 newPosition = animator.transform.position;
        newPosition.x += m_MoveDistance;

        Hashtable hash = iTween.Hash( "position", newPosition, "time", m_MoveTime, "easetype", iTween.EaseType.easeOutElastic);
        iTween.MoveTo(animator.gameObject, hash);

        m_IKSwitched = false;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        m_MoveTimer += Time.deltaTime;

        //-- watch for when the switch the ik target
        if(m_IKSwitched == false)
        {
            if (m_MoveTimer > m_IKTransitionTime)
            {
                //--switch IK target
                m_IKSwitched = true;
            }
        }
        
        //-- watch for when the state is done
        if(m_MoveTimer > m_MoveTime)
        {
            //-- go back to idle
            animator.SetTrigger("Idle");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
