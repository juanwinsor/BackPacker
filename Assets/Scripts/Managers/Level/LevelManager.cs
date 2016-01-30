using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    //TODO: Spawn Initial Level with tile objects, Spawn new tiles as the character moves up, Scroll the tiles down when player jumps
    //get new tiles from the tile 

    public List<Sprite> safeSprites = new List<Sprite>();
    public List<Sprite> slideSprites = new List<Sprite>();
    public List<Sprite> spikeSprites = new List<Sprite>();

    List<GameObject> TileSetList = new List<GameObject>();
    int maxLevelSize = 15;
    int bossTime = 5;
    int currentElevation = 0;
    int nextElevation = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void InitializeLevel()
    {
        for (int i = 0; i < maxLevelSize - bossTime; i++)
        {
            //Get free tile from tilepool.
            //Make all safe tiles for now.
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
}
