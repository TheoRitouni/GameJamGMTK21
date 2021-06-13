using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardVisual : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] GuardBehavior guard;
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Waypoint")]
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject finishPoint;

    [Header("FX")]
    [SerializeField] AudioSource audioRun;
    [SerializeField] AudioSource audioDeath;
    [SerializeField] ParticleSystem psRun;
    [SerializeField] ParticleSystem psDeath;

    private void Start()
    {
        guard.OnRun += OnRun;
        guard.OnDeath += OnDeath;
        guard.OnFlip += OnFlip;
        guard.OnStopMoving += OnStopMoving;

        if (startPoint.transform.position.x >= finishPoint.transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (startPoint.transform.position.x < finishPoint.transform.position.x)
        {
            spriteRenderer.flipX = true;
        }


    }

    void OnRun()
    {
        //PLAY SFX
        audioRun.Play();
        psRun.Play();
    }

    void OnFlip()
    {
        if (spriteRenderer.flipX) spriteRenderer.flipX = false;
        else if (!spriteRenderer.flipX) spriteRenderer.flipX = true;
    }

    void OnStopMoving()
    {
        if (audioRun != null) audioRun.Stop();
        if(psRun != null) psRun.Stop();
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
        if (audioDeath != null) audioDeath.Play();
        if (psDeath != null) psDeath.Play();
    }
}
