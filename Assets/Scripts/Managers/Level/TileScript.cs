using UnityEngine;
using System.Collections;

public enum TileType { Safe, Slide, Spike }
public enum LaneNumber { Left, LeftCenter, RightCenter, Right }

public class TileScript : MonoBehaviour, IPoolable
{
  public TileType myTileType; //The tile type that was given by the initilization from the manager when setting the sprite.
  public LaneNumber myLaneNumber; //What lane the tile is in (right, center or left)
  public int myElevation = 0; //what elevation the tile is.
  Sprite sprite;

  public IKTargetSet ikTarget;
  private Transform m_PreviousIKTargetParent;

  float spriteScaledWidth;
  float spriteScaledHeight;

  // Use this for initialization
  void Start()
  {

  }

  public void OnDisable()
  {
    if(ikTarget != null )
    {
      ikTarget.gameObject.SetActive(false);
      ikTarget = null;
    }    
  }

  //initialization from Objectpool
  public void Initialize()
  {
    //float spriteScale = 160.0f/256.0f;
    //gameObject.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
  }

  // Update is called once per frame
  void Update()
  {

  }
  
  public void SetNewTile(TileType tileType, LaneNumber laneNumber, int elevation, Sprite theSprite, IKTargetSet ikTargetSet)
  {
    sprite = theSprite;
    myTileType = tileType;
    myLaneNumber = laneNumber;
    myElevation = elevation;
    gameObject.GetComponent<SpriteRenderer>().sprite = sprite;

    spriteScaledWidth = 160.0f / 256.0f * sprite.textureRect.width;
    spriteScaledHeight = spriteScaledWidth;

    gameObject.transform.localScale = new Vector3(1, 1, 1);

    //-- store the object from the pool
    ikTarget = ikTargetSet;
    //-- we set active to enable and reserve the pool object
    ikTarget.gameObject.SetActive(true);
    //-- cache the pool parent so we can set it back when done
    m_PreviousIKTargetParent = ikTarget.transform.parent;
    //-- parent the ik target to the tile
    ikTarget.transform.parent = this.gameObject.transform;
    ikTarget.transform.localPosition = Vector3.zero;

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
