using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class EyeTracking : MonoBehaviour
{

    public bool eyeTracking = false;
    public Vector3 worldPoint;
    public Vector3 viewportPoint;

    private float positionX, positionY, positionZ; 

    // Start is called before the first frame update
    void Start()
    {
        positionX = Screen.width / 2;
        positionY = Screen.height / 2;
        positionZ = 1;
        Vector3 SimulatedGazePoint = new Vector3(positionX, positionY, positionZ);
        worldPoint = Camera.main.ScreenToWorldPoint(SimulatedGazePoint);
        viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);
    }

    // Update is called once per frame
    void Update()
    {
        if (!eyeTracking)
        {
            if (Input.GetKeyUp(KeyCode.UpArrow))
            {
                positionY += 10;
            }
            if (Input.GetKeyUp(KeyCode.DownArrow)) 
            {
                positionY -= 10;
            }
            if (Input.GetKeyUp(KeyCode.LeftArrow)) 
            {
                positionX -= 10;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                positionX += 10;
            }

            Vector3 SimulatedGazePoint = new Vector3(positionX, positionY, 1);
            worldPoint = Camera.main.ScreenToWorldPoint(SimulatedGazePoint);
            viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);
        }
        else
        {
            GazePoint gazePoint = TobiiAPI.GetGazePoint();
            Vector3 eyetrackingGazePoint = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 1);
            worldPoint = Camera.main.ScreenToWorldPoint(eyetrackingGazePoint);
            viewportPoint = Camera.main.WorldToViewportPoint(worldPoint);
        }

    }
}
