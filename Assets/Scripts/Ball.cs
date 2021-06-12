using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using utils;

public class Ball : MonoBehaviour
{
    public UnityAction<Collision2D> onHitGround;
    public UnityAction<int> onChangeForce;
    public UnityAction onHitEnemy;

    // Force of ball
    [SerializeField]
    private float force1 = 100.0f;
    [SerializeField]
    private float force2 = 200.0f;
    [SerializeField]
    private float force3 = 300.0f;

    private float finalForce = 0;

    // condition of launch
    private bool holdDir = false;


    // Rotation Joystick Parameters
    private int yPosAxis = 0;
    private int xPosAxis = 0;
    private int yNegAxis = 0;
    private int xNegAxis = 0;


    private bool isYPosAxis = true;
    private bool isXPosAxis = true;
    private bool isYNegAxis = true;
    private bool isXNegAxis = true;

    [Space]
    [SerializeField]
    private float toleranceRotation = 0.5f;


    // Reflect 
    private Vector2 reflect;
    [Space]
    [SerializeField]
    private float reflectForce = 3000.0f;
    private bool checkRightTrigger = false;

    // Generals Properties 
    private Rigidbody2D rigid;
    [SerializeField]
    private BallTrigger ballTrigger;


    void Start()
    {
        rigid = gameObject.GetComponent<Rigidbody2D>();
        ballTrigger = gameObject.GetComponentInChildren<BallTrigger>();
    }

    void Update()
    {
        CheckBallPower();
        DirectionBall();

        if (ballTrigger.isReflect)
            SetVelocityOfReflect();
    }

    void CheckBallPower()
    {
        // Reset all check power 
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            ResetValueRotation();
            ResetBoolOneRotateDone();
        }

        if (finalForce != force3)
        {
            RotationCheck();
            SetupForceOfBall();
        }
    }

    void RotationCheck()
    {
        int maxRotate = 3;
        // Check Rotation of Joystick
        if (Input.GetAxis("Horizontal") > toleranceRotation && xPosAxis < maxRotate && isXPosAxis)
        {
            xPosAxis += 1;
            isXPosAxis = false;
        }
        if (Input.GetAxis("Horizontal") < -toleranceRotation && xNegAxis < maxRotate && isXNegAxis)
        {
            xNegAxis += 1;
            isXNegAxis = false;
        }
        if (Input.GetAxis("Vertical") > toleranceRotation && yPosAxis < maxRotate && isYPosAxis)
        {
            yPosAxis += 1;
            isYPosAxis = false;
        }
        if (Input.GetAxis("Vertical") < -toleranceRotation && yNegAxis < maxRotate && isYNegAxis)
        {
            yNegAxis += 1;
            isYNegAxis = false;
        }
    }

    void SetupForceOfBall()
    {
        int rotate1 = 1;
        int rotate2 = 2;
        int rotate3 = 3;

        // Easy way to check rotation joystick not the cleanest at all !  
        if (xPosAxis >= rotate1 && xNegAxis >= rotate1 && yPosAxis >= rotate1 && yNegAxis >= rotate1 && finalForce < force1)
        {
            finalForce = force1;
            print("1");
            onChangeForce?.Invoke(1);
            holdDir = true;
            ResetBoolOneRotateDone();
        }
        if (xPosAxis >= rotate2 && xNegAxis >= rotate2 && yPosAxis >= rotate2 && yNegAxis >= rotate2 && finalForce < force2)
        {
            finalForce = force2;
            print("2");
            onChangeForce?.Invoke(2);
            ResetBoolOneRotateDone();
        }
        if (xPosAxis >= rotate3 && xNegAxis >= rotate3 && yPosAxis >= rotate3 && yNegAxis >= rotate3 && finalForce < force3)
        {
            finalForce = force3;
            print("3");
            onChangeForce?.Invoke(3);
        }
    }

    void ResetBoolOneRotateDone()
    {
        isYPosAxis = true;
        isXPosAxis = true;
        isYNegAxis = true;
        isXNegAxis = true;
    }

    void ResetValueRotation()
    {
        yPosAxis = 0;
        xPosAxis = 0;
        yNegAxis = 0;
        xNegAxis = 0;
        finalForce = 0;
        onChangeForce?.Invoke(0);
    }

    void DirectionBall()
    {
        if (holdDir)
        {
            Vector2 dirJoystickLeft = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

            // just debug line for see direction 
            Debug.DrawRay(transform.position, new Vector3(dirJoystickLeft.x, dirJoystickLeft.y, 0) * 100, Color.green);

            if (Input.GetAxis("RightTrigger") != 0)
            {
                rigid.velocity = (dirJoystickLeft.normalized * finalForce);
                ResetValueRotation();
                ResetBoolOneRotateDone();
                holdDir = false;
            }
        }
    }

    // check ennemy if you touch kill it 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(TagList.groundTag))
        {
            onHitGround?.Invoke(collision);
            ReflectBall(collision.contacts[0].normal);
        }
        else if (collision.gameObject.CompareTag(TagList.enemyTag)) onHitEnemy?.Invoke();

    }

   
    void ReflectBall(Vector2 collisionNormal)
    {
        reflect = Vector2.Reflect(ballTrigger.saveVelocityBall.normalized, collisionNormal);
    }

    void SetVelocityOfReflect()
    {

        if (Input.GetAxis("RightTrigger") != 0 && !checkRightTrigger)
        {
            checkRightTrigger = true;
            rigid.velocity = new Vector2(0, 0);
            rigid.velocity = (reflect.normalized * reflectForce);
        }

        if (Input.GetAxis("RightTrigger") > -1)
            checkRightTrigger = false;
    }
}
