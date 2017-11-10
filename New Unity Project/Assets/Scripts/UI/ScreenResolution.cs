using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*For Screen resolution, if the canvas render mode is under  Screen Space - Camera, then we do not have access to canvas' value.
*/
public class ScreenResolution : MonoBehaviour
{
    public Camera mCamera;
    private float baseResolutionWidth = 960;
    private float baseResolutionHeight = 540;
    private float widthRatio;
    private float heightRatio;
    void Awake()
    {

        heightRatio = Screen.height / baseResolutionHeight;
        Debug.Log("HELLO FROM BACKGROUND IMGAE");
        mCamera.orthographicSize = mCamera.orthographicSize * heightRatio;

    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
