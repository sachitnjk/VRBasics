using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
	[Header("Panel references")]
	[SerializeField] private GameObject settingsPanel;
	[SerializeField] private GameObject mainMenuPanel;
	[SerializeField] private GameObject audioPanel;
	[SerializeField] private GameObject controlsPanel;

	[Header("AudioMixer")]
	[SerializeField] private AudioMixer audioMixer;

	public void StartGame(int index)
	{
		SceneManager.LoadScene(index);
	}
	public void ToSettings()
	{
		mainMenuPanel.SetActive(false);
		settingsPanel.SetActive(true);
	}

	public void ToAudioPanel()
	{
		settingsPanel.SetActive(false);
		audioPanel.SetActive(true);
	}

	public void ToControlsPanel()
	{
		settingsPanel.SetActive(false);
		controlsPanel.SetActive(true);
	}

	public void ReturnToMainMenu()
	{
		settingsPanel.SetActive(false);
		mainMenuPanel.SetActive(true);
	}

	public void ReturnToSettings(GameObject activePanel)
	{
		activePanel.SetActive(false);
		settingsPanel.SetActive(true);
	}

	public void Exitgame()
	{
		Debug.Log("qutting application");
		Application.Quit();
	}

	//Slider Functions

	public void MasterVolumeSetter(float slidervalue)
	{
		audioMixer.SetFloat("MasterVolume", Mathf.Log10(slidervalue) * 20);
	}
	public void EntityVolumeSetter(float slidervalue)
	{
		audioMixer.SetFloat("EntityVolume", Mathf.Log10(slidervalue) * 20);
	}
	public void BGMSetter(float slidervalue)
	{
		audioMixer.SetFloat("BGM", Mathf.Log10(slidervalue) * 20);
	}
}
