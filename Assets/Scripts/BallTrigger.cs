﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils;

public class BallTrigger : MonoBehaviour
{
    private Rigidbody2D ball;
    public Vector2 saveVelocityBall;
    public bool isReflect = false;

    [SerializeField]
    private float timerSpecialCapacity = 1.0f;
    private float saveTimerSC= 0f;

    public bool isChainReflect = false;
    public bool checkRightTrigger = false;



    // Start is called before the first frame update
    void Start()
    {
        ball = gameObject.GetComponentInParent<Rigidbody2D>();
        saveTimerSC = timerSpecialCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if(isReflect)
        {
            timerSpecialCapacity -= Time.deltaTime; 
        
            if(timerSpecialCapacity <= 0)
            {
                timerSpecialCapacity = saveTimerSC;
                isReflect = false;
                isChainReflect = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
 
        if (collision.CompareTag(TagList.groundTag))
        {
            saveVelocityBall = ball.velocity.normalized;
            isReflect = true;
            isChainReflect = true;
            checkRightTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagList.groundTag))
        {
            isReflect = false;
        }
    }
}
