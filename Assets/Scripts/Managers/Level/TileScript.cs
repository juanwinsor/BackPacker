using UnityEngine;
using System.Collections;

enum TileType { Safe, Empty, Spike }

public class TileScript : MonoBehaviour
{
    TileType myTileType; //The tile type that was given by the initilization from the manager when setting the sprite.


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void SetTileType(TileType tileType)
    {
        myTileType = tileType;
        switch (tileType)
        {
            case TileType.Safe:
                //Set Sprite to passable tile
                break;
            case TileType.Empty:
                //Set Sprite to Empty
                break;
            case TileType.Spike:
                //Set Sprite to Spike
                break;
        }//End switch
    }//End SetTileType

}
