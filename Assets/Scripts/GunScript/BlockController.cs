using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	[SerializeField] private float blockCooldown;

	private bool canBlock;
	private bool blockTriggered;
	private bool blockEnded;

	public int blockMeterValue;
	public int blockMeterMax;

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
	}
	private IEnumerator BlockIsTriggered()
	{
		while(blockMeterValue > 0)
		{
			blockMeterValue--;
			yield return null;
		}
		blockMeterValue = 0;

		yield return new WaitForSeconds(blockCooldown);

		while (blockMeterValue < blockMeterMax)
		{
			blockMeterValue++;
			yield return null;
		}

		canBlock = true;
		blockMeterValue = blockMeterMax;
		Debug.Log("Block cooldown reset");
	}
}
