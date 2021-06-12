using System.Collections;
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
            timerSpecialCapacity -= Time.unscaledTime;
        
            if(timerSpecialCapacity <= 0)
            {
                timerSpecialCapacity = saveTimerSC;
                isReflect = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(TagList.groundTag))
        {
            saveVelocityBall = ball.velocity;
            isReflect = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(TagList.groundTag))
            isReflect = false;
    }
}
