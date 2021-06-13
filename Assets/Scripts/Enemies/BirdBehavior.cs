using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdBehavior : MonoBehaviour
{
    //UNITY ACTION
    public UnityAction OnRun;
    public UnityAction OnDeath;
    public UnityAction OnFlip;
    public UnityAction OnStopMoving;

    // FIELDS
    // SERIALIZED FIELDS
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float deathLimit = 30f;
    public bool hasLetter = false;
    [SerializeField] private bool isCycling = false;

    // PRIVATE FIELDS
    private float birdSpeed;
    private bool goingTowardFinish = true;
    private Vector3 direction;

    // REFERENCES
    private Animator birdAnimator;
    [Space]
    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject finish;
    [SerializeField] private GameObject[] guards;
    private Ball ballScript;

    [Header("Score")]
    [SerializeField] private int birdPoint = 100;
    [SerializeField] private int letterBirdPoint = 150;


    // Start is called before the first frame update
    void Start()
    {
        birdSpeed = Random.Range(minSpeed, maxSpeed);
        OnRun?.Invoke();

        // Letter manager
        if (hasLetter)
        {
            letter.SetActive(true);
        }
        else
        {
            letter.SetActive(false);
        }

        // Set the position of the bird to the starting point
        transform.position = start.transform.position;

        // Getting the moving direction
        direction = Vector3.Normalize(finish.transform.localPosition - start.transform.localPosition);

        // References
        birdAnimator = GetComponent<Animator>();
        ballScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        BirdMovement();

        // Destroy the game object if it cross the death boundery
        if (Mathf.Abs(transform.position.x) >= deathLimit)
        {
            Destroy(gameObject);
        }
    }

    private void BirdMovement()
    {
        // If cycling, we reverse the direction each time SP or FP is met
        if (isCycling)
        {

            if (Vector3.Distance(transform.position, finish.transform.position) < 0.1f && goingTowardFinish)
            {
                direction *= -1;
                goingTowardFinish = false;
                OnFlip?.Invoke();
            }
            else if (Vector3.Distance(transform.position, start.transform.position) < 0.1f && !goingTowardFinish)
            {
                direction *= -1;
                goingTowardFinish = true;
                OnFlip?.Invoke();
            }
        }
        else
        {
            // Just stop the bird at the finish point
            if (Vector3.Distance(transform.position, finish.transform.position) < 0.1f && birdSpeed != 0)
            {
                birdSpeed = 0;
                OnStopMoving?.Invoke();

                // In the case of the letter bird
                if (hasLetter)
                {
                    // Guards will spawn
                    foreach (GameObject guard in guards)
                    {
                        guard.SetActive(true);
                    }
                }
            }
        }
        // Move the bird
        transform.Translate(direction * Time.deltaTime * birdSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player.getInstance().TakeDamage();
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            if (ballScript.isMoving)
            {
                birdAnimator.SetBool("isDead", true);
                birdSpeed = 0f;
                GetComponent<Collider2D>().enabled = false;

                // SFX
                OnDeath?.Invoke();
                int lScore = hasLetter ? letterBirdPoint : birdPoint;
                GameManager.getInstance().IncreaseScore(lScore);
            }
        }
    }
}