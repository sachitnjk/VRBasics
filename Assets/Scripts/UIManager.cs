using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[SerializeField] private BlockController playerBlockController;

	public Slider healthSlider;
	public Slider blockMeterSlider;
	public float blockMeterMax;
	public Image healthSliderFillImage;

	private HealthController playerHealthController;
	private BlockController blockController;


	private void Start()
	{
		if(GameManager.instance.playerHealthController != null)
		{
			playerHealthController = GameManager.instance.playerHealthController;
		}
		healthSlider.maxValue = playerHealthController.MaxHealth;
		healthSlider.value = healthSlider.maxValue;

		blockMeterSlider.maxValue = blockMeterMax;
		blockMeterSlider.value = blockMeterSlider.maxValue;
	}

	private void Update()
	{
		healthSlider.value = playerHealthController.CurrentHealth;
		UpdateHealthBarColor();

		if(playerBlockController != null && blockMeterSlider != null) 
		{
			if(playerBlockController.BlockTriggered) 
			{
				BlockTriggered();
			}
		}
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

	private void BlockTriggered()
	{
		blockMeterSlider.value = Mathf.Clamp(blockMeterSlider.value, 0f, blockMeterMax);
		blockMeterSlider.value--;
		if(blockMeterSlider.value <= 0f)
		{
			playerBlockController.BlockEnded = true;
		}
	}
	public void OnBlockCooldownReset()
	{
		if (blockMeterSlider != null)
		{
			Debug.Log("going here");
			// Reset the block meter slider to its maximum value
			blockMeterSlider.value = blockMeterSlider.maxValue;
		}
	}
}
