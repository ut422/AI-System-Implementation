using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIController : MonoBehaviour
{
    public StateMachine stateMachine;
    public Transform player;
    public NavMeshAgent agent;
    public Transform[] patrolWaypoints;

    public Vector3 lastKnownPlayerPos;

    public float playerVolume = 15f;
    public int currentWaypointIndex;


    public float patrolSpeed = 5;
    public float detectionRange = 10;

    public bool playerInCone;
    public bool canSeePlayer;

    public float visionAngle = 90f;

    public float hearingRange = 15f;
    public float hearingThreshold = 10f;

    public bool isManaged = true;
    

    private void Start()
    {
        //stateMachine.ChangeState(new StateIdle(this));
        stateMachine = new StateMachine();
        if (isManaged)
        {
            AIManager.Instance.RegisterAgent(this);
        }
        stateMachine.ChangeState(new StateIdle(this));
    }

    private void Update()
    {
        stateMachine.Update();
    }

    public void ChangeState(State newState)
    {
        stateMachine.ChangeState(newState);
    }

    public bool CanSeePlayer()
    {

        return HasLineOfSight(player);
        
        //return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public bool CanHearPlayer(float noiseLevel)
    {
        if (player == null) return false;

        if (Vector3.Distance(transform.position, player.position) < hearingRange && noiseLevel > hearingThreshold)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetPlayerInVisionCone(bool invisible)
    {
        playerInCone = invisible;
    }


    public bool HasLineOfSight(Transform target)
    {
        /* if (!playerInCone)
        {
            return false;
        }

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange))
        {
            if (hit.transform == target)
            {
                return true;
            }
            return false;
        }
        */
        if (Vector3.Distance(transform.position, player.position) <= 0.5f)
        {
            AIManager.Instance.lastKnownPlayerPos = player.position;
            return true;
        }

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToTarget);

        if (angleToPlayer < visionAngle / 2f) 
        {
            Debug.Log("Player in cone");

            RaycastHit hit;
            if (Physics.Raycast(transform.position, directionToTarget, out hit, detectionRange))
            {
                if (hit.transform == target)
                {
                    AIManager.Instance.lastKnownPlayerPos = player.position;
                    return true;
                }
                
            }
        }
        return false;
    }

    public void ChasePlayer()
    {
        /*transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * patrolSpeed);

        Vector3 direction = (player.position - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            float rotationSpeed = 3f;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }*/
        if (player == null || agent == null)
        {
            return;
        }
        if (!agent.pathPending && agent.destination != player.position)
        {
            agent.SetDestination(player.position);
        }
    }

    public void Patrol()
    {
        if (patrolWaypoints.Length == 0)
        {
            return;
        }

        Transform targetWaypoint = patrolWaypoints[currentWaypointIndex];

        /*
        Vector3 direction = (targetWaypoint.position - transform.position).normalized;
        if (direction != Vector3.zero) 
        {
            float rotationSpeed = 3f;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, rotationSpeed * Time.deltaTime, 0);
            transform.rotation = Quaternion.LookRotation(newDirection);
        }

        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, Time.deltaTime * patrolSpeed);
        
        if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.2f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
        }
        */

        if (agent.remainingDistance < 0.02f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
            agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        }

    }
}
