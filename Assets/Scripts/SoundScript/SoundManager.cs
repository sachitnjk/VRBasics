using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using static SoundManager;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	[Header("Player Audio Clips")]
	public AudioClip playerAttack;
	public AudioClip playerShield;
	public AudioClip playerLowHealth;

	[Header("Audio Mixer Groups")]
	public AudioMixerGroup playerGroup;
	public AudioMixerGroup playerHealthGroup;

	[SerializeField] private AudioSource audioSource;
	[SerializeField] private AudioSource loopedAudioSource;

	public enum SoundType
	{
		PlayerShoot,
		PlayerShield,
		PlayerLowHealth,
	}

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
	}

	public void PlaySound(SoundType soundType)
	{
		AudioClip clip = null;
		AudioMixerGroup audioMixerGroup = null;

		switch(soundType)
		{
			case SoundType.PlayerShoot:
				clip = playerAttack;
				audioMixerGroup = playerGroup;
				break;
			case SoundType.PlayerShield:
				clip = playerShield;
				audioMixerGroup = playerGroup;
				break;
		}

		if(clip != null)
		{
			audioSource.clip = clip;
			audioSource.outputAudioMixerGroup = audioMixerGroup;
			audioSource.PlayOneShot(audioSource.clip);
		}
		else
		{
			Debug.Log("Sound type not found");
		}
	}


	public void PlayLoopSound(SoundType type)
	{
		AudioClip clip = null;
		AudioMixerGroup audioMixerGroup = null;
		AudioSource currentAudioSource = null;

		switch (type)
		{
			case SoundType.PlayerLowHealth:
				clip = playerLowHealth;
				audioMixerGroup = playerHealthGroup;
				currentAudioSource = loopedAudioSource;
				break;
		}

		if (clip != null)
		{
			currentAudioSource.clip = clip;
			currentAudioSource.outputAudioMixerGroup = audioMixerGroup;
			currentAudioSource.Play();
		}
		else
		{
			Debug.Log("Sound type not found");
		}
	}
	public void StopLoopSound(SoundType type)
	{
		AudioSource currentAudioSource = null;

		switch (type)
		{
			case SoundType.PlayerLowHealth:
				currentAudioSource = loopedAudioSource;
				break;
		}

		if (currentAudioSource != null && currentAudioSource.isPlaying)
		{
			currentAudioSource.Stop();
		}
	}
}
