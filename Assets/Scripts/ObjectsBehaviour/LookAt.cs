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
        //transform.LookAt(-Camera.main.transform.position);
        // Получаем позицию камеры
        Vector3 cameraPosition = Camera.main.transform.position;
        // Направление от объекта к камере
        Vector3 directionToCamera = cameraPosition - transform.position;
        // Поворачиваем направление на 180 градусов
        Vector3 directionAwayFromCamera = -directionToCamera;
        // Устанавливаем направление взгляда объекта
        transform.LookAt(transform.position + directionAwayFromCamera);
        //transform.Rotate(-Camera.main.transform.position);
    }
}