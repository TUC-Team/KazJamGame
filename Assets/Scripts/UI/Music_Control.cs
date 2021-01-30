using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
public class Music_Control : MonoBehaviour
{

AudioMixer audioMixer;

[Space(15)]
public Slider musicSlider;
public Slider sfxSlider;

public void SetMusicVolume(float volume)
{
	//audioMixer.SetFloat("musicVolume", volume);
}

public void SetSFXVolume(float volume)
{
	//audioMixer.SetFloat("sfxVolume", volume);
}

public void Start()
{
	musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
	sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0);
}

public void OnDisable()
{
	float musicVolume = 0;
	float sfxVolume = 0;

	//audioMixer.GetFloat("musicVolume", out musicVolume);
	//audioMixer.GetFloat("sfxVolume", out sfxVolume);

	PlayerPrefs.SetFloat("musicVolume", musicVolume);
	PlayerPrefs.SetFloat("sfxVolume", sfxVolume);
	PlayerPrefs.Save();
}
}