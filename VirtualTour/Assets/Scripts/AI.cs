using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;


public enum AIState { WALKING, TALKING, SITTING };

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(SphereCollider))]

public class AI : MonoBehaviour {

    public Transform[] patrolPoints;
    private int wanderIndex;
    public NavMeshAgent agent;
    public Animator anim;

    public AIState _state;

    public float wanderDistance;

    // Use this for initialization
    void Start () {
        agent.avoidancePriority = UnityEngine.Random.Range(1, 100); //set random priority
    }
	
	// Update is called once per frame
	void Update () {
        changeState();
	}

    //Set the next destination point 
    void getNewDestination()
    {
        wanderIndex++;

        if (wanderIndex >= patrolPoints.Length)
        {
            wanderIndex = 0;
        }
        agent.SetDestination(patrolPoints[wanderIndex].transform.position);
    }

    public static Vector3 RandomNavSphere(Vector3 origin, float distance, int layermask)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * distance;
        randomDirection = new Vector3(randomDirection.x, origin.y, randomDirection.z);
        randomDirection += origin;

        NavMeshHit navHit;

        NavMesh.SamplePosition(randomDirection, out navHit, distance, layermask);

        return navHit.position;
    }

    public void changeState() {
        switch (_state) {
            case AIState.WALKING:
                //Changes the animation depending on the speed the agent is moving
                anim.Play("Walk");
                //move the patrol 
                if (agent.remainingDistance <= 1 || agent.destination == null || agent.velocity.magnitude == 0)
                {
                    Vector3 newPos = RandomNavSphere(transform.position, wanderDistance, 9);
                    agent.SetDestination(newPos);
                }
                break;

            case AIState.TALKING:
                break;

            case AIState.SITTING:
                break;

        } 
    }
}
