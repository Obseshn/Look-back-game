using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicVolumeController : MonoBehaviour
{
    [SerializeField] private AudioSource music;
    [SerializeField] private Text percentText;

    private float musicVolume;
    private float percentOfVolume;

    // Update is called once per frame
    void Update()
    {
        music.volume = musicVolume;
        percentOfVolume = musicVolume * 100;
        percentText.text = percentOfVolume.ToString("0") + "%";
    }

    public void ChangeVolume(float volume)
    {
        musicVolume = volume; 
    }
}
