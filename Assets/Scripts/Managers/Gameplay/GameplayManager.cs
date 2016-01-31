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

    private PlayerController m_PlayerController;

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
        //-- do level lookup here

    }
    


    public void RegisterPlayerController( PlayerController playerController )
    {
        m_PlayerController = playerController;
    }
}
