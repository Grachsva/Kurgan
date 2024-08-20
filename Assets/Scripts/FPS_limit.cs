using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
[ExecuteAlways]
public class FPS_limit
{
    static FPS_limit()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        Debug.Log(Application.targetFrameRate);
    }
}