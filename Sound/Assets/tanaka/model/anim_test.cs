using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class anim_test : MonoBehaviour
{
    public float testSpeed = 3.0f;
    private Rigidbody rb;
    public float jumpSpeed;
    private bool isJumping = false;
    //private bool isTackel = false;
    private Animator animator;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Dash", false);
        if (Input.GetKey("up") || Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * testSpeed * Time.deltaTime;
            animator.SetBool("Dash", true);

        }
        if (Input.GetKey("down") || Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * testSpeed * Time.deltaTime;
            animator.SetBool("Dash", true);

        }
        if (Input.GetKey("right")||Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * testSpeed * Time.deltaTime;
            animator.SetBool("Dash", true);

        }
        if (Input.GetKey("left") || Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * testSpeed * Time.deltaTime;
            animator.SetBool("Dash", true);

        }
        if (Input.GetKey(KeyCode.Space)&&isJumping==false)
        {
            rb.velocity = Vector3.up * jumpSpeed;
            animator.SetBool("Jump", true);
            isJumping = true;
        }
        animator.SetBool("Tackle", false);
        if (Input.GetKey(KeyCode.E))
        {
            //isTackel = true;
            animator.SetBool("Tackle", true);

        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
            animator.SetBool("Jump", false);
        }
    }
}
