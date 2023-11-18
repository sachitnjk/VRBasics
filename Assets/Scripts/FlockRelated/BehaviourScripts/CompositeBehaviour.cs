using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
	public FlockBehaviour[] behaviours;
	public float[] weights;


	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, GameObject target)
	{
		//To handle data mismatch
		if(weights.Length != behaviours.Length) 
		{
			Debug.LogError("Data mismatch in :" + name, this);
			return Vector3.zero;
		}

		//move setup
		Vector3 move = Vector3.zero;

		//iterate through behaviours
		for(int i = 0; i < behaviours.Length; i++)
		{
			Vector3 partialMove = behaviours[i].CalculateMove(agent, context, flock, target) * weights[i];

			//limiting partialMove to extent of weights
			if(partialMove != Vector3.zero) 
			{
				if(partialMove.sqrMagnitude > weights[i] * weights[i])
				{
					partialMove.Normalize();
					partialMove *= weights[i];
				}

				move += partialMove;
			}
		}

		return move;
	}
}
