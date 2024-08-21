using UnityEngine.EventSystems;
using UnityEngine;

namespace Buttons
{
    public class ButtonLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private CameraMovement _cameraMovement;
        private bool _isPressed = false;

        private void Start()
        {
            _cameraMovement = FindObjectOfType<CameraMovement>();
        }

        private void Update()
        {
            if (_isPressed)
            {
                _cameraMovement.MoveToPreviousPoint();  // Перемещаем камеру вперед
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = true;
            Debug.Log(this.gameObject.name + " Was Clicked.");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isPressed = false;
            Debug.Log(this.gameObject.name + " Was Unclicked.");
        }
    }
}