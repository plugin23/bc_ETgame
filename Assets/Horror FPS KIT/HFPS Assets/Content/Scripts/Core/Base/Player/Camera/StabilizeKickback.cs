using UnityEngine;
using Tobii.Gaming;
using System.Collections;

public class StabilizeKickback : MonoBehaviour
{
    public float returnSpeed = 2.0f;
    public Transform myTransform;
    public Transform mySphere;

    void LateUpdate()
    {
        Quaternion target = Quaternion.Euler(0, 0, 0);

        GazePoint gazePoint = TobiiAPI.GetGazePoint();
        //Vector3 cameraVector = gazePoint;
        //Camera.main.ScreenToWorldPoint(gazePoint)
        Vector3 newGazePoint = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 1);
        Vector3 worldPoint = Camera.main.ScreenToWorldPoint(newGazePoint);
        Vector3 direction = worldPoint - myTransform.position;
        Debug.DrawRay(myTransform.position, direction, Color.red);
        /*if (gazePoint.IsRecent())
        {
            target = Quaternion.LookRotation(direction);
        }
        myTransform.localRotation = Quaternion.Slerp(myTransform.localRotation, target, Time.deltaTime * returnSpeed);*/
        if (Input.GetKeyUp(KeyCode.L))
        {
            Debug.Log(direction);
        }

    }
}