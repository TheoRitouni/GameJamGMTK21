using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigidBody;

    //Movements Player Settings
    [SerializeField]
    private float speed = 1.0f;


    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovementPlayer();

    }

    void BasicMovementPlayer()
    {

        if (Input.GetAxis("Horizontal") < 0)
            rigidBody.velocity = new Vector2(-speed * Time.deltaTime, 0);


        if (Input.GetAxis("Horizontal") > 0)
            rigidBody.velocity = new Vector2(speed * Time.deltaTime, 0);


        if (Input.GetAxis("Horizontal") == 0)
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
    }

    void RotationBall()
    {

    }

    void LaunchBall()
    {

    }

}
