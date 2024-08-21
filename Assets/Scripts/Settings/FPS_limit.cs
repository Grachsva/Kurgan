using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class FPS_limit : MonoBehaviour 
{
    private void Awake()
    {
        QualitySettings.vSyncCount = 1;
        Application.targetFrameRate = 60;
        Debug.Log(Application.targetFrameRate);
    }
}