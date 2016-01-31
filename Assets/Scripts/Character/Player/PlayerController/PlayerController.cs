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

    // Use this for initialization
    void Start()
    {
        m_Animator = GetComponent<Animator>();

        //-- give the gameplay manager a reference to the player controller
        GameplayManager.Instance.RegisterPlayerController( this );

        //-- register for input events
        touchInput.Swipe += TouchInput_Swipe;        
    }

    private void TouchInput_Swipe(TouchEventInfo touchInfo, float deltaTime, Vector2 deltaPosition)
    {
        switch( GetSwipeDirectionFromVector( deltaPosition ))
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

    void RequestMove( MoveDirection direction )
    {
        GameplayManager.Instance.RequestMove( direction );
    }

    public void SetPlayerState( PlayerState state )
    {
        //-- this will trigger movement and animations on the character
        switch( state )
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

    public void SetMoveTime( float moveTime )
    {
        m_Animator.SetFloat("MoveTime", moveTime);
    }

    public void SetIKTransitionTime(float ikTime)
    {
        m_Animator.SetFloat("IKTransitionTime", ikTime);
    }
}
