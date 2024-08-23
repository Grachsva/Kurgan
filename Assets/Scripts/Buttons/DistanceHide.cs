using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceHide : MonoBehaviour
{
    // Коэффициенты прозрачности
    [SerializeField] private float minTransparency = 0.0f; // Минимальная прозрачность
    [SerializeField] private float maxTransparency = 1.0f; // Максимальная прозрачность

    [SerializeField] private float dist;
    // Дистанция
    [SerializeField] private float minDistance = 5.0f;  // Расстояние, при котором используется минимальная прозрачность
    [SerializeField] private float maxDistance = 20.0f; // Расстояние, при котором используется максимальная прозрачность
    float alpha;

    private void Update()
    {
        // Вычисление расстояния от камеры до объекта
        dist = Vector3.Distance(Camera.main.transform.position, transform.position);
        print("Distance to other: " + dist);

        // Получение всех компонентов Image, находящихся внутри объекта и его потомков
        Image[] images = GetComponentsInChildren<Image>();

        // Если массив изображений не пустой
        if (images.Length > 0)
        {
            // Вычисление прозрачности на основе расстояния и настроек
            //float t = Mathf.InverseLerp(minDistance, maxDistance, dist); // Нормализация расстояния между minDistance и maxDistance
            //float alpha = Mathf.Lerp(minTransparency, maxTransparency, t); // Интерполяция прозрачности

            // Ограничение значения alpha, чтобы за пределами диапазона прозрачность оставалась постоянной
            if (dist < minDistance)
            {
                alpha = maxTransparency;
            }
            else if (dist > maxDistance)
            {
                alpha = minTransparency; 
            }

            // Применение новой прозрачности ко всем найденным изображениям
            foreach (Image image in images)
            {
                Color color = image.color;
                color.a = alpha;
                image.color = color;
            }
        }
    }
}
