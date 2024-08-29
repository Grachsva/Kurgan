using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Config
{
    public class SpeedSlider : MonoBehaviour
    {
        [SerializeField] private Slider _speedSlider;
        [SerializeField] private Text _speedText;

        private void Start()
        {
            if (PlayerPrefs.GetInt("CameraSpeed") == 0)
            {
                Debug.Log("Значение пустое и было выставлено с: " + PlayerPrefs.GetInt("CameraSpeed"));
                PlayerPrefs.SetInt("CameraSpeed", 5);
            }
            _speedSlider.value = PlayerPrefs.GetInt("CameraSpeed");
            _speedText.text = "Скорость: " + PlayerPrefs.GetInt("CameraSpeed");
        }

        public void SetSpeed()
        {
            PlayerPrefs.SetInt("CameraSpeed", Convert.ToInt32(_speedSlider.value));
            _speedText.text = "Скорость: " + PlayerPrefs.GetInt("CameraSpeed");
        }
    }
}