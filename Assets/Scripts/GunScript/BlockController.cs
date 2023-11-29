using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	[SerializeField] private HitDetector playerHitDetector;

	private bool canBlock;
	private bool blockTriggered;
	private bool blockEnded;

	[SerializeField] private float blockCooldown;
	public int blockMeterMax;
	[HideInInspector] public int blockMeterValue;

	private void Start()
	{
		canBlock = true;
		blockEnded = false;

		blockMeterValue = blockMeterMax;
	}

	private void Update()
	{
		if(VrController_Inputs.Instance.rightgrip.IsPressed() && VrController_Inputs.Instance.leftgrip.IsPressed() && canBlock) 
		{
			canBlock = false;
			StartCoroutine(BlockIsTriggered());
		}

		Debug.Log(playerHitDetector.enabled);
	}
	private IEnumerator BlockIsTriggered()
	{
		while(blockMeterValue > 0)
		{
			blockMeterValue--;
			yield return null;
		}

		playerHitDetector.enabled = false;

		blockMeterValue = 0;

		yield return new WaitForSeconds(blockCooldown);

		while (blockMeterValue < blockMeterMax)
		{
			blockMeterValue++;
			yield return null;
		}

		canBlock = true;
		playerHitDetector.enabled = true;
		blockMeterValue = blockMeterMax;
		Debug.Log("Block cooldown reset");
	}
}
