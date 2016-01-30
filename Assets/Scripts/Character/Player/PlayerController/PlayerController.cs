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

    }
}
