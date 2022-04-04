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
    }

    void LateUpdate()
    {

        Vector3 worldPoint = eyeTracking.worldPoint;
        Vector3 direction = worldPoint - myTransform.position;
        Debug.DrawRay(myTransform.position, direction, Color.red);

        /*if (gazePoint.IsRecent())
        {
            target = Quaternion.LookRotation(direction);
        }
        myTransform.localRotation = Quaternion.Slerp(myTransform.localRotation, target, Time.deltaTime * returnSpeed);*/

        myTransform.LookAt(worldPoint);
        if (Input.GetKeyUp(KeyCode.L))
        {
            mySphere.position = worldPoint;
            Debug.Log(mySphere.position);
        }

    }
}