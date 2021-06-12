using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdVisual : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] BirdBehavior bird;

    [Header("FX")]
    [SerializeField] AudioSource audioFly;
    [SerializeField] AudioSource audioDeath;
    [SerializeField] ParticleSystem psFly;
    [SerializeField] ParticleSystem psDeath;

    private void Start()
    {
        bird.OnRun += OnRun;
        bird.OnDeath += OnDeath;
    }

    void OnRun()
    {
        //PLAY SFX
        audioFly.Play();
        psFly.Play();
    }

    void OnDeath()
    {
        //STOP RUN FX
        audioFly.Stop();
        psFly.Stop();

        //SET PARENT NULL
        audioDeath.transform.SetParent(null);
        audioDeath.transform.localScale = Vector3.one;
        psDeath.transform.SetParent(null);
        psDeath.transform.localScale = Vector3.one;

        //PLAY DEATH FX
        audioDeath.Play();
        psDeath.Play();
    }
}
