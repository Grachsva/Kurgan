using Buttons;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace StateMachine
{
    [ExecuteAlways]
    public class StateMachineButtons : MonoBehaviour
    {
        private Dictionary<Image, Sprite> SpritesForImagesEnabled = new Dictionary<Image, Sprite>();
        private Dictionary<Image, Sprite> SpritesForImagesDisabled = new Dictionary<Image, Sprite>();

        //[SerializeField] private float timerDurationForSliderWithoutText = 45.0f; // Продолжительность таймера в секундах
        //[SerializeField] private float timerDurationForSliderVideo = 180.0f; // Продолжительность таймера в секундах
        [SerializeField] private float timeRemaining; // Оставшееся время
        [SerializeField] private bool timerIsRunning = false; // Состояние таймера
        private Coroutine timerCoroutine; // Хранит ссылку на запущенную корутину

        [SerializeField] private GameObject _canvasVideo;
        [SerializeField] List<GameObject> _canvasList = new List<GameObject>();    

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

        public States _lastState;
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
            _lastState = _currentState;
            _currentState = state;

            switch (state)
            {
                case States.WithoutMarkers:
                    StopTimer();
                    ActivateButtons(SpritesForImagesEnabled, true);
                    MarkersVisible(false);
                    _canvasVideo.SetActive(false);
                    break;

                case States.WithMarkers:
                    StopTimer();
                    ActivateButtons(SpritesForImagesEnabled, true);
                    _buttonSlider.GetComponent<Image>().sprite = _infoSelected; // Желтая кнопка Info
                    MarkersVisible(true);
                    _canvasVideo.SetActive(false);
                    DisactivateAllCanvasesSlider();
                    break;

                case States.Slider:
                    ActivateButtons(SpritesForImagesDisabled, false);
                    _buttonSlider.GetComponent<Image>().sprite = _close; // Кнопка крестик вместо Info
                    MarkersVisible(false);
                    break;

                case States.VideoPlayer:
                    ActivateButtons(SpritesForImagesDisabled, false);
                    _buttonVideoPlayer.GetComponent<Image>().sprite = _close; // Кнопка крестик вместо Play
                    _canvasVideo.SetActive(true);
                    MarkersVisible(false);
                    break;
            }
        }

        private IEnumerator TimerCoroutine()
        {
            timerIsRunning = true;
            while (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                yield return null; // Ожидание до следующего кадра
            }
            timeRemaining = 0;
            TimerFinished();
            timerIsRunning = false;
        }

        private void TimerFinished()
        {
            Debug.Log("Таймер завершен!");
            ChangeState(_lastState);
        }

        public void ResetTimer(float waitSec)
        {
            if (timerIsRunning)
            {
                StopCoroutine(timerCoroutine);
            }
            timeRemaining = waitSec;
            timerCoroutine = StartCoroutine(TimerCoroutine());
        }

        public void StopTimer()
        {
            if (timerIsRunning)
            {
                StopCoroutine(timerCoroutine);
                timerIsRunning = false;
                Debug.Log("Таймер остановлен!");
            }
        }

        private void DisactivateAllCanvasesSlider()
        {
            foreach (var canvas in _canvasList)
            {
                canvas.SetActive(false);
            }
        }

        private void ActivateButtons(Dictionary<Image, Sprite> sprites, bool disableInteractable)
        {
            foreach (var button in sprites.Keys)
            {
                button.sprite = sprites[button];
                //print(button.name);
                ButtonLeft buttonLeft = button.GetComponent<ButtonLeft>();
                ButtonRight buttonRight = button.GetComponent<ButtonRight>();

                if (buttonLeft != null)
                {
                    buttonLeft.enabled = disableInteractable;
                }
                else if (buttonRight != null)
                {
                    buttonRight.enabled = disableInteractable;
                }

                //if (button.name == "ButtonLeft" || button.name == "ButtonRight")
                //{
                //    button.GetComponent<Button>().interactable = disableInteractable;
                //    print(button.name + " " + disableInteractable);
                //}
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