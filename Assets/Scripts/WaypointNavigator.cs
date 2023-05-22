using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigator : MonoBehaviour
{
    [Header("AI Character")]
    EnemyMovement character;
    [SerializeField] Waypoint currentWaypoint;
    public GameObject[] initialWaypoints;


    private void Awake()
    {
        character = GetComponent<EnemyMovement>();
        initialWaypoints = GameObject.FindGameObjectsWithTag("FirstWaypoint");
        Debug.Log(initialWaypoints[0]);
    }

    private void Start()
    {
        currentWaypoint = GetInitialWaypoint();
        character.LocateDestination(currentWaypoint.GetPosition());
    }

    private void Update()
    {
        if (character.destinationReached)
        {
            currentWaypoint = currentWaypoint.nextWaypoint;
            character.LocateDestination(currentWaypoint.GetPosition());
        }
    }

    public Waypoint GetInitialWaypoint()
    {
        int randomInitialWaypoint = Random.Range(0,2);
        return initialWaypoints[randomInitialWaypoint].GetComponent<Waypoint>();
    }

    
}
