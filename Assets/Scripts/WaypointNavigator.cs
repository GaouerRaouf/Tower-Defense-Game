using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{

    GameManager gameManager;
    [Header("AI Character")]
    EnemyMovement character;
    [SerializeField] Waypoint currentWaypoint;
    public GameObject[] initialWaypoints;
    private Animator animator;

    private void Awake()
    {
        character = GetComponent<EnemyMovement>();
        gameManager = GameManager.Instance;
        initialWaypoints = GameObject.FindGameObjectsWithTag("FirstWaypoint");
        currentWaypoint = GetInitialWaypoint();
        transform.position = GetInitialPosition();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {       
        character.LocateDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (character.destinationReached)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            character.LocateDestination(currentWaypoint.GetPosition());
        }
        if (character.destinationReached && currentWaypoint == currentWaypoint.nextWaypoint)
        {
            animator.SetBool("Attack", true);
        }
    }

    public Waypoint GetInitialWaypoint()
    {
        int randomInitialWaypoint = Random.Range(0,2);
        return initialWaypoints[randomInitialWaypoint].GetComponent<Waypoint>();
    }
    public Vector3 GetInitialPosition()
    {
        return currentWaypoint.GetPosition();
    }
    
}
