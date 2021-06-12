using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollows : MonoBehaviour
{
    // SERIALIZED FIELDS
    [SerializeField] private Vector3 offset;

    // REFERENCES
    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Getting the player's transform
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerTransform.position + offset;
    }
}
