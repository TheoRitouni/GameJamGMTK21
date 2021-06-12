using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // FIELDS

    // SERIALIZE FIELDS
    [SerializeField] private float spawnPosX = 40f;

    // GUARD
    [SerializeField] private float minSpawnRateGuard = 0.5f;
    [SerializeField] private float maxSpawnRateGuard = 2f;
    [SerializeField] private float guardSpawnPosY = 2f;
    [SerializeField] private GameObject guardPrefab;

    // BIRD
    [SerializeField] private float minSpawnRateBird = 0.5f;
    [SerializeField] private float maxSpawnRateBird = 2f;
    [SerializeField] private float minBirdSpawnPosY = 3f;
    [SerializeField] private float maxBirdSpawnPosY = 6f;
    [SerializeField] private GameObject birdPrefab;

    // LETTER BIRD
    [SerializeField] private float letterBirdProba = 0.2f;
    [SerializeField] private GameObject letterBirdPrefab;

    // ALERT
    public bool isAlert = false;
    [SerializeField] private float alertSpawnRateGuard = 0.6f;
    [SerializeField] private int alertSize = 20;
    public int alertCounter = 0;

    // PRIVATE FIELDS
    private float spawnRateGuard;
    private float spawnRateBird;
    private bool isGuardSpawning = false;
    private bool isBirdSpawning = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // GUARD SPAWNER
        if (!isGuardSpawning)
        {
            // The spawn rate changes if the alert is on
            if (!isAlert)
            {
                spawnRateGuard = Random.Range(minSpawnRateGuard, maxSpawnRateGuard);
            }
            else
            {
                spawnRateGuard = alertSpawnRateGuard;

                // Keeping track of the alert :
                // When the number of enemies reaches the alert size
                // Alert mode is over
                alertCounter++;
                Debug.Log(alertCounter);
                if (alertCounter >= alertSize)
                {
                    isAlert = false;
                    alertCounter = 0;
                }
            }

            StartCoroutine(SpawnGuard(spawnRateGuard));
            isGuardSpawning = true;
        }

        // BIRDS SPAWNER
        if (!isBirdSpawning)
        {
            StartCoroutine(SpawnBird());
            isBirdSpawning = true;
        }
    }

    private IEnumerator SpawnGuard(float spawnRateGuard)
    {
        yield return new WaitForSeconds(spawnRateGuard);

        // Instantiate the guard and allows another respawn
        Instantiate(guardPrefab, randomStart("Guard"), guardPrefab.transform.rotation);
        isGuardSpawning = false;
    }

    private IEnumerator SpawnBird()
    {
        spawnRateBird = Random.Range(minSpawnRateBird, maxSpawnRateBird);

        yield return new WaitForSeconds(spawnRateBird);

        // Instantiate the bird and allows another respawn
        if(Random.Range(0f, 1f) < letterBirdProba)
        {
            Instantiate(letterBirdPrefab, randomStart("Bird"), letterBirdPrefab.transform.rotation);
        }
        else
        {
            Instantiate(birdPrefab, randomStart("Bird"), birdPrefab.transform.rotation);
        }
        isBirdSpawning = false;
    }

    private Vector2 randomStart(string enemyType) 
    {
        // Variables
        Vector2 randomVector;
        float randomNumber = Random.Range(0f, 1f);

        // Changes the spawn position in function of the enemy's type
        switch (enemyType)
        {
            case "Guard":
                if(randomNumber <= 0.5f)
                {
                    // Spawn on the left side
                    return randomVector = new Vector2(-spawnPosX, guardSpawnPosY);
                }
                else
                {
                    // Spawn on the right side
                    return randomVector = new Vector2(spawnPosX, guardSpawnPosY);
                }

            case "Bird":
                if (randomNumber <= 0.5f)
                {
                    // Spawn on the left side with random height
                    return randomVector = new Vector2(-spawnPosX, Random.Range(minBirdSpawnPosY, maxBirdSpawnPosY));
                }
                else
                {
                    // Spawn on the right side with random height
                    return randomVector = new Vector2(spawnPosX, Random.Range(minBirdSpawnPosY, maxBirdSpawnPosY));
                }

        }

        return Vector2.zero;
    }
}
