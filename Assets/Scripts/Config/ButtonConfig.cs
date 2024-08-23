using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Snapshots
{
    public class ButtonConfig : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        public delegate void OnButtonConfigHandler();
        public static event OnButtonConfigHandler e_OnButtonConfig;

        public float holdTime = 2.0f; // Время удержания
        private float timer = 0.0f;
        private bool isHolding = false;
        private bool holdTriggered = false;

        // Метод, который будет вызван при удержании
        public void EnableConfigScene()
        {
            Debug.Log("Method called after holding the button for 2 seconds");
            // Ваш метод переключения камер и UI
        }

        void Update()
        {
            if (isHolding)
            {
                timer += Time.deltaTime;
                if (timer >= holdTime && !holdTriggered)
                {
                    Debug.Log("Button config event");
                    holdTriggered = true;
                    e_OnButtonConfig?.Invoke();
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            // Начинаем отслеживать удержание
            isHolding = true;
            timer = 0.0f;
            holdTriggered = false;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            // Прекращаем отслеживание удержания
            isHolding = false;
        }
    }
}