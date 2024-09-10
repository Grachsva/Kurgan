using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class VideoController : MonoBehaviour
{
    [SerializeField] private GameObject _playButton;

    private VideoPlayer _player;

    private void OnEnable()
    {
        _playButton.SetActive(true);
        _player = GetComponent<VideoPlayer>();
        _player.loopPointReached += EndReached;
    }

    public void SetFrame(int frame)
    {
        _player.frame = frame;
        //GetComponent<VideoPlayer>().frame = frame;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        _playButton.SetActive(true);
        _player.frame = 100;
    }

}