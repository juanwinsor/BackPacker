using UnityEngine;
using System.Collections;

public enum SwipeDirection
{
  None,
  SwipeLeft,
  SwipeRight,
  SwipeUp,
  SwipeDown
}

public enum MoveDirection
{
  MoveLeft,
  MoveRight,
  MoveUp,
  MoveDown
}


public class PlayerController : MonoBehaviour
{
  public TouchInput touchInput;
  private Animator m_Animator;

  public bool isBusy = false;

  private IKTargetSet m_CurrentIKSet;
  private IKTargetSet m_NextIKSet;

  private PlayerIK m_PlayerIK;

  public void SetCurrentIKTarget( IKTargetSet target )
  {
    m_CurrentIKSet = target;
  }

  public void SetNextIKSet( IKTargetSet target )
  {
    //-- this gets called halfway through moving between tiles
    //-- cache previous
    m_CurrentIKSet = m_NextIKSet;
    //-- set next
    m_NextIKSet = target;
  }

  public void ApplyIKTarget( IKTargetSet target )
  {
    //-- we swap our current arm and leg targets to the new tiles targets
    if (target != null && m_PlayerIK != null)
    {
      //-- left arm
      m_PlayerIK.ikLimbs[0].target = target.leftHand.transform;
      m_PlayerIK.ikLimbs[0].elbowTarget = target.leftElbow.transform;

      //-- right arm
      m_PlayerIK.ikLimbs[1].target = target.rightHand.transform;
      m_PlayerIK.ikLimbs[1].elbowTarget = target.rightElbow.transform;

      //-- left leg
      m_PlayerIK.ikLimbs[2].target = target.leftFoot.transform;
      m_PlayerIK.ikLimbs[2].elbowTarget = target.leftKnee.transform;

      //-- right leg
      m_PlayerIK.ikLimbs[3].target = target.rightFoot.transform;
      m_PlayerIK.ikLimbs[3].elbowTarget = target.rightKnee.transform;
    }
  }

  public void SwitchNextIKTarget()
  {
    ApplyIKTarget(m_NextIKSet);
  }

  // Use this for initialization
  void Start()
  {
    m_Animator = GetComponent<Animator>();
    m_PlayerIK = GetComponent<PlayerIK>();

    //-- give the gameplay manager a reference to the player controller
    GameplayManager.Instance.RegisterPlayerController(this);

    //-- register for input events
    touchInput.Swipe += TouchInput_Swipe;
  }

  private void TouchInput_Swipe(TouchEventInfo touchInfo, float deltaTime, Vector2 deltaPosition)
  {
    switch (GetSwipeDirectionFromVector(deltaPosition))
    {
      case SwipeDirection.SwipeDown:
        RequestMove(MoveDirection.MoveDown);
        break;
      case SwipeDirection.SwipeUp:
        RequestMove(MoveDirection.MoveUp);
        break;
      case SwipeDirection.SwipeLeft:
        RequestMove(MoveDirection.MoveLeft);
        break;
      case SwipeDirection.SwipeRight:
        RequestMove(MoveDirection.MoveRight);
        break;
    }
  }

  // Update is called once per frame
  void Update()
  {

  }

  void RequestMove(MoveDirection direction)
  {
    GameplayManager.Instance.RequestMove(direction);
  }

  public void SetPlayerState(PlayerState state)
  {
    //-- this will trigger movement and animations on the character
    switch (state)
    {
      case PlayerState.Bump_Up:
        break;
      case PlayerState.Bump_Down:
        break;
      case PlayerState.Bump_Left:
        break;
      case PlayerState.Bump_Right:
        break;
      case PlayerState.Hop_Down:
        m_Animator.SetTrigger("HopDown");
        break;
      case PlayerState.Hop_Up:
        m_Animator.SetTrigger("HopUp");
        break;
      case PlayerState.Hop_Left:
        m_Animator.SetTrigger("HopLeft");
        break;
      case PlayerState.Hop_Right:
        m_Animator.SetTrigger("HopRight");
        break;
      case PlayerState.Slide:
        break;
      case PlayerState.Die:
        break;
    }
  }

  SwipeDirection GetSwipeDirectionFromVector(Vector2 dir)
  {
    SwipeDirection result = SwipeDirection.None;

    //-- check which axis has the most influence
    if (Mathf.Abs(dir.x) > Mathf.Abs(dir.y))
    {
      if (dir.x < 0)
      {
        result = SwipeDirection.SwipeLeft;
      }
      else
      {
        result = SwipeDirection.SwipeRight;
      }
    }
    else
    {
      if (dir.y < 0)
      {
        result = SwipeDirection.SwipeDown;
      }
      else
      {
        result = SwipeDirection.SwipeUp;
      }
    }

    return result;
  }

  public void SetMoveTime(float moveTime)
  {
    m_Animator.SetFloat("MoveTime", moveTime);
  }

  public void SetIKTransitionTime(float ikTime)
  {
    m_Animator.SetFloat("IKTransitionTime", ikTime);
  }

  public void SetMoveDistance(float distance)
  {
    m_Animator.SetFloat("MoveDistance", distance);
  }

  public void MoveUp()
  {
    if (!isBusy)
    {
      TouchEventInfo temp = new TouchEventInfo();
      TouchInput_Swipe(temp, 0, Vector2.up);
    }
  }

  public void MoveDown()
  {
    if (!isBusy)
    {
      TouchEventInfo temp = new TouchEventInfo();
      TouchInput_Swipe(temp, 0, Vector2.down);
    }
  }

  public void MoveLeft()
  {
    if (!isBusy)
    {
      TouchEventInfo temp = new TouchEventInfo();
      TouchInput_Swipe(temp, 0, Vector2.left);
    }
  }

  public void MoveRight()
  {
    if (!isBusy)
    {
      TouchEventInfo temp = new TouchEventInfo();
      TouchInput_Swipe(temp, 0, Vector2.right);
    }
  }
}
