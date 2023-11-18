using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	[SerializeField] float disableTime;

	private void Update()
	{
		StartCoroutine(DestroyOverTime(disableTime));
	}

	private IEnumerator DestroyOverTime(float time)
	{
		yield return new WaitForSeconds(time);
		this.gameObject.SetActive(false);
	}
}
