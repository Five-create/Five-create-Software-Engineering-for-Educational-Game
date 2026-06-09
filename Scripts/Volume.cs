using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    void Start()
    {
        // Set initial volume and link slider to control it
        volumeSlider.value = AudioListener.volume;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        SetVolume(volumeSlider.value);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
