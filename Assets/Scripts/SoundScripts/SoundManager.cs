﻿using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
                instance = GameObject.FindObjectOfType<SoundManager>();
            return instance;
        }
    }

    public string mainFolder = "GameSound";
	public string soundFolder = "Sounds";
	public string musicFolder = "Music";
    bool timerOn;
    static float timer;

    public float fadeSpeed = 50; // скорость плавного перехода между треками музыки
    public float pitch = 1;

	public AudioMixerGroup musicGroup;
	public AudioMixerGroup soundGroup;

	private static AudioSource last, current, steps;
	public static float musicVolume, soundVolume;
	private static bool muteMusic, muteSound;
    private static float currentPitch = 1;


    void Awake()
	{
        if (!PlayerPrefs.HasKey("MusicIsOn"))
            PlayerPrefs.SetInt("MusicIsOn", 1);
        if (!PlayerPrefs.HasKey("SoundsIsOn"))
            PlayerPrefs.SetInt("SoundsIsOn", 1);

        if (PlayerPrefs.GetInt("MusicIsOn") == 0)
            MuteMusic(true);
        else MuteMusic(false);

        if (PlayerPrefs.GetInt("SoundsIsOn") == 0)
            MuteSound(true);
        else MuteSound(false);

        musicVolume = 0.7f;
        soundVolume = 1;

        StartCoroutine(InitiateSteps("player_step")); // стринговое константа требует замены на переменную

    }

    void Update()
    {
        Fader();
        if (timerOn)
        {
            timer += Time.deltaTime;
        }
        if (timer > 1)
        {
            timer = 0;
            timerOn = false;
        }
    }

    public static void SetPitch(float pitch)
    {
        currentPitch = pitch;
        AudioSource[] sounds = Instance.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource sound in sounds)
        {
            sound.pitch = pitch;
        }
    }

	public static void SoundVolume(float volume)
	{
		soundVolume = volume;
	}

	public static void MusicVolume(float volume)
	{
		musicVolume = volume;
		if(current) current.volume = volume;
	}

	public static void MuteSound(bool value)
	{
		muteSound = value;
	}

	public static void MuteMusic(bool value)
	{
		muteMusic = value;
		if(current) current.mute = value;
	}

	void PlaySoundInternal(string soundName)
	{
		if(string.IsNullOrEmpty(soundName))
		{
			Debug.Log(Instance + " :: Имя файла не указано.");
			return;
		}

		StartCoroutine(GetSound(soundName));
	}

    void PlaySoundInternalPitched(string soundName, float pitch)
    {
        if (string.IsNullOrEmpty(soundName))
        {
            Debug.Log(Instance + " :: Имя файла не указано.");
            return;
        }

        StartCoroutine(GetSoundPitched(soundName, pitch));
    }

    public static void PlaySound(string name)
	{
		Instance.PlaySoundInternal(name);
	}

    public static void PlayPitchedSound(string name)
    {
        Instance.PlaySoundInternalPitched(name, timer);
    }

    void PlayMusicInternal(string musicName, bool loop)
	{
		if(string.IsNullOrEmpty(musicName))
		{
			Debug.Log(Instance + " :: Имя файла не указанно.");
			return;
		}

		StartCoroutine(GetMusic(musicName, loop));
	}

	public static void PlayMusic(string name, bool loop)
	{
		Instance.PlayMusicInternal(name, loop);
	}

	void Fader()
	{
		if(last == null) return;

		last.volume = Mathf.Lerp(last.volume, 0, fadeSpeed * Time.deltaTime);
		current.volume = Mathf.Lerp(current.volume, musicVolume, fadeSpeed * Time.deltaTime);

		if(last.volume < 0.05f)
		{
			last.volume = 0;
			Destroy(last.gameObject);
		}
	}

	IEnumerator GetMusic(string musicName, bool loop)
	{
		ResourceRequest request = LoadAsync(musicFolder + "/" + musicName);

		while(!request.isDone)
		{
			yield return null;
		}

		AudioClip clip = (AudioClip)request.asset;

		if(clip == null)
		{
			Debug.Log(Instance + " :: Файл не найден: " + musicName);
			yield return false;
		}

		last = current;

		GameObject obj = new GameObject("Music: " + musicName);
		AudioSource au = obj.AddComponent<AudioSource>();
		obj.transform.parent = transform;
		au.outputAudioMixerGroup = musicGroup;
		au.playOnAwake = false;
		au.loop = loop;
        au.pitch = currentPitch;
		au.mute = muteMusic;
		au.volume = (last == null) ? musicVolume : 0;
		au.clip = clip;
		au.Play();
		current = au;
	}

	IEnumerator GetSound(string soundName)
	{
		ResourceRequest request = LoadAsync(soundFolder + "/" + soundName);

		while(!request.isDone)
		{
			yield return null;
		}

		AudioClip clip = (AudioClip)request.asset;

		if(clip == null)
		{
			Debug.Log(Instance + " :: Файл не найден: " + soundName);
			yield return false;
		}

		GameObject obj = new GameObject("Sound: " + soundName);
		AudioSource au = obj.AddComponent<AudioSource>();
		obj.transform.parent = transform;
		au.outputAudioMixerGroup = soundGroup;
		au.playOnAwake = false;
		au.loop = false;
        au.pitch = currentPitch;
		au.mute = muteSound;
		au.volume = soundVolume;
		au.clip = clip;
		au.Play();
        //Debug.Log(au.volume + " " + soundVolume);
		Destroy(obj, clip.length);
	}

    IEnumerator GetSoundPitched(string soundName, float pitch)
    {
        ResourceRequest request = LoadAsync(soundFolder + "/" + soundName);

        while (!request.isDone)
        {
            yield return null;
        }

        AudioClip clip = (AudioClip)request.asset;

        if (clip == null)
        {
            Debug.Log(Instance + " :: Файл не найден: " + soundName);
            yield return false;
        }

        GameObject obj = new GameObject("Sound: " + soundName);
        AudioSource au = obj.AddComponent<AudioSource>();
        obj.transform.parent = transform;
        au.outputAudioMixerGroup = soundGroup;
        au.playOnAwake = false;
        au.loop = false;
        au.pitch = currentPitch + pitch/4;
        au.mute = muteSound;
        au.volume = soundVolume;
        au.clip = clip;
        au.Play();
        timerOn = true;
        Destroy(obj, clip.length);
    }

    ResourceRequest LoadAsync(string name)
	{
		string path = mainFolder + "/" + name;
		return Resources.LoadAsync<AudioClip>(path);
	}

    IEnumerator InitiateSteps(string musicName)
    {
        ResourceRequest request = LoadAsync(soundFolder + "/" + musicName);

        while (!request.isDone)
        {
            yield return null;
        }

        AudioClip clip = (AudioClip)request.asset;

        if (clip == null)
        {
            Debug.Log(Instance + " :: Файл не найден: " + musicName);
            yield return false;
        }

        GameObject stepsObj = new GameObject("Steps");
        AudioSource stepsAudio = stepsObj.AddComponent<AudioSource>();
        stepsObj.transform.parent = transform;
        stepsAudio.playOnAwake = false;
        stepsAudio.loop = true;
        stepsAudio.pitch = currentPitch;
        stepsAudio.mute = muteSound;
        stepsAudio.volume = (last == null) ? musicVolume : 0;
        //Debug.Log(clip.name);
        stepsAudio.clip = clip;
        stepsAudio.Pause();
        steps = stepsAudio;
    }
    
    public static void MakeSteps(bool isStepping)
    {
		if (steps != null) 
		{
			if (steps.isPlaying && !isStepping)
				steps.Pause ();
			else if (!steps.isPlaying && isStepping)
				steps.Play ();
		}
    }
}
