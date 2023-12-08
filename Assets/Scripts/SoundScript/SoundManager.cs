using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
	public static SoundManager instance;

	[Header("Player Audio Clips")]
	public AudioClip playerAttack;
	public AudioClip playerShield;
	public AudioClip playerLowHealth;

	[Header("Audio Mixer Groups")]
	public AudioMixerGroup playerGroup;

	[SerializeField] private AudioSource audioSource;

	public enum SoundType
	{
		PlayerShoot,
		PlayerShield,
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
			Debug.Log("wee");
			audioSource.PlayOneShot(audioSource.clip);
		}
		else
		{
			Debug.Log("Sound type not found");
		}
	}
}
