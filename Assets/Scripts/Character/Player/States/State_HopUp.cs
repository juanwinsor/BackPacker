﻿using UnityEngine;
using System.Collections;

public class State_HopUp : StateMachineBehaviour
{
  private float m_IKTransitionTime = 0.0f;
  private float m_MoveTimer = 0.0f;
  private float m_MoveTime = 0.0f;

  private PlayerController m_PlayerController;

  override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    m_PlayerController = animator.gameObject.GetComponent<PlayerController>();

    m_IKTransitionTime = animator.GetFloat("IKTransitionTime");
    m_MoveTime = animator.GetFloat("MoveTime");
    m_MoveTimer = 0.0f;
  }

  override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {
    m_MoveTimer += Time.deltaTime;

    if (m_MoveTimer > m_IKTransitionTime)
    {
      //-- switch ik targets
      m_PlayerController.SwitchNextIKTarget();
    }
    if (m_MoveTimer > m_MoveTime)
    {
      animator.SetTrigger("Idle");
    }
  }

  override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
  {

  }
}
