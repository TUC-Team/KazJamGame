using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class Music_Control : MonoBehaviour
{

	public Slider music_Slider;
	public Slider sfx_Slider;
	public float musicVolume;
	public float SFXVolume;

	public void SetVolume(string Wh_Value)
	{



		if (Wh_Value == "Music")
        {
			float sliderValue = music_Slider.value;
			Debug.Log("Music to change Value");
			musicVolume = music_Slider.value;
			AkSoundEngine.SetRTPCValue("Music", musicVolume);
        }

		if (Wh_Value == "Volume")
		{
			float sliderValue = sfx_Slider.value;
			Debug.Log("Volume sound to change Value");
			SFXVolume = sfx_Slider.value;
			AkSoundEngine.SetRTPCValue("Volume", SFXVolume);
		}
	}

}