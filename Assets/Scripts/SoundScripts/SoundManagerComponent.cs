using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerComponent : MonoBehaviour {

	public void PlayMusic(string name)
	{
		SoundManager.PlayMusic(name, true);
	}

	public void PlaySound(string name)
	{
		SoundManager.PlaySound(name);
	}

	public void SoundVolume(float volume)
	{
		SoundManager.SoundVolume(volume);
	}

	public void MusicVolume(float volume)
	{
		SoundManager.MusicVolume(volume);
	}

	public void ToggleMusicMuted(bool value)
	{
		SoundManager.MuteMusic(!value);
	}

	public void ToggleSoundMuted(bool value)
	{
		SoundManager.MuteSound(!value);
	}
}
