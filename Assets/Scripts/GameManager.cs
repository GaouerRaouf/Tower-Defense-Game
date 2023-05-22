using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    

    [Header("Spawn management")]
    [SerializeField] GameObject[] enemiesToSpawn;
    [SerializeField] float spawnRate;

    // Start is called before the first frame update
    void Start()
    {        
        StartCoroutine(SpawnEnemies());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            Debug.Log("idk");
            int randomEnemy = Random.Range(0, 5);
          //  Vector3 position = enemiesToSpawn[randomEnemy].GetComponent<WaypointNavigator>().currentWaypoint.GetPosition();
            Instantiate(enemiesToSpawn[randomEnemy], new Vector3(-40f, 4.13f, -0.48f), transform.rotation);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
