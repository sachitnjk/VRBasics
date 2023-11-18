using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
	Flock agentFlock;
	public Flock AgentFlock { get { return agentFlock; } }

	Collider agentCollider;

	//public collider accesor
	public Collider AgentCollider {  get {  return agentCollider; } }

	private void Start()
	{
		agentCollider = GetComponent<Collider>();
	}

	public void Move(Vector3 velocity)
	{
		//turn agent to direction to move to

		//transform.forward for 3D
		transform.forward = velocity;
		transform.position += (Vector3)velocity * Time.deltaTime;
		//actually move the agent to the position to move
	}

	public void Initialize(Flock flock)
	{
		agentFlock = flock;
	}

}
