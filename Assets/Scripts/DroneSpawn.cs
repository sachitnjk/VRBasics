using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawn : MonoBehaviour
{
	[SerializeField] GameObject flockObject;

	private void Start()
	{
		StartCoroutine(EnableDrones());
	}

	private IEnumerator EnableDrones()
	{
		yield return new WaitForSeconds(10);
		flockObject.SetActive(true);
	}

}
