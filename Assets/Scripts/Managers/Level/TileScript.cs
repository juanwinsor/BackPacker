using UnityEngine;
using System.Collections;

public enum TileType { Safe, Slide, Spike }
public enum LaneNumber {Left, LeftCenter, RightCenter, Right}

public class TileScript : MonoBehaviour
{
    TileType myTileType; //The tile type that was given by the initilization from the manager when setting the sprite.
    public LaneNumber myLaneNumber; //What lane the tile is in (right, center or left)
    int myElevation = 0; //what elevation the tile is.
    Sprite sprite;

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
        sprite = theSprite;
        myTileType = tileType;
        myLaneNumber = laneNumber;
        myElevation = elevation;
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite;
    }//End SetNewTile

    public void setTilePosition(int currElevation)
    {
        float x = 0.0f;
        float y = 0.0f;
        switch (myLaneNumber)
        {
            case LaneNumber.Left:
                x = sprite.textureRect.width / 2;
                break;
            case LaneNumber.LeftCenter:
                x = sprite.textureRect.width + sprite.textureRect.width / 2;
                break;
            case LaneNumber.RightCenter:
                x = sprite.textureRect.width * 2 + sprite.textureRect.width / 2;
                break;
            case LaneNumber.Right:
                x = sprite.textureRect.width * 3 + sprite.textureRect.width / 2;
                break;
        }
        x = x / sprite.pixelsPerUnit;
        gameObject.transform.position = new Vector3(x, y, 0.0f);
    }
}
