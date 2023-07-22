using System.Collections;
using UnityEngine;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public Transform spawnPoint;
    public float timeBetweenWaves = 5f;

    public TextMeshProUGUI waveNumberText; // Aggiornato il tipo della variabile

    private float countdownTimer = 2f;
    private bool isSpawning = false;

    void Start()
    {
        // Azzeriamo il numero di ondate raggiunte salvato nei PlayerPrefs
        PlayerPrefs.DeleteKey("WaveCount");
        PlayerPrefs.Save();

        // Inizializziamo il numero di wave corrente a 0 all'inizio della partita
        GameData.CurrentWave = 0;
    }

    void Update()
    {
        if (!isSpawning)
        {
            countdownTimer -= Time.deltaTime;

            if (countdownTimer <= 0f)
            {
                StartCoroutine(SpawnWave());
                countdownTimer = timeBetweenWaves;
            }
        }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;

        // Incrementiamo il numero di wave corrente usando GameData
        GameData.CurrentWave++;
        int waveIndex = GameData.CurrentWave;

        waveNumberText.text = "Wave: " + waveIndex; // Aggiorna il testo del componente TextMeshProUGUI

        int enemyCount = waveIndex * 2;
        int enemiesSpawned = 0;

        while (enemiesSpawned < enemyCount)
        {
            SpawnEnemy();
            enemiesSpawned++;
            yield return new WaitForSeconds(1f);
        }

        yield return StartCoroutine(WaitForEnemiesToBeDefeated());

        isSpawning = false;
    }

    IEnumerator WaitForEnemiesToBeDefeated()
    {
        while (true)
        {
            int activeEnemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

            if (activeEnemyCount == 0)
            {
                Debug.Log("Wave " + GameData.CurrentWave + " completed!");

                // Salviamo il numero di wave raggiunto nei PlayerPrefs
                PlayerPrefs.SetInt("WaveCount", GameData.CurrentWave);
                PlayerPrefs.Save();

                break;
            }

            yield return null;
        }
    }

    void SpawnEnemy()
    {
        int randomIndex = Random.Range(0, enemyPrefabs.Length);
        GameObject enemyPrefab = enemyPrefabs[randomIndex];
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
    }
}