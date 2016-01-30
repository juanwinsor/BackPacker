using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameObjectPool : MonoBehaviour 
{
  public string description;
  public GameObject poolPrefab;
  public bool growingPool = false;
  public int poolSize = 5;
  public GameObject parentObject = null;

  private List<GameObject> m_Pool;

  void Awake()
  {
    //-- instantiate the pool
    m_Pool = new List<GameObject>();

    if( poolPrefab != null )
    {
      for( int i = 0; i < poolSize; ++i )
      {
        CreatePoolObject();
      }
    }
  }

  public List<GameObject> GetPoolObjects()
  {
    return m_Pool;
  }

  public GameObject GetPoolObject()
  {
    GameObject result = null;

    //-- loop through the pool and find a free resource
    for( int i = 0; i < m_Pool.Count; ++i )
    {
      if( m_Pool[i].activeInHierarchy == false )
      {
        result = m_Pool[i];
        break;
      }
    }

    //-- if no free resource has been found and we want the pool to grow then create a new pool object
    if (growingPool == true && result == null )
    {
      result = CreatePoolObject();
    }

    return result;
  }

  private GameObject CreatePoolObject()
  {
    //-- create a new gameobject
    GameObject obj = (GameObject)Instantiate( poolPrefab );
    obj.SetActive( false );

    //-- child the pooled objects if there is a parent specified
    if( parentObject != null )
    {
      obj.transform.SetParent( parentObject.transform );
    }

    //-- initialize if it is IPoolable
    IPoolable poolable = obj.GetComponent<IPoolable> ();
    if( poolable != null )
    {
      poolable.Initialize();
    }

    //-- add the created object to the pool
    m_Pool.Add( obj );

    //-- return a reference of the pooled object for use
    return obj;
  }

  public int GetPoolCount()
  {
    //-- return how many allocated objects there are in the pool
    return m_Pool.Count;
  }

  public int GetActivePoolCount()
  {
    //-- count the number of active allocations in the pool
    int result = 0;

    for( int i = 0; i < m_Pool.Count; ++i )
    {
      if( m_Pool[i].activeInHierarchy == true )
      {
        result++;
      }
    }

    return result;
  }
}
