using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	[SerializeField] private float maxHealth;

	private float currentHealth;
	private bool isDead;

	private void OnEnable()
	{
		currentHealth = maxHealth;
	}
	private void Start()
	{
		isDead = false;
		currentHealth = maxHealth;
	}

	private void Update()
	{
		currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
	}

	public void DamageHealth(float damageTaken)
	{
		currentHealth -= damageTaken;
	}
	public void HealHealth(float healAmount)
	{
		currentHealth += healAmount;
	}

	public bool EntityDead()
	{
		if(currentHealth <= 0) 
		{
			isDead = true;
		}
		else
		{
			isDead = false;
		}
		return isDead;
	}
}
