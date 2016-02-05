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

  public float playerMoveTime = 0.5f;
  public float playerIKTransitionTime = 0.5f;
  private float m_TileSizeScaled = 0.0f;

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

  void StartLevel()
  {
    //-- set the characters current tile
    m_CurrentTile = levelManager.GetTile(MoveDirection.MoveLeft, LaneNumber.LeftCenter, 0);
    m_TileSizeScaled = levelManager.spriteScaledSize;

    //-- pass the scaled tile size as the move distance
    m_PlayerController.SetMoveDistance(m_TileSizeScaled);

    //-- set the character position
    m_PlayerController.transform.position = m_CurrentTile.transform.position;
    m_PlayerController.SetCurrentIKTarget(m_CurrentTile.ikTarget);
    m_PlayerController.ApplyIKTarget(m_CurrentTile.ikTarget);
  }

  public void RequestMove(MoveDirection direction)
  {
    //-- get the tile in the direction
    TileScript nextTile = levelManager.GetTile(direction, m_CurrentTile.myLaneNumber, m_CurrentTile.myElevation);
    //if( nextTile.myTileType != TileType.Blocker )
    if (nextTile != null)
    {
      if (direction == MoveDirection.MoveUp)
      {
        m_PlayerController.SetPlayerState(PlayerState.Hop_Up);
        levelManager.MoveTiles(MoveDirection.MoveUp, playerMoveTime);
      }
      if (direction == MoveDirection.MoveDown)
      {
        m_PlayerController.SetPlayerState(PlayerState.Hop_Down);
        levelManager.MoveTiles(MoveDirection.MoveDown, playerMoveTime);
      }
      if (direction == MoveDirection.MoveLeft)
      {
        m_PlayerController.SetPlayerState(PlayerState.Hop_Left);
      }
      if (direction == MoveDirection.MoveRight)
      {
        m_PlayerController.SetPlayerState(PlayerState.Hop_Right);
      }

      //-- let the player controller know we are preparing to switch ik targets
      m_PlayerController.SetNextIKSet(nextTile.ikTarget);

      //-- cache the current tile
      m_CurrentTile = nextTile;

    }
  }



  public void RegisterPlayerController(PlayerController playerController)
  {
    m_PlayerController = playerController;
    //-- set the variable for the time it takes the player to hop to a tile
    m_PlayerController.SetMoveTime(playerMoveTime);
    m_PlayerController.SetIKTransitionTime(playerIKTransitionTime);

    StartLevel();
  }
}
