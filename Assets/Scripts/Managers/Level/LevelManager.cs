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



    List<TileScript> TileSetList = new List<TileScript>();
    int maxLevelSize = 15;
    int bossTime = 5;
    int currentElevation = 0;
    int nextElevation = 0;
    float spriteScale = 160.0f / 256.0f;

    // Use this for initialization
    void Start()
    {
        gameObject.transform.localScale = new Vector3(spriteScale, spriteScale, spriteScale);
        InitializeLevel();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeLevel()
    {
        Sprite spriteToUse;
        TileScript tileScriptToUse;
        for (int i = 0; i < maxLevelSize - bossTime; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                spriteToUse = safeSprites[Random.Range(0, safeSprites.Count)];//Gets a random sprite of safe type
                tileScriptToUse = theDeadPool.GetPoolObject().GetComponent<TileScript>(); //gets the TileScript from the tile prefab
                switch (j)
                {
                    case 0:
                        tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.Left, nextElevation, spriteToUse); //Sets the new tile's properties
                        break;
                    case 1:
                        tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.LeftCenter, nextElevation, spriteToUse); //Sets the new tile's properties
                        break;
                    case 2:
                        tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.RightCenter, nextElevation, spriteToUse); //Sets the new tile's properties
                        break;
                    case 3:
                        tileScriptToUse.SetNewTile(TileType.Safe, LaneNumber.Right, nextElevation, spriteToUse); //Sets the new tile's properties
                        break;
                }//end switch
                tileScriptToUse.setTilePosition(currentElevation); //Sets the new tiles position
                tileScriptToUse.gameObject.SetActive(true);//Set the tile to be active
                TileSetList.Add(tileScriptToUse); //add the tile to the active list
            }
            nextElevation++;
        }
    }

    void SpawnRow()
    {
        //Get free tiles from tilepool and set to nextElevation
        nextElevation++;
    }

    void GoUp()
    {
        currentElevation++;
        //Shift all tiles down
        SpawnRow();
    }

    void GoDown()
    {
        currentElevation--;
        //shift all tiles up
    }

    TileScript GetTile(MoveDirection direction, LaneNumber lane, int elevation)
    {
        
        switch (direction)
        {
            case MoveDirection.MoveUp:
                for (int i = 0; i < TileSetList.Count; i++)
                {
                  //  if()
                }
                    break;
            case MoveDirection.MoveDown:
                break;
            case MoveDirection.MoveLeft:
                if (LaneNumber.Left == lane)
                {
                    return null;
                }
                break;
            case MoveDirection.MoveRight:
                if (LaneNumber.Right == lane)
                {
                    return null;
                }
                break;
        }
        return null;
    }


}
