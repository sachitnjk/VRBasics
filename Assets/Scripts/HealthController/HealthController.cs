using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	[SerializeField] private float maxHealth;

	private float currentHealth;

	private void OnEnable()
	{
		currentHealth = maxHealth;
	}
	private void Start()
	{
		currentHealth = maxHealth;
	}

	private void Update()
	{
		currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
		EntityDeadCheck();
	}

	public void DamageHealth(float damageTaken)
	{
		currentHealth -= damageTaken;
	}
	public void HealHealth(float healAmount)
	{
		currentHealth += healAmount;
	}

	public void EntityDeadCheck()
	{
		if(currentHealth <= 0f) 
		{
			this.gameObject.SetActive(false);
		}
		else
		{
			return;
		}
	}
}
