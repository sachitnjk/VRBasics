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
	[HideInInspector] public InputAction rightgrip;
	[HideInInspector] public InputAction leftgrip;

	private void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
		}
	}

	private void Start()
	{
		rightgrip = rightHandController.selectAction.reference;
		leftgrip = leftHandController.selectAction.reference;

		rightTrigger = rightHandController.activateAction.reference;
		leftTrigger = leftHandController.activateAction.reference;
	}
}
