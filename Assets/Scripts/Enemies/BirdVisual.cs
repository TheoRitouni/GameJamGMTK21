using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdVisual : MonoBehaviour
{
    [Header("Enemies")]
    [SerializeField] BirdBehavior bird;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] SpriteRenderer spriteRendererLetter;

    [Header("Waypoint")]
    [SerializeField] GameObject startPoint;
    [SerializeField] GameObject finishPoint;

    [Header("FX")]
    [SerializeField] AudioSource audioFly;
    [SerializeField] AudioSource audioDeath;
    [SerializeField] ParticleSystem psFly;
    [SerializeField] ParticleSystem psDeath;
    [SerializeField] Color letterColor;

    private void Start()
    {
        bird.OnRun += OnRun;
        bird.OnDeath += OnDeath;

        if(startPoint.transform.position.x >= finishPoint.transform.position.x)
        {
            spriteRenderer.flipX = false;
            spriteRendererLetter.flipX = false;
            spriteRendererLetter.transform.localPosition = new Vector3(-2f, 0.33f, transform.position.z);
        }
        else if (startPoint.transform.position.x < finishPoint.transform.position.x)
        {
            spriteRenderer.flipX = true;
            spriteRendererLetter.flipX = true;
            spriteRendererLetter.transform.localPosition = new Vector3(2f, 0.33f, transform.position.z);
        }

        if (bird.hasLetter)
        {
            spriteRenderer.color = letterColor;
        }
        else if (!bird.hasLetter)
        {
            spriteRenderer.color = Color.white;
        }

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
