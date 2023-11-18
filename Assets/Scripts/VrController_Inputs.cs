using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class VrController_Inputs : MonoBehaviour
{
	public static VrController_Inputs Instance;

	[SerializeField] private ActionBasedController leftHandController; 
	[SerializeField] private ActionBasedController rightHandController; 
	[HideInInspector] public InputAction rightTrigger;
	[HideInInspector] public InputAction leftTrigger;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		rightTrigger = rightHandController.activateAction.reference;
		leftTrigger = leftHandController.activateAction.reference;
	}

	//private void Update()
	//{
	//	TriggerCheck();
	//}

	//private void TriggerCheck()
	//{
	//	if (rightTrigger != null && rightTrigger.IsPressed())
	//	{
	//		Debug.Log("right pressed");
	//		//DebugLogger.current.AddLine("right Pressed");
	//	}
	//	if (leftTrigger != null && leftTrigger.IsPressed())
	//	{
	//		Debug.Log("left pressed");
	//		//DebugLogger.current.AddLine("left Pressed");
	//	}
	//}
}
