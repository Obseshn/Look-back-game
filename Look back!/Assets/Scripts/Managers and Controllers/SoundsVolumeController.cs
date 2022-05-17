using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundsVolumeController : MonoBehaviour
{
    public AudioManager AudioManager;
    [SerializeField] private Text percentText;
    private AudioSource playerAudio;

    private float percentOfVolume;
    private float volume;
    private void Start()
    {
        playerAudio = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
    }
    private void Update()
    {
        percentOfVolume = volume * 100;
        percentText.text = percentOfVolume.ToString("0") + "%";
    }
    public void VolumeChange(float volumeToChange)
    {
        volume = volumeToChange;
        for (int i = 0; i < AudioManager.sounds.Length; i++)
        {
            AudioManager.sounds[i].source.volume = volumeToChange;
        }
        playerAudio.volume = volumeToChange;
    }

}
