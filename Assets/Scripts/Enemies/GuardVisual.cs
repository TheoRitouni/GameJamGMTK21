using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVisual : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] GuardBehavior guard;

    [Header("FX")]
    [SerializeField] AudioSource audioRun;
    [SerializeField] AudioSource audioDeath;
    [SerializeField] ParticleSystem psRun;
    [SerializeField] ParticleSystem psDeath;

    private void Start()
    {
        guard.OnRun += OnRun;
        guard.OnDeath += OnDeath;
    }

    void OnRun()
    {
        //PLAY SFX
        audioRun.Play();
        psRun.Play();
    }

    void OnDeath()
    {
        //STOP RUN FX
        audioRun.Stop();
        psRun.Stop();

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
