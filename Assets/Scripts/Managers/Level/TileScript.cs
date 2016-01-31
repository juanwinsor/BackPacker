using UnityEngine;
using System.Collections;

public enum TileType { Safe, Slide, Spike }
public enum LaneNumber {Left, LeftCenter, RightCenter, Right}

public class TileScript : MonoBehaviour, IPoolable
{
    public TileType myTileType; //The tile type that was given by the initilization from the manager when setting the sprite.
    public LaneNumber myLaneNumber; //What lane the tile is in (right, center or left)
    public int myElevation = 0; //what elevation the tile is.
    Sprite sprite;

    float spriteScaledWidth;
    float spriteScaledHeight;

    // Use this for initialization
    void Start()
    {
        
    }

    //initialization from Objectpool
    public void Initialize ()
    {
        //float spriteScale = 160.0f/256.0f;
        //gameObject.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
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

        spriteScaledWidth = 160.0f / 256.0f * sprite.textureRect.width;
        spriteScaledHeight = spriteScaledWidth;

        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }//End SetNewTile

    public void setTilePosition(int currElevation)
    {
        float x = 0.0f;
        float y = 0.0f;
        switch (myLaneNumber)
        {
            case LaneNumber.Left:
                x = spriteScaledWidth / 2;
                break;
            case LaneNumber.LeftCenter:
                x = spriteScaledWidth + spriteScaledWidth / 2;
                break;
            case LaneNumber.RightCenter:
                x = spriteScaledWidth * 2 + spriteScaledWidth / 2;
                break;
            case LaneNumber.Right:
                x = spriteScaledWidth * 3 + spriteScaledWidth / 2;
                break;
        }
        y = (spriteScaledHeight * (myElevation - currElevation) + spriteScaledHeight / 2) / sprite.pixelsPerUnit;
        x = x / sprite.pixelsPerUnit;
        gameObject.transform.position = new Vector3(x, y, 0.0f);
    }
}
