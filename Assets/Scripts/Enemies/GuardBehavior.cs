using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GuardBehavior : MonoBehaviour
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
    [Space]
    [SerializeField] private bool isCycling = false;

    // PRIVATE FIELDS
    private float guardSpeed;
    private bool goingTowardFinish = true;
    private Vector3 direction;
    private Animator guardAnimator;

    // REFERENCES
    [Space]
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject finish;

    [Header("Score")]
    [SerializeField] private int guardPoint = 50;
    private Ball ballScript;


    // Start is called before the first frame update
    void Start()
    {
        guardSpeed = Random.Range(minSpeed, maxSpeed);
        OnRun?.Invoke();

        // Set the position of the bird to the starting point
        transform.position = start.transform.position;

        // Getting the moving direction
        direction = Vector3.Normalize(finish.transform.localPosition - start.transform.localPosition);

        // References
        guardAnimator = GetComponent<Animator>();
        ballScript = GameObject.FindGameObjectWithTag("Ball").GetComponent<Ball>();
    }

    // Update is called once per frame
    void Update()
    {
        GuardMovement();

        // Destroy the game object if it cross the death boundery
        if (Mathf.Abs(transform.position.x) >= deathLimit)
        {
            Destroy(gameObject);
        }
    }

    private void GuardMovement()
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
            if (Vector3.Distance(transform.position, finish.transform.position) < 0.2f)
            {
                guardSpeed = 0;
                OnStopMoving?.Invoke();
                guardAnimator.SetBool("stopMoving", true);
            }
        }
        // Move the bird
        transform.Translate(direction * Time.deltaTime * guardSpeed);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player takes damage or is dead
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.getInstance().GameOver();
        }

        // Enemy is dead
        if (collision.gameObject.CompareTag("Ball"))
        {

            if (ballScript.isMoving)
            {
                GameManager.getInstance().IncreaseScore(guardPoint);
                guardAnimator.SetBool("isDead", true);
                guardSpeed = 0f;
                GetComponent<Collider2D>().enabled = false;

                // SFX
                OnDeath?.Invoke();
            }
        }
    }
}
