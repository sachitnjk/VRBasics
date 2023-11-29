using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[Header("Slider References")]
	[SerializeField] private BlockController playerBlockController;
	[SerializeField] private GameTimer gameTimerScript;

	[Header("Slider References")]
	public Slider healthSlider;
	public Slider blockMeterSlider;
	public Slider gameTimerSlider;
	public Image healthSliderFillImage;

	private HealthController playerHealthController;

	private void Start()
	{
		if(GameManager.instance.playerHealthController != null)
		{
			playerHealthController = GameManager.instance.playerHealthController;
		}
		healthSlider.maxValue = playerHealthController.MaxHealth;
		healthSlider.value = healthSlider.maxValue;

		blockMeterSlider.maxValue = playerBlockController.blockMeterMax;
		blockMeterSlider.value = blockMeterSlider.maxValue;

		gameTimerSlider.value = 0f;
		gameTimerSlider.maxValue = gameTimerScript.holdOutTime;
	}

	private void Update()
	{
		healthSlider.value = playerHealthController.CurrentHealth;
		UpdateHealthBarColor();

		blockMeterSlider.value = playerBlockController.blockMeterValue;

		gameTimerSlider.value = gameTimerScript.elapsedTime;
	}

	private void Awake()
	{
		if(Instance == null)
		{
			Instance = this;
		}
	}

	private void UpdateHealthBarColor()
	{
		// Calculate the gradient color based on the health value
		float normalizedHealth = healthSlider.normalizedValue;
		Color gradientColor = new Color(1f - normalizedHealth, normalizedHealth, 0f, 1f);

		// Set the gradient color to the slider fill image
		healthSliderFillImage.color = gradientColor;
	}
}