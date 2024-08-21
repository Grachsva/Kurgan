using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Buttons
{
    public class ButtonRight : MonoBehaviour, IPointerDownHandler// required interface when using the OnPointerDown method.
    {
        //Do this when the mouse is clicked over the selectable object this script is attached to.
        public void OnPointerDown(PointerEventData eventData)
        {
            Debug.Log(this.gameObject.name + " Was Clicked.");
        }
    }
}