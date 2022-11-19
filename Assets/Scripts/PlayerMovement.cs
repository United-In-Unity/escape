using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    Collider cl;
    Animator anim;

    public bool alive = true;

    public float walkSpeed = 4.0f;
    public float jumpSpeed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<Collider>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive){
            WalkHandler();
            JumpHandler();
        }
        if (Input.GetButtonDown("Fire1")) {
            print("die!!!!!!!!!!!!!");
            // PlayerManager.instance.Die();
        }
    }

    void WalkHandler() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(hInput) > 0.01) hInput = Mathf.Sign(hInput);
        else hInput = 0;
        if (Mathf.Abs(vInput) > 0.01) vInput = Mathf.Sign(vInput);
        else vInput = 0;
        anim.SetBool("isWalking", Mathf.Abs(hInput) + Mathf.Abs(vInput) > 0.01);
        Vector2 direction = new Vector2(hInput, vInput);
        if (Mathf.Abs(hInput) + Mathf.Abs(vInput) > 0.1) {
            float angle = Mathf.Atan(direction.x/direction.y);
            if (direction.y < 0) angle = angle+Mathf.PI;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle*180/Mathf.PI, transform.eulerAngles.z);
            rb.velocity = new Vector3(direction.x*walkSpeed, rb.velocity.y, direction.y*walkSpeed);
        }
        else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
        }
    }
    

    void JumpHandler() {
        var jumpInput = Input.GetButtonDown("Jump");
        var jumpInputReleased = Input.GetButtonUp("Jump");
        bool grounded = isGrounded();
        anim.SetBool("isGrounded", grounded);
        anim.SetFloat("upVelocity", rb.velocity.y);
        if (jumpInput && isGrounded()){
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
            anim.SetTrigger("jump");
            // GameManager.instance.IncreaseLevel();
        }
        else {
            anim.ResetTrigger("jump");
        }
        if (jumpInputReleased && rb.velocity.y > 0) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/2, rb.velocity.z);
        }
    }

    bool isGrounded() {
        float sizeY = cl.bounds.size.y;
        Vector3 pos = cl.bounds.center;
        Vector3 bottom = pos + new Vector3(0, -sizeY/2 + 0.1f, 0);
        bool grounded = Physics.Raycast(bottom, new Vector3(0, -1, 0), 0.15f);
        return grounded;
    }


}
