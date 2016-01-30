using UnityEngine;
using System.Collections;

public enum MoveDirection
{

}

public class PlayerController : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        //-- give the gameplay manager a reference to the player controller
        GameplayManager.Instance.RegisterPlayerController( this );
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
                break;
            case PlayerState.Hop_Up:
                break;
            case PlayerState.Hop_Left:
                break;
            case PlayerState.Hop_Right:
                break;
            case PlayerState.Slide:
                break;
            case PlayerState.Die:
                break;
        }
    }
}
