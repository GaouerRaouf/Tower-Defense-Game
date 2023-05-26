using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("Spawn management")]
    [SerializeField] GameObject[] enemiesToSpawn;
    [SerializeField] float spawnRate;


    [Header("UI")]
    [SerializeField] TextMeshProUGUI WaveText;

    private void Awake()
    {
        Instance = this;
    }

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
            int randomEnemy = Random.Range(0, 6);
            Instantiate(enemiesToSpawn[randomEnemy]);
            yield return new WaitForSeconds(spawnRate);
        }
    }
}
