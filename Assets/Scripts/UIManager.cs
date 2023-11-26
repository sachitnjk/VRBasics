using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	public Slider healthSlider;
	public Image sliderFillImage;

	private HealthController playerHealthController;


	private void Start()
	{
		playerHealthController = GameManager.instance.playerHealthController;
		healthSlider.maxValue = playerHealthController.MaxHealth;
		healthSlider.value = healthSlider.maxValue;
	}

	private void Update()
	{
		healthSlider.value = playerHealthController.CurrentHealth;
		UpdateHealthBarColor();
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
		sliderFillImage.color = gradientColor;
	}
}
