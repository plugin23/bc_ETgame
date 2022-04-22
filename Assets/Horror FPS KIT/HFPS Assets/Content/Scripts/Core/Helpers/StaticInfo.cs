using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StaticInfo : MonoBehaviour
{
    // Start is called before the first frame update

    public static bool tracking { get; set; }
    public static DateTime levelStart { get; set; }
    public static DateTime levelEnd { get; set; }
    public static FileStream timeFile { get; set; }
    public static StreamWriter sw { get; set; }

    void Awake()
    {
        timeFile = null;
        sw = null;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void setFile()
    {
        if (timeFile == null)
        {
            timeFile = new FileStream(@"Assets\Time_Stamp.txt", FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(timeFile);
            sw.AutoFlush = true;
        }
            
    }

    public static TimeSpan getDuration()
    {
        levelEnd = DateTime.Now;
        return levelEnd - levelStart;
    }
}
