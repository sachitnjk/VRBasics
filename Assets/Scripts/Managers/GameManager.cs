using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager instance;

	public HealthController playerHealthController;
	public BlockController blockController;

	public bool empDischarged;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
	}
}
