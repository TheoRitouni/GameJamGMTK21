using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    // FIELDS

    // SERIALIZED FIELDS
    [SerializeField] private float speed = 10f;
    // PRIVATE FIELDS
    private float horizontalInput;
    private float verticalInput;

    // Update is called once per frame
    void Update()
    {
        // Getting input information
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        // Player movement
        gameObject.transform.Translate(Vector2.right * Time.deltaTime * horizontalInput * speed);
        gameObject.transform.Translate(Vector2.up * Time.deltaTime * verticalInput * speed);
    }
}
