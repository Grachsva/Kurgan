using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace StateMachine
{
    [ExecuteAlways]
    public class StateMachineButtons : MonoBehaviour
    {
        private Dictionary<Image, Sprite> SpritesForImagesEnabled = new Dictionary<Image, Sprite>();
        private Dictionary<Image, Sprite> SpritesForImagesDisabled = new Dictionary<Image, Sprite>();

        [SerializeField] private GameObject _markers;

        [SerializeField] private Button _buttonArrowLeft;
        [SerializeField] private Button _buttonSlider;
        [SerializeField] private Button _buttonVideoPlayer;
        [SerializeField] private Button _buttonArrowRight;

        //--------------------------------------------- Enabled Sprites
        [SerializeField] private Sprite _arrowLeft;
        [SerializeField] private Sprite _info;
        [SerializeField] private Sprite _infoSelected; // Желтая кнопка Info
        [SerializeField] private Sprite _play;
        [SerializeField] private Sprite _arrowRight;

        [SerializeField] private Sprite _close; // Спрайт крестика

        //--------------------------------------------- Disabled Sprites
        [SerializeField] private Sprite _arrowLeftDis;
        [SerializeField] private Sprite _infoDis;
        [SerializeField] private Sprite _playDis;
        [SerializeField] private Sprite _arrowRightDis;

        public States _currentState = States.WithoutMarkers;

        private void Start()
        {
            // Enabled sprites
            SpritesForImagesEnabled.Add(_buttonArrowLeft.GetComponent<Image>(), _arrowLeft);
            SpritesForImagesEnabled.Add(_buttonSlider.GetComponent<Image>(), _info);
            SpritesForImagesEnabled.Add(_buttonVideoPlayer.GetComponent<Image>(), _play);
            SpritesForImagesEnabled.Add(_buttonArrowRight.GetComponent<Image>(), _arrowRight);

            // Disabled sprites
            SpritesForImagesDisabled.Add(_buttonArrowLeft.GetComponent<Image>(), _arrowLeftDis);
            SpritesForImagesDisabled.Add(_buttonSlider.GetComponent<Image>(), _infoDis);
            SpritesForImagesDisabled.Add(_buttonVideoPlayer.GetComponent<Image>(), _playDis);
            SpritesForImagesDisabled.Add(_buttonArrowRight.GetComponent<Image>(), _arrowRightDis);

            // Устанавливаем начальное состояние
            ChangeState(_currentState);
        }

        public void ChangeState(States state)
        {
            _currentState = state;

            switch (state)
            {
                case States.WithoutMarkers:
                    ActivateButtons(SpritesForImagesEnabled);
                    MarkersVisible(false);
                    break;

                case States.WithMarkers:
                    ActivateButtons(SpritesForImagesEnabled);
                    _buttonSlider.GetComponent<Image>().sprite = _infoSelected; // Желтая кнопка Info
                    MarkersVisible(true);
                    break;

                case States.Slider:
                    ActivateButtons(SpritesForImagesDisabled);
                    _buttonSlider.GetComponent<Image>().sprite = _close; // Кнопка крестик вместо Info
                    break;

                case States.VideoPlayer:
                    ActivateButtons(SpritesForImagesDisabled);
                    _buttonVideoPlayer.GetComponent<Image>().sprite = _close; // Кнопка крестик вместо Play
                    break;
            }
        }

        private void ActivateButtons(Dictionary<Image, Sprite> sprites)
        {
            foreach (var button in sprites.Keys)
            {
                button.sprite = sprites[button];
            }
        }

        private void MarkersVisible(bool Bool)
        {
            _markers.SetActive(Bool);
        }
    }

    public enum States
    {
        WithoutMarkers,
        WithMarkers,
        Slider,
        VideoPlayer,
    }
}
