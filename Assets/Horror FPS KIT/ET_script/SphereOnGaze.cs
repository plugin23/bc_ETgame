using System.Collections;
using System.Collections.Generic;
using Tobii.Gaming;
using UnityEngine;

public class SphereOnGaze : MonoBehaviour
{
    public Transform mySphere;
    public Transform mouseSphere;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        //Vector3 cameraVector = gazePoint;
        //Camera.main.ScreenToWorldPoint(gazePoint)
        //Vector3 newGazePoint = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 1);
        Vector3 newGazePoint = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 1);
        Vector3 mouseGazePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(newGazePoint);
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(mouseGazePoint);
        //Vector3 direction = worldPoint - myTransform.position;
        //Debug.DrawRay(.position, direction, Color.red);

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
