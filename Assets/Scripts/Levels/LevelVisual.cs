using UnityEngine;

public class LevelVisual : MonoBehaviour
{
    private Level level;

    [SerializeField] ParticleSystem spawnParticles;
    [SerializeField] AudioSource spawnAudioSource;
    [SerializeField] ShakeData shakeOnSpawn;

    private void Start()
    {
        level = GetComponent<Level>();
        level.onObjectSpawn += OnObjectSpawn;
    }

    void OnObjectSpawn(Vector3 pPosition)
    {
        ParticleSystem lParticles = Instantiate(spawnParticles);
        lParticles.transform.position = pPosition;
        lParticles.Play();
        ShakeManager.getInstance().Shake(shakeOnSpawn);

        spawnAudioSource?.Play();
    }
}
