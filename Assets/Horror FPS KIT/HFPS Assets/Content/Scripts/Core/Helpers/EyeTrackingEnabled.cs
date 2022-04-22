using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTrackingEnabled : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool tracking { get; set; }

    void Awake()
    {
        DontDestroyOnLoad(this);
    }



    // Update is called once per frame
    void Update()
    {
        
    }

}
