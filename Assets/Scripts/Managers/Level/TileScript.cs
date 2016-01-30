using UnityEngine;
using System.Collections;

public enum TileType { Safe, Slide, Spike }
public enum LaneNumber {Left, LeftCenter, RightCenter, Right}

public class TileScript : MonoBehaviour
{
    TileType myTileType; //The tile type that was given by the initilization from the manager when setting the sprite.
    LaneNumber myLaneNumber; //What lane the tile is in (right, center or left)
    int myElevation = 0; //what elevation the tile is.

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNewTile(TileType tileType, LaneNumber laneNumber, int elevation, Sprite theSprite)
    {
        myTileType = tileType;
        myLaneNumber = laneNumber;
        myElevation = elevation;
        gameObject.GetComponent<SpriteRenderer>().sprite = theSprite;
    }//End SetNewTile

}
