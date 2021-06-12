using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BirdBehavior : MonoBehaviour
{
    //UNITY ACTION
    public UnityAction OnRun;
    public UnityAction OnDeath;

    // FIELDS
    // SERIALIZED FIELDS
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float deathLimit = 30f;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private bool hasLetter = false;
    [SerializeField] private bool isCycling = false;

    // PRIVATE FIELDS
    private float birdSpeed;
    private bool goingTowardFinish = true;
    private bool canMove = false;
    private Vector3 direction;

    // REFERENCES
    private Animator birdAnimator;
    [SerializeField] private GameObject letter;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject finish;


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

        // Start time for spawning
        StartCoroutine(Spawner());

        // References
        birdAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            BirdMovement();

            // Destroy the game object if it cross the death boundery
            if (Mathf.Abs(transform.position.x) >= deathLimit)
            {
                Destroy(gameObject);
            }
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
            }
            else if (Vector3.Distance(transform.position, start.transform.position) < 0.1f && !goingTowardFinish)
            {
                direction *= -1;
                goingTowardFinish = true;
            }
        }
        else
        {
            // Just stop the bird at the finish point
            if (Vector3.Distance(transform.position, finish.transform.position) < 0.1f && birdSpeed != 0)
            {
                birdSpeed = 0;

                // In the case of the letter bird
                Debug.Log("ALERT!!!");
            }
        }
        // Move the bird
        transform.Translate(direction * Time.deltaTime * birdSpeed);
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSecondsRealtime(spawnTime);

        canMove = true;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }

        if (collision.gameObject.CompareTag("Ball"))
        {
            birdAnimator.SetBool("isDead", true);
            birdSpeed = 0f;

            // SFX
            OnDeath?.Invoke();
        }
    }
}
