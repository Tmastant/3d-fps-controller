using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;
    public float jumpHeight;
    public BoxCollider feet;
    public CapsuleCollider col;

    private Rigidbody rb;
    private bool canJump = true;

    public float Energy = 1000;

    Vector3 movedir;
    Vector3 altdir;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Physics.IgnoreCollision(col, feet);
    }

    void Update()
    {
        float hormove = Input.GetAxisRaw("Horizontal");
        float vermove = Input.GetAxisRaw("Vertical");

        if (canJump)
        {
            movedir = (hormove * transform.right + vermove * transform.forward).normalized;
        }
        else if (!canJump)
        {
            altdir = (hormove * transform.right + vermove * transform.forward).normalized;
        }


        if (canJump && Input.GetKey(KeyCode.Space))  ///Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        OnTriggerStay(feet);
        OnTriggerExit(feet);


    }
    

    void OnTriggerStay(Collider other)
    {
        canJump = true;
    }

    private void OnTriggerExit(Collider other)
    {
        canJump = false;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
    }

    void FixedUpdate()
    {
        Move();
        if (Energy > 1000) Energy = 1000;
    }

    void Move()
    {
        Vector3 yfix = new Vector3(0, rb.velocity.y, 0);
        if (Input.GetKey(KeyCode.LeftShift) && Energy > 0)
        {
            rb.velocity = movedir * runSpeed * Time.deltaTime;
            if (!canJump)
            {
                rb.velocity = rb.velocity + (altdir * runSpeed * Time.deltaTime) / 50;
            }
            if(movedir != Vector3.zero)
            {
                Energy-=2f;
            }
        }
        else
        {
            rb.velocity = movedir * walkSpeed * Time.deltaTime;
            if (!canJump)
            {
                rb.velocity = rb.velocity + (altdir * walkSpeed * Time.deltaTime) / 50;
            }
            Energy += 0.25f;
        }
        if (movedir == Vector3.zero) Energy += 0.5f;

        
        rb.velocity += yfix;
        
    }
    


}
