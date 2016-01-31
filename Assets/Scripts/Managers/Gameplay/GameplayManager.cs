using UnityEngine;
using System.Collections;

public enum PlayerState
{
    None,
    Hop_Left,
    Hop_Right,
    Hop_Up,
    Hop_Down,
    Bump_Left,
    Bump_Right,
    Bump_Up,
    Bump_Down,
    Slide,
    Die
}

public class GameplayManager : MonoBehaviour
{
    public static GameplayManager Instance = null;

    public LevelManager levelManager;

    public float playerMoveSpeed = 0.5f;

    private PlayerController m_PlayerController;
    private TileScript m_CurrentTile;

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void RequestMove( MoveDirection direction )
    {
        //-- get the tile in the direction
        TileScript nextTile = levelManager.GetTile(direction, m_CurrentTile.myLaneNumber, m_CurrentTile.myElevation);
        //if( nextTile.myTileType != TileType.Blocker )
        if (nextTile != null)
        {
            if( direction == MoveDirection.MoveUp )
            {
                m_PlayerController.SetPlayerState(PlayerState.Hop_Up);
            }
            if (direction == MoveDirection.MoveDown)
            {
                m_PlayerController.SetPlayerState(PlayerState.Hop_Down);
            }
            if (direction == MoveDirection.MoveLeft)
            {
                m_PlayerController.SetPlayerState(PlayerState.Hop_Left);
            }
            if (direction == MoveDirection.MoveRight)
            {
                m_PlayerController.SetPlayerState(PlayerState.Hop_Right);
            }

        }
    }
    


    public void RegisterPlayerController( PlayerController playerController )
    {
        m_PlayerController = playerController;
    }
}
