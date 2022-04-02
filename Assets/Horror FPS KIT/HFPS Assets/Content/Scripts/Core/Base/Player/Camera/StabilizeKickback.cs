using UnityEngine;
using Tobii.Gaming;
using System.Collections;

public class StabilizeKickback : MonoBehaviour
{
    public GameObject eyeTrackingObject;
    EyeTracking eyeTracking;
    public float returnSpeed = 2.0f;
    public Transform myTransform;
    public Transform mySphere;

    private void Awake()
    {
        eyeTracking = eyeTrackingObject.GetComponent<EyeTracking>();
        //eyeTracking = GetComponent<EyeTracking>();
    }

    void LateUpdate()
    {
        /*Quaternion target = Quaternion.Euler(0, 0, 0);
        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        Vector3 cameraVector = gazePoint;
        Camera.main.ScreenToWorldPoint(gazePoint)
        Vector3 newGazePoint = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 1);
        Vector3 newGazePoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(newGazePoint);
        Vector3 direction = worldPoint - myTransform.position;*/

        Vector3 worldPoint = eyeTracking.worldPoint;
        Vector3 direction = worldPoint - myTransform.position;
        Debug.DrawRay(myTransform.position, direction, Color.red);
        
        /*if (gazePoint.IsRecent())
        {
            target = Quaternion.LookRotation(direction);
        }
        myTransform.localRotation = Quaternion.Slerp(myTransform.localRotation, target, Time.deltaTime * returnSpeed);*/

        
        //mySphere.position = worldPoint;
        if (Input.GetKeyUp(KeyCode.L))
        {
            Quaternion target = Quaternion.LookRotation(direction);
            myTransform.localRotation = Quaternion.Slerp(myTransform.localRotation, target, Time.deltaTime * returnSpeed);
            //myTransform.position = eyeTracking.viewportPoint;
            mySphere.position = worldPoint;
            Debug.Log(mySphere.position);
        }

    }
}