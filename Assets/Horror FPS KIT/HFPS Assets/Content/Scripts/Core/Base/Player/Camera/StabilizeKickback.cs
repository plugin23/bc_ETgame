using UnityEngine;
using Tobii.Gaming;
using System.Collections;

public class StabilizeKickback : MonoBehaviour
{
    public GameObject eyeTrackingObject;
    EyeTracking eyeTracking;
    public float returnSpeed = 2.0f;
    public Transform myTransform;

    private void Awake()
    {
        eyeTracking = eyeTrackingObject.GetComponent<EyeTracking>();
    }

    void LateUpdate()
    {

        Vector3 worldPoint = eyeTracking.worldPoint;
        Vector3 direction = worldPoint - myTransform.position;
        Debug.DrawRay(myTransform.position, direction, Color.red);

        myTransform.LookAt(worldPoint);

    }
}