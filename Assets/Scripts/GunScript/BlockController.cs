using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
	[SerializeField] private float blockCooldown;

	public bool CanBlock { get; private set; }
	public bool BlockTriggered{get; private set;}
	public bool BlockEnded { get; set; }

	private void Start()
	{
		CanBlock = true;
		BlockEnded = true;
		BlockTriggered = false;
	}

	private void Update()
	{
		if(VrController_Inputs.Instance.rightgrip.IsPressed() && VrController_Inputs.Instance.leftgrip.IsPressed()) 
		{
			Debug.Log("Both grips pressed");
			if(CanBlock)
			{
				BlockTriggered = true;
				CanBlock = false;
				Invoke("ResetBlockCooldown", blockCooldown);
			}
		}
	}

	private void ResetBlockCooldown()
	{
		//Called through Invoke function
		if ((VrController_Inputs.Instance.rightgrip.WasReleasedThisFrame() || VrController_Inputs.Instance.leftgrip.WasReleasedThisFrame()) && BlockEnded)
		{
			CanBlock = true;
			UIManager.Instance.blockMeterSlider.value = UIManager.Instance.blockMeterSlider.maxValue;
			Debug.Log("Block cooldown reset");
		}
	}
}