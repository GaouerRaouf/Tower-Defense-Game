using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{

    public static GameManager Instance;

    [Header("Spawn management")]
    [SerializeField] GameObject[] enemiesToSpawn;
    private GameObject[] enemies;
    [SerializeField] float spawnRate;



    [Header("UI")]
    [SerializeField] TextMeshProUGUI WaveText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] PlayerMovement player;
    [SerializeField] GameObject restartButton;
    private int lives = 3;
    private int waveNum = 1;
    private bool waveInProgress = false;

   
    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        Instance = this;
        StartCoroutine(ShowWaveNum());
        StartCoroutine(SpawnWave());
    }

    public void Penalty()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log(lives);
        }
        livesText.text = "Lives: " + lives;
        if (lives == 0)
        {
            GameOver();
        }
    }
    public void Restart()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        lives = 3;
        waveNum = 1;
        StartCoroutine(ShowWaveNum());
        StartCoroutine(SpawnWave());
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (!waveInProgress && enemies.Length == 0 && player.enabled ==true)
        {
            waveInProgress = true;
            StartCoroutine(SpawnWave());
        }
    }

    IEnumerator ShowWaveNum()
    {
        WaveText.gameObject.SetActive(true);
        player.PlayerAnimator.SetBool("Walk", false);
        player.PlayerAnimator.SetBool("Running", false);
        restartButton.SetActive(false);
        WaveText.text = "Wave " + waveNum;
        player.enabled = false;
        yield return new WaitForSeconds(2f);
        WaveText.text = "Start";
        yield return new WaitForSeconds(1f);
        livesText.text = "Lives: " + lives;
        WaveText.gameObject.SetActive(false);
        player.enabled = true;     
    }

    IEnumerator SpawnWave()
    {
        int enemiesToSpawnCount = waveNum + 3;
        for (int i = 0; i < enemiesToSpawnCount; i++)
        {
            int randomEnemy = Random.Range(0, enemiesToSpawn.Length);
            Instantiate(enemiesToSpawn[randomEnemy]);
            yield return new WaitForSeconds(spawnRate);
        }

        yield return new WaitForSeconds(1f); 

        while (enemies.Length > 0)
        {
            yield return null; 
        }

        waveNum++;
        waveInProgress = false;
        StartCoroutine(ShowWaveNum());
    }

    private void GameOver()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        WaveText.gameObject.SetActive(true);
        WaveText.text = "Game Over";

        restartButton.SetActive(true);
        player.enabled = false;
        StopAllCoroutines();

        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

    public bool IsPlayerAlive()
    {
        return lives > 0;
    }
}
