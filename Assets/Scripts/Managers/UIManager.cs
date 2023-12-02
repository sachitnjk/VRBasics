using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	[Header("Script References")]
	[SerializeField] private BlockController playerBlockController;
	[SerializeField] private GameTimer gameTimerScript;

	[Header("UI Elements References")]
	public Slider healthSlider;
	public Slider blockMeterSlider;
	public Slider gameTimerSlider;
	public GameObject startTextPanel;
	public Image healthSliderFillImage;

	[SerializeField] private TextMeshProUGUI blockStatusText;

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

		startTextPanel.SetActive(true);
		StartCoroutine(OnStartTextTimer());

		gameTimerSlider.value = 0f;
		gameTimerSlider.maxValue = gameTimerScript.HoldOutTime;
	}

	private void Update()
	{
		healthSlider.value = playerHealthController.CurrentHealth;
		UpdateHealthBarColor();

		blockMeterSlider.value = playerBlockController.blockMeterValue;

		gameTimerSlider.value = gameTimerScript.elapsedTime;
		if(gameTimerSlider.value >= gameTimerSlider.maxValue)
		{
			GameManager.instance.empDischarged = true;
		}

		if(blockMeterSlider.value <= 0f)
		{
			blockStatusText.text = "Shield Recharging";
		}
		else if(blockMeterSlider.value < blockMeterSlider.maxValue && blockMeterSlider.value > 0f)
		{
			blockStatusText.text = "Shield Active";
		}
		else if(blockMeterSlider.value >= blockMeterSlider.maxValue)
		{
			blockStatusText.text = "Shield Usable";
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

	private IEnumerator OnStartTextTimer()
	{
		yield return new WaitForSeconds(10);
		startTextPanel.SetActive(false);
	}
}
