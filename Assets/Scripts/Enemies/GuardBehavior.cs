using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardBehavior : MonoBehaviour
{
    // FIELDS
    // SERIALIZED FIELDS
    [SerializeField] private float minSpeed = 5f;
    [SerializeField] private float maxSpeed = 15f;
    [SerializeField] private float deathLimit = 30f;
    [SerializeField] private float spawnTime = 2f;
    [SerializeField] private bool isCycling = false;

    // PRIVATE FIELDS
    private float guardSpeed;
    private bool goingTowardFinish = true;
    private bool canMove = false;
    private Vector3 direction;

    // REFERENCES
    private Animator guardAnimator;
    private AudioSource guardAudioSource;
    [SerializeField] private GameObject start;
    [SerializeField] private GameObject finish;
    private UIManager uiManagerScript;

    // SOUNDS
    [SerializeField] private AudioClip deathSound;

    // Start is called before the first frame update
    void Start()
    {
        guardSpeed = Random.Range(minSpeed, maxSpeed);

        // Set the position of the bird to the starting point
        transform.position = start.transform.position;

        // Getting the moving direction
        direction = Vector3.Normalize(finish.transform.localPosition - start.transform.localPosition);

        // Start time for spawning
        StartCoroutine(Spawner());

        // References
        guardAnimator = GetComponent<Animator>();
        guardAudioSource = GetComponent<AudioSource>();
        uiManagerScript = GameObject.Find("UIManager").GetComponent<UIManager>();
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
            if (Vector3.Distance(transform.position, finish.transform.position) < 0.1f)
            {
                guardSpeed = 0;
            }
        }
        // Move the bird
        transform.Translate(direction * Time.deltaTime * guardSpeed);
    }

    private IEnumerator Spawner()
    {
        yield return new WaitForSecondsRealtime(spawnTime);

        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Player takes damage or is dead
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Game Over");
        }

        // Enemy is dead
        if (collision.gameObject.CompareTag("Ball"))
        {
            guardAnimator.SetBool("isDead", true);
            guardSpeed = 0f;

            // SFX
            guardAudioSource.Stop();
            guardAudioSource.PlayOneShot(deathSound);

            // UP DATE SCORE
            uiManagerScript.UpdateScore("Guard");
        }
    }
}
