using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class characterMove : MonoBehaviour
{
   
    [SerializeField]
    float speed = 5;

    [SerializeField]
    float Gravity = 1f;
    [SerializeField]
    float jump = 5;
    [SerializeField]
    float MaxSpeed = 10;
    [SerializeField]
    float MinPenetrationForPanalty = 0.0001f;

    float upForce = 0;

    Rigidbody2D rigidbody2D;
   public Vector2 moveDir;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();

       if(MinPenetrationForPanalty < 0.0001f)
        {
            MinPenetrationForPanalty = 1f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * 1.001f, Color.cyan);
    }
    void FixedUpdate()
    {
        Move();

    }
    private void Move()
    {
        bool hasControl = (moveDir != Vector2.zero);

        if (hasControl)
        {
            rigidbody2D.drag = 0;
            //transform.position += new Vector3(moveDir.x * Time.deltaTime, 0, 0);
            rigidbody2D.AddForce(moveDir, ForceMode2D.Impulse);

            if (rigidbody2D.velocity.x > MaxSpeed )
            {
                rigidbody2D.velocity = new Vector2(MaxSpeed, rigidbody2D.velocity.y);
            }
            else if (rigidbody2D.velocity.x < - MaxSpeed)
            {
                rigidbody2D.velocity = new Vector2(-MaxSpeed, rigidbody2D.velocity.y);
            }
        }
        else
        {
            rigidbody2D.drag = 10;
        }

        
    }
    public void OnMove(InputValue value)
    {
        Vector2 input = value.Get<Vector2>();
        if (input != null)
        {
            moveDir = new Vector2(input.x, 0);
            Debug.Log($"SEND_MASSAGE : {input.magnitude}");
        }


    }

    public void OnJump(InputAction action)
    {
        if(action != null)
        {
            upForce = jump;
        }
    }

}
