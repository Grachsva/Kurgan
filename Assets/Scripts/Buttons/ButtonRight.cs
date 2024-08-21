using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons
{
    public class ButtonRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
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
                _cameraMovement.MoveToNextPoint();  // Перемещаем камеру вперед
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