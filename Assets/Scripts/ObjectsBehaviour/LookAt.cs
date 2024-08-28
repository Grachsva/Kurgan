using Snapshots;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class LookAt : MonoBehaviour
{

    private bool isConfig;

    private void Start()
    {
        ButtonConfig.e_OnButtonConfig += EnableConfigScene;
    }

    private void EnableConfigScene()
    {
        isConfig = !isConfig;
    }

    private void Update()
    {
        //if (isConfig) 
            transform.LookAt(-Camera.main.transform.position);
        //transform.Rotate(-Camera.main.transform.position);
    }
}