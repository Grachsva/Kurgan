using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;

    private void OnEnable()
    {
        _playButton.SetActive(true);
    }

    public void SetFrame(int frame)
    {
        
        GetComponent<VideoPlayer>().frame = frame;
    }
}