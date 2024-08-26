using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
//[InitializeOnLoad]
public class StateMachineButtons : MonoBehaviour
{
    Dictionary<Image, Sprite> SpritesForImagesEnabled = new Dictionary<Image, Sprite> ();
    Dictionary<Image, Sprite> SpritesForImagesDisabled = new Dictionary<Image, Sprite> ();

    public States currentState;

    [SerializeField] private GameObject _markers;

    [SerializeField] private Button _buttonArrowLeft;
    [SerializeField] private Button _buttonArrowRight;

    [SerializeField] private Button _buttonSlider;
    [SerializeField] private Button _buttonVideoPlayer;
    //--------------------------------------------- Enable
    [SerializeField] private Sprite _arrowLeft;
    [SerializeField] private Sprite _arrowRight;

    [SerializeField] private Sprite _info;
    [SerializeField] private Sprite _play;

    [SerializeField] private Sprite _close;
    //--------------------------------------------- Disable
    [SerializeField] private Sprite _arrowLeftDis;
    [SerializeField] private Sprite _arrowRightDis;

    [SerializeField] private Sprite _infoDis;
    [SerializeField] private Sprite _playDis;

    //List<Image> _imagesInMainButtons = new List<Image>();

    private void Start()
    {
        //_imagesInMainButtons.Add(); // 0
        //_imagesInMainButtons.Add(_buttonVideoPlayer.GetComponent<Image>()); // 1
        //_imagesInMainButtons.Add(_buttonArrowLeft.GetComponent<Image>()); // 2
        //_imagesInMainButtons.Add(_buttonArrowRight.GetComponent<Image>()); // 3

        //--------------------------------------------- Enable
        SpritesForImagesEnabled.Add(_buttonSlider.GetComponent<Image>(), _info); // 0
        SpritesForImagesEnabled.Add(_buttonVideoPlayer.GetComponent<Image>(), _play); // 1

        SpritesForImagesEnabled.Add(_buttonArrowLeft.GetComponent<Image>(), _arrowLeft); // 2
        SpritesForImagesEnabled.Add(_buttonArrowRight.GetComponent<Image>(), _arrowRight); // 3
        //--------------------------------------------- Disable
        SpritesForImagesDisabled.Add(_buttonSlider.GetComponent<Image>(), _arrowLeftDis); // 0
        SpritesForImagesDisabled.Add(_buttonVideoPlayer.GetComponent<Image>(), _arrowRightDis); // 1

        SpritesForImagesDisabled.Add(_buttonArrowLeft.GetComponent<Image>(), _infoDis); // 2
        SpritesForImagesDisabled.Add(_buttonArrowRight.GetComponent<Image>(), _playDis); // 3
    }

    public void ChangeState(States state)
    {
        switch (SpritesForImagesEnabled.)
        {
            case States.WithoutMarkers:
                Debug.Log("WithoutMarkers is enabled");
                //Debug.Log("Дизактивируем кнопки");
                DisactivateButtons();
                break;

            case States.WithMarkers:
                Debug.Log("WithMarkers is enabled");
                //Debug.Log("Дизактивируем кнопки");
                //Debug.Log("Делаем кнопку меток активной");
                //Debug.Log("Показываем метки на карте");
                break;

            case States.Slider:
                Debug.Log("VideoPlayer is enabled");
                //Debug.Log("Дизактивируем кнопки");
                //Debug.Log("Делаем кнопку слайдер активной (крестик)");
                break;

            case States.VideoPlayer:
                Debug.Log("VideoPlayer is enabled");
                //Debug.Log("Дизактивируем кнопки");
                //Debug.Log("Делаем кнопку видео активной (крестик)");
                break;
        }
    }

    private void DisactivateButtons()
    {
        foreach (var button in SpritesForImagesEnabled.Keys)
        {
            button.sprite = SpritesForImagesEnabled[button];
        }
    }

    private void ChangeSpriteOnButtons(Sprite sprite, Button targetButton)
    {

    }

    private void MarkersVisible()
    {
        _markers.SetActive(!_markers.activeInHierarchy);
    }
}

public enum States
{
    WithoutMarkers,
    WithMarkers,
    Slider,
    VideoPlayer,
}