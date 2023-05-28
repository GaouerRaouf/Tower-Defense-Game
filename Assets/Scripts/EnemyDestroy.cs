using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDestroy : MonoBehaviour
{
    GameManager gameManager;
    private void Awake()
    {
        gameManager = GameManager.Instance;
    }



    Dictionary<GameObject, Coroutine> destroyCoroutines = new Dictionary<GameObject, Coroutine>(); // Declare a dictionary to store coroutines for each enemy

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            Coroutine destroyCoroutine = StartCoroutine(DestroyEnemy(enemy));
            destroyCoroutines.Add(enemy, destroyCoroutine);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            GameObject enemy = other.gameObject;
            if (destroyCoroutines.ContainsKey(enemy))
            {
                Coroutine destroyCoroutine = destroyCoroutines[enemy];
                StopCoroutine(destroyCoroutine); // Cancel the destruction coroutine
                destroyCoroutines.Remove(enemy);
            }
        }
    }

    IEnumerator DestroyEnemy(GameObject enemy)
    {
        yield return new WaitForSeconds(15f);

        if (gameManager.IsPlayerAlive()) // Check if the player is still alive
        {
            gameManager.Penalty();
        }

        Destroy(enemy);
    }

}