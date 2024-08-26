using UnityEngine;

public class StateMachineButtons : MonoBehaviour
{
    public States currentState;

    void Update()
    {
        switch (currentState)
        {
            case States.WithoutMarkers:
                Debug.Log("WithoutMarkers is enabled");
                Debug.Log("������������� ������");
                break;

            case States.WithMarkers:
                Debug.Log("WithMarkers is enabled");
                Debug.Log("������������� ������");
                Debug.Log("������������� ������");
                break;

            case States.Slider:
                Debug.Log("VideoPlayer is enabled");
                break;

            case States.VideoPlayer:
                Debug.Log("VideoPlayer is enabled");
                break;
        }
    }
}

public enum States
{
    WithoutMarkers,
    WithMarkers,
    Slider,
    VideoPlayer,
}