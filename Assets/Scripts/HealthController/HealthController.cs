using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
	[SerializeField] private float maxHealth;
	[SerializeField] private GameObject destroyEffect;

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
			GameObject deathEffect = ObjectPooler.instance.GetPooledObject(destroyEffect);
			if(deathEffect != null) 
			{
				deathEffect.transform.position = this.gameObject.transform.position;
				deathEffect.SetActive(true);
				Debug.Log("going here");
			}

			this.gameObject.SetActive(false);
		}
		else
		{
			return;
		}
	}
}
