using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
	public override Vector3 CalculateMove(FlockAgent agent, List<Transform> context, Flock flock, GameObject target)
	{
		//if no neighbours, main current alignment
		if (context.Count == 0)
		{
			return agent.transform.forward;
		}

		//add all points together and average
		Vector3 alignmentMove = Vector3.zero;
		List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);
		foreach (Transform item in filteredContext)
		{
			alignmentMove += (Vector3)item.transform.forward;
		}
		alignmentMove /= context.Count;

		return alignmentMove;
	}
}
