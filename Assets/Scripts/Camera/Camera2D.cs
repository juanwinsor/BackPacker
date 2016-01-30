using UnityEngine;
using System.Collections;

public class Camera2D : MonoBehaviour 
{
    public bool scaleToHeight = true;

	public float pixelsPerUnit = 100.0f;
	public float zoom = 1.0f;

	public bool updateSizeAutomatically = false;
	public bool useCustomHeight = true;
	public float screenHeight = 480.0f;

    public bool useCustomWidth = true;
    public float screenWidth = 640.0f;

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
        if (scaleToHeight)
        {
            if (m_Camera != null)
            {
                //-- if we are not specifying a manual screen height then use the actual screen height
                if (useCustomHeight == false)
                {
                    screenHeight = Screen.height;
                }

                //-- initial camera zoom 1:1 pixel/unit ratio
                m_Camera.orthographicSize = (screenHeight / pixelsPerUnit * 0.5f) * zoom;
            }
        }
        else
        {
            if (m_Camera != null)
            {
                //-- if we are not specifying a manual screen height then use the actual screen height
                if (useCustomWidth == false)
                {
                    screenWidth = Screen.width;
                }

                float screenAspectRatio = m_Camera.aspect;
                float heightOfScreen = screenWidth / screenAspectRatio;
                //-- initial camera zoom 1:1 pixel/unit ratio
                m_Camera.orthographicSize = (heightOfScreen / pixelsPerUnit * 0.5f) * zoom;

                //Setting screen position (offset)
                m_Camera.transform.position = new Vector3(screenWidth / 2 / pixelsPerUnit, heightOfScreen / 2 / pixelsPerUnit, -10.0f);
            }
        }
	}
}
