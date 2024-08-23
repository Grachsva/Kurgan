using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceHide : MonoBehaviour
{
    // Размер фрусума
    [SerializeField] private float frustumFieldOfView = 60.0f;  // Поле зрения фрусума
    [SerializeField] private float frustumAspect = 1.0f;        // Соотношение сторон
    [SerializeField] private float frustumNear = 0.3f;          // Ближайшая плоскость
    [SerializeField] private float frustumFar = 10.0f;          // Дальняя плоскость
    [SerializeField]
    private bool isInsideFrustum = false;

    private void OnDrawGizmos()
    {
        // Нарисовать фрумум для наглядности в редакторе
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, Vector3.one);
        Gizmos.DrawFrustum(Vector3.zero, frustumFieldOfView, frustumFar, frustumNear, frustumAspect);
    }

    private void Update()
    {
        // Проверка, находится ли камера внутри фрусума
        isInsideFrustum = CheckIfInsideFrustum();

        // Получение всех компонентов Image, находящихся внутри объекта и его потомков
        Image[] images = GetComponentsInChildren<Image>();

        // Если массив изображений не пустой
        if (images.Length > 0)
        {
            // Установка прозрачности в зависимости от того, находится ли камера внутри фрусума
            float alpha = isInsideFrustum ? 1.0f : 0.0f;

            // Применение новой прозрачности ко всем найденным изображениям
            foreach (Image image in images)
            {
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }

    private bool CheckIfInsideFrustum()
    {
        // Вычисление расстояния от камеры до центра фрусума
        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 localCameraPosition = transform.InverseTransformPoint(cameraPosition);

        // Проверка, находится ли камера внутри фрусума по расстоянию
        if (localCameraPosition.z > frustumNear && localCameraPosition.z < frustumFar)
        {
            float halfHeightAtNear = Mathf.Tan(frustumFieldOfView * 0.5f * Mathf.Deg2Rad) * frustumNear;
            float halfWidthAtNear = halfHeightAtNear * frustumAspect;

            float halfHeightAtFar = Mathf.Tan(frustumFieldOfView * 0.5f * Mathf.Deg2Rad) * frustumFar;
            float halfWidthAtFar = halfHeightAtFar * frustumAspect;

            // Линейная интерполяция размеров для z текущей позиции камеры
            float lerpFactor = (localCameraPosition.z - frustumNear) / (frustumFar - frustumNear);
            float currentHalfHeight = Mathf.Lerp(halfHeightAtNear, halfHeightAtFar, lerpFactor);
            float currentHalfWidth = Mathf.Lerp(halfWidthAtNear, halfWidthAtFar, lerpFactor);

            // Проверка, попадает ли камера в текущие размеры фрусума
            if (Mathf.Abs(localCameraPosition.x) < currentHalfWidth && Mathf.Abs(localCameraPosition.y) < currentHalfHeight)
            {
                return true;
            }
        }

        return false;
    }
}
