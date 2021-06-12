using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level : MonoBehaviour
{
    private const string PARENT_NAME = "--- CURRENT LEVEL ---";

    private static Level currentLevel;
    public UnityAction<Vector3> onObjectSpawn;

    [SerializeField] float _levelTimerMax = 10f;

    [Header("Object spawn")]
    [SerializeField] List<GameObject> objectToSpawn;
    [SerializeField] float timeBetweenSpawns = 0.5f;
    [SerializeField] float timeBeforeSpawn = 2;

    private void Awake()
    {
        currentLevel = this;
        StartCoroutine(SpawnObjects());

        Transform lParent = GameObject.Find(PARENT_NAME).transform;
        transform.parent = lParent != null ? lParent : new GameObject(PARENT_NAME).transform;
    }

    IEnumerator SpawnObjects()
    {
        Time.timeScale = 0;

        foreach (GameObject lObject in objectToSpawn)
            lObject.SetActive(false);

        yield return new WaitForSecondsRealtime(timeBeforeSpawn);

        foreach (GameObject lObject in objectToSpawn)
        {
            onObjectSpawn?.Invoke(lObject.transform.position);
            lObject.SetActive(true);
            yield return new WaitForSecondsRealtime(timeBetweenSpawns);
        }

        Time.timeScale = 1;
    }

    IEnumerator DespawnObjects()
    {
        Time.timeScale = 0;

        foreach (GameObject lObject in objectToSpawn)
        {
            onObjectSpawn?.Invoke(lObject.transform.position);
            lObject.SetActive(false);
            yield return new WaitForSecondsRealtime(timeBetweenSpawns);
        }

        Destroy(gameObject);
        Time.timeScale = 1;
    }

    public float UnLoad()
    {
        StartCoroutine(DespawnObjects());
        return objectToSpawn.Count * timeBetweenSpawns;
    }

    public static Level getCurrentLevel() => currentLevel;

    public float levelTimerMax => _levelTimerMax;
}