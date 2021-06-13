using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTime : MonoBehaviour
{
    // FIELDS

    // SERIALIZE FIELDS

    [SerializeField] private float spawnTime = 2f;
    [SerializeField] ParticleSystem particlesSpawn;

    // REFERENCES
    [SerializeField] private GameObject prefab;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSecondsRealtime(spawnTime);
        if (particlesSpawn != null)
        {
            particlesSpawn.transform.SetParent(null);
            particlesSpawn.Play();
        }

        prefab.SetActive(true);

    }

}
