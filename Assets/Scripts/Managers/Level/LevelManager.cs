using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
  //TODO: Spawn Initial Level with tile objects, Spawn new tiles as the character moves up, Scroll the tiles down when player jumps
  //get new tiles from the tile 
  public GameObjectPool theDeadPool;

  public List<Sprite> safeSprites = new List<Sprite>();
  public List<Sprite> slideSprites = new List<Sprite>();
  public List<Sprite> spikeSprites = new List<Sprite>();

  public List<GameObjectPool> ikTargetPools = new List<GameObjectPool>();

  public float spriteScaledSize = 0;

  public List<TileScript> tileSetList = new List<TileScript>();
  int maxLevelSize = 15;
  int bossTime = 5;
  int currentElevation = 0;
  int nextElevation = 0;
  int lastElevation = 0;
  float spriteScale = 160.0f / 256.0f;

  // Use this for initialization
  void Start()
  {
    gameObject.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
    spriteScaledSize = 160.0f / 256.0f * safeSprites[0].textureRect.width;
    spriteScaledSize /= safeSprites[0].pixelsPerUnit;
    InitializeLevel();
  }

  // Update is called once per frame
  void Update()
  {

  }

  void InitializeLevel()
  {
    IKTargetSet ikTargetScript;
    Sprite spriteToUse;
    TileScript tileScriptToUse;
    for (int i = 0; i < maxLevelSize - bossTime; i++)
    {
      for (int j = 0; j < 4; j++)
      {
        int tileIndex = Random.Range(0, safeSprites.Count);
        spriteToUse = safeSprites[tileIndex];//Gets a random sprite of safe type
        ikTargetScript = GetIKTargetObject(tileIndex);//use the same random number of the sprite to match the ik target
        tileScriptToUse = theDeadPool.GetPoolObject().GetComponent<TileScript>(); //gets the TileScript from the tile prefab
        switch (j)
        {
          case 0:
            tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.Left, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
            break;
          case 1:
            tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.LeftCenter, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
            break;
          case 2:
            tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.RightCenter, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
            break;
          case 3:
            tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.Right, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
            break;
        }//end switch
        tileScriptToUse.setTilePosition(currentElevation); //Sets the new tiles position
        tileScriptToUse.gameObject.SetActive(true);//Set the tile to be active
        tileSetList.Add(tileScriptToUse); //add the tile to the active list
      }
      nextElevation++;
    }
  }

  IKTargetSet GetIKTargetObject(int tileIndex)
  {
    GameObject ikTarget = ikTargetPools[tileIndex].GetPoolObject();
    IKTargetSet targetScript = ikTarget.GetComponent<IKTargetSet>();

    return targetScript;
  }

  void SpawnRow()
  {
    if (currentElevation - lastElevation > maxLevelSize)
    {
      //RemoveRow();
    }

    IKTargetSet ikTargetScript;
    Sprite spriteToUse;
    TileScript tileScriptToUse;
    for (int j = 0; j < 4; j++)
    {
      int tileIndex = Random.Range(0, safeSprites.Count);
      spriteToUse = safeSprites[tileIndex];//Gets a random sprite of safe type
      ikTargetScript = GetIKTargetObject(tileIndex);//use the same random number of the sprite to match the ik target
      tileScriptToUse = theDeadPool.GetPoolObject().GetComponent<TileScript>(); //gets the TileScript from the tile prefab
      switch (j)
      {
        case 0:
          tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.Left, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
          break;
        case 1:
          tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.LeftCenter, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
          break;
        case 2:
          tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.RightCenter, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
          break;
        case 3:
          tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.Right, nextElevation, spriteToUse, ikTargetScript); //Sets the new tile's properties
          break;
      }//end switch
      tileScriptToUse.setTilePosition(currentElevation); //Sets the new tiles position
      tileScriptToUse.gameObject.SetActive(true);//Set the tile to be active
      tileSetList.Add(tileScriptToUse); //add the tile to the active list
    }
    nextElevation++;
  }

  void RemoveRow()
  {

    for (int i = tileSetList.Count - 1; i >= 0; --i)
    {
      if (tileSetList[i].myElevation == lastElevation)
      {
        tileSetList[i].gameObject.SetActive(false);
        tileSetList.Remove(tileSetList[i]);
      }
    }
    lastElevation++;
  }

  public void MoveTiles(MoveDirection direction, float moveTime)
  {
    Vector3 newPosition = Vector3.zero;
    if (MoveDirection.MoveUp == direction)
    {
      for (int i = 0; i < tileSetList.Count; i++)
      {
        newPosition = new Vector3(tileSetList[i].gameObject.transform.position.x, tileSetList[i].gameObject.transform.position.y - spriteScaledSize, tileSetList[i].gameObject.transform.position.z);
        Hashtable hash = iTween.Hash("position", newPosition, "time", moveTime, "easetype", iTween.EaseType.easeOutElastic);
        iTween.MoveTo(tileSetList[i].gameObject, hash);
      }
      currentElevation++;
      if (nextElevation - currentElevation < maxLevelSize - bossTime)
      {
        SpawnRow();
      }
    }
    else if (MoveDirection.MoveDown == direction)
    {
      for (int i = 0; i < tileSetList.Count; i++)
      {
        //MoveTo with Itween
        newPosition = new Vector3(tileSetList[i].gameObject.transform.position.x, tileSetList[i].gameObject.transform.position.y + spriteScaledSize, tileSetList[i].gameObject.transform.position.z);
        Hashtable hash = iTween.Hash("position", newPosition, "time", moveTime, "easetype", iTween.EaseType.easeOutElastic);
        iTween.MoveTo(tileSetList[i].gameObject, hash);
      }
      currentElevation--;
    }
  }


  public TileScript GetTile(MoveDirection direction, LaneNumber lane, int elevation)
  {

    switch (direction)
    {
      case MoveDirection.MoveUp:
        for (int i = 0; i < tileSetList.Count; i++)
        {
          if (tileSetList[i].myLaneNumber == lane && tileSetList[i].myElevation == elevation + 1)
          {
            return tileSetList[i];
          }
        }
        break;
      case MoveDirection.MoveDown:
        for (int i = 0; i < tileSetList.Count; i++)
        {
          if (tileSetList[i].myLaneNumber == lane && tileSetList[i].myElevation == elevation - 1)
          {
            return tileSetList[i];
          }
        }
        break;
      case MoveDirection.MoveLeft:
        if (LaneNumber.Left == lane)
        {
          return null;
        }

        for (int i = 0; i < tileSetList.Count; i++)
        {
          if (tileSetList[i].myLaneNumber == lane - 1 && tileSetList[i].myElevation == elevation)
          {
            return tileSetList[i];
          }
        }
        break;
      case MoveDirection.MoveRight:
        if (LaneNumber.Right == lane)
        {
          return null;
        }

        for (int i = 0; i < tileSetList.Count; i++)
        {
          if (tileSetList[i].myLaneNumber == lane + 1 && tileSetList[i].myElevation == elevation)
          {
            return tileSetList[i];
          }
        }
        break;
    }
    return null;
  }

}
