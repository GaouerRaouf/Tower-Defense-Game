using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [Header("CharacterInfo")]
    public float movingSpeed;
    public float turningSpeed;
    public float stopSpeed;

    [Header("Destination Variables")]
    public Vector3 destination;
    public bool destinationReached;

    [Header("Animators")]
   // [SerializeField] Animator playerAnimator;
    [SerializeField] Animator enemyAnimator;
    
    private void Update()
    {
        Walk();
    }

    public void Walk()
    {
        if(transform.position != destination)
        {
            Vector3 destinationDirection = destination - transform.position;
            float destinationDistance = destinationDirection.magnitude;


            if (destinationDistance >= stopSpeed)
            {
                destinationReached = false;
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, turningSpeed * Time.deltaTime);

                transform.Translate(Vector3.forward * movingSpeed * Time.deltaTime);
            }
            else
            {
                destinationReached = true;
            }
        }       
    }

    public void LocateDestination(Vector3 destination)
    {
        this.destination = destination;
        destinationReached = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("Sword"))
      {
            Debug.Log("idk");
      }
    }

}
