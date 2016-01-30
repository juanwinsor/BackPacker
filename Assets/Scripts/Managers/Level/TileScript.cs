using UnityEngine;
using System.Collections;

enum TileType { Safe, Empty, Spike }
enum LaneNumber {Right, Center, Left}

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

    void SetNewTile(TileType tileType, LaneNumber laneNumber, int elevation)
    {
        //TODO: Check the tile pool and get one that is available, else create new one? or does it go to manager?
        myTileType = tileType;
        myLaneNumber = laneNumber;
        myElevation = elevation;
        switch (tileType)
        {
            case TileType.Safe:
                //Set Sprite to safe tile
                break;
            case TileType.Empty:
                //Set Sprite to Empty
                break;
            case TileType.Spike:
                //Set Sprite to Spike
                break;
        }//End switch
    }//End SetNewTile

}
