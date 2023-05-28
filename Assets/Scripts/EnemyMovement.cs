using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    GameManager gameManager;

    [Header("CharacterInfo")]
    public float movingSpeed;
    public float turningSpeed;
    public float stopSpeed;

    [Header("Destination Variables")]
    public Vector3 destination;
    public bool destinationReached;
    bool isTriggered = false;

    [Header("Animators")]
    [SerializeField] Animator enemyAnimator;
    private Animator playerAnimator;
    private float deathTime = 3f;


    private void Start()
    {
        gameManager = GameManager.Instance;
        playerAnimator = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        enemyAnimator.SetFloat("Speed", movingSpeed);
    }
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
            enemyAnimator.SetFloat("Speed", movingSpeed);

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

    public int health = 100;
    

    public void TakeSwordHit(int swordDamage)
    {
        if (playerAnimator.GetBool("AttackBool"))
        {
            health -= 1;
            if (health <= 0)
            {
                StartCoroutine(Die());
            }
            else
            {
                TakeAttacks();
            }
        }
    }

    private void TakeAttacks()
    {
        enemyAnimator.SetTrigger("Hit");
        Debug.Log("Enemy attacks!");
        
    }

    IEnumerator Die()
    {
        enemyAnimator.SetBool("Dead", true);
        yield return new WaitForSeconds(deathTime);
        Destroy(gameObject); 
    }
    IEnumerator DieOnTrigger()
    {
        if (isTriggered)
        {
            yield return new WaitForSeconds(15);
            gameManager.Penalty();
            isTriggered = false;
            Destroy(gameObject);
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Dead"))
        {
            isTriggered = true; 
            StartCoroutine(DieOnTrigger());
        }
    }
}
