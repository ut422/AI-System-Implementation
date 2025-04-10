using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    // exclamation mark TMP object
    public TextMeshPro exclamationMark;

    // flashing logic
    public float flashDuration = 0.25f; // duration of the flash (in seconds)
    private bool isFlashing = false;


    private void Start()
    {
        //stateMachine.ChangeState(new StateIdle(this));
        stateMachine = new StateMachine();
        if (isManaged)
        {
            // register the AI with AIManager if it's managed
            AIManager.Instance.RegisterAgent(this);
        }
        // change state to idle
        stateMachine.ChangeState(new StateIdle(this));

        if (exclamationMark != null)
        {
            exclamationMark.gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        // update the current state of the state machine
        stateMachine.Update();


        if (CanSeePlayer() || CanHearPlayer(playerVolume))
        {
            if (!isFlashing)
            {
                StartCoroutine(FlashExclamationMark());
            }
        }
        else
        {
            if (exclamationMark != null)
            {
                exclamationMark.gameObject.SetActive(false); // hide exclamation mark when not detected
            }
        }
    }

    private IEnumerator FlashExclamationMark()
    {
        isFlashing = true;

        while (CanSeePlayer() || CanHearPlayer(playerVolume))
        {
            // show the exclamation mark
            if (exclamationMark != null)
            {
                exclamationMark.gameObject.SetActive(true);
            }

            // wait for the flash duration
            yield return new WaitForSeconds(flashDuration);

            // hide the exclamation mark
            if (exclamationMark != null)
            {
                exclamationMark.gameObject.SetActive(false);
            }

            // wait for the flash duration again
            yield return new WaitForSeconds(flashDuration);
        }

        isFlashing = false; // flashing ends when player is no longer detected
    }

    public void ChangeState(State newState)
    {
        // change the state in the state machine
        stateMachine.ChangeState(newState);
    }

    public bool CanSeePlayer()
    {
        // check if AI has line of sight to the player
        return HasLineOfSight(player);

        //return Vector3.Distance(transform.position, player.position) < detectionRange;
    }

    public bool CanHearPlayer(float noiseLevel)
    {
        // check if AI hears the player based on noise level and distance
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
        // set whether the player is in the AI's vision cone
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
        // check if the AI has line of sight to the player
        if (Vector3.Distance(transform.position, player.position) <= 0.5f)
        {
            AIManager.Instance.lastKnownPlayerPos = player.position;
            return true;
        }

        Vector3 directionToTarget = (target.position - transform.position).normalized;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToTarget);

        // check if the player is within the vision cone
        if (angleToPlayer < visionAngle / 2f)
        {
            Debug.Log("Player in cone");

            RaycastHit hit;
            // cast a ray to check for obstacles between AI and player
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
        // use the navmesh agent to chase the player
        if (player == null || agent == null)
        {
            return;
        }
        if (!agent.pathPending && agent.destination != player.position)
        {
            // set destination to player's position
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

        // if close enough to waypoint, move to next waypoint
        if (agent.remainingDistance < 0.02f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % patrolWaypoints.Length;
            // set destination to the next waypoint
            agent.SetDestination(patrolWaypoints[currentWaypointIndex].position);
        }

    }


}
