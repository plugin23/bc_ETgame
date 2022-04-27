using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class SphereOnGaze : MonoBehaviour
{
    public Transform mySphere;
    public Transform mouseSphere;

    private float positionX = Screen.width / 2;
    private float positionY = Screen.height / 2;
    private float positionZ = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GazePoint gazePoint = TobiiAPI.GetGazePoint();
        //Vector3 cameraVector = gazePoint;
        //Camera.main.ScreenToWorldPoint(gazePoint)
        //Vector3 newGazePoint = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 1);
        //Vector3 newGazePoint = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 1);

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

        Vector3 SimulatedGazePoint = new Vector3(positionX, positionY, positionZ);
        Vector3 mouseGazePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(SimulatedGazePoint);
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(mouseGazePoint);
        //Vector3 direction = worldPoint - myTransform.position;
        //Debug.DrawRay(.position, direction, Color.red);
        mySphere.position = worldPoint;
        if (Input.GetKeyUp(KeyCode.L))
        {
            mySphere.position = worldPoint;
            mouseSphere.position = mousePoint; 
            Debug.Log(worldPoint);
            Debug.Log(mousePoint);
            //Debug.Log(direction);
        }
    }
}
