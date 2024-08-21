using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Buttons
{
    public class ButtonLeft : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        private Button _buttonLeft;

        private void Start()
        {
            _buttonLeft = GetComponent<Button>();
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