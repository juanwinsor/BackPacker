using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour 
{
	public float pixelsPerUnit = 100.0f;
	public float zoom = 1.0f;

	public bool updateSizeAutomatically = false;
	public bool useCustomHeight = true;
	public float screenHeight = 480.0f;

  public GameObject followObject = null;
  public float followOffsetX = 0.0f;

	private Camera m_Camera = null;

	// Use this for initialization
	void Start () 
	{
		m_Camera = GetComponent<Camera>();
		calculateOrthoSize();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if( updateSizeAutomatically == true )
		{
			calculateOrthoSize();
		}

    if (followObject != null)
    {
      transform.localPosition = new Vector3(followObject.transform.position.x + followOffsetX, transform.localPosition.y, transform.localPosition.z);
    }

	}

	void calculateOrthoSize()
	{
		if (m_Camera != null) 
		{
			//-- if we are not specifying a manual screen height then use the actual screen height
			if (useCustomHeight == false) 
			{
				screenHeight = Screen.height;
			}
			
			//-- initial camera zoom 1:1 pixel/unit ratio
			m_Camera.orthographicSize = (screenHeight / pixelsPerUnit * 0.5f ) * zoom;
		}
	}
}
