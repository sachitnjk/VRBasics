using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public HealthController playerHealthController;

	public TextMeshProUGUI tempHealthLogger;

	private void Update()
	{
		tempHealthLogger.text = playerHealthController.CurrentHealth.ToString();
	}

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
}
