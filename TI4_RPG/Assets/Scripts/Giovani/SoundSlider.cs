using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundSlider : MonoBehaviour
{
    [SerializeField] AudioMixer myAudioMixer;
    public void MasterVolume(float sliderValue){
        myAudioMixer.SetFloat("Master", Mathf.Log10(sliderValue) * 20);
    }
    public void SFXVolume(float sliderValue){
        myAudioMixer.SetFloat("SFX", Mathf.Log10(sliderValue) * 20);
    }
    public void MusicVolume(float sliderValue){
        myAudioMixer.SetFloat("Music", Mathf.Log10(sliderValue) * 20);
    }
    public void DialogueVolume(float sliderValue){
        myAudioMixer.SetFloat("Dialogue", Mathf.Log10(sliderValue) * 20);
    }
}