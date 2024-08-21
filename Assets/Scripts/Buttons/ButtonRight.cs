using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Buttons
{
    public class ButtonRight : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Button _buttonRight;

        private void Start()
        {
            _buttonRight = GetComponent<Button>();
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(this.gameObject.name + " Was Clicked.");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            Debug.Log(this.gameObject.name + " Was Unclicked.");
        }

    }
}