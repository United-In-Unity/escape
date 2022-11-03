using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    public float walkSpeed = 4.0f;
    public float jumpSpeed = 8.0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        WalkHandler();
        JumpHandler();
        
    }

    void WalkHandler() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(hInput, vInput);
        direction.Normalize();
        rb.velocity = new Vector3(direction.x*walkSpeed, rb.velocity.y, direction.y*walkSpeed);
        // if (hInput < 0) 
        //     transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        // else if (hInput > 0)
        //     transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, 1);
        // cam.transform.position = new Vector3(transform.position.x, cam.transform.position.y, cam.transform.position.z);
    }
    

    void JumpHandler() {
        var jumpInput = Input.GetButtonDown("Jump");
        var jumpInputReleased = Input.GetButtonUp("Jump");
        if (jumpInput && isGrounded()){
            rb.velocity = new Vector3(rb.velocity.x, jumpSpeed, rb.velocity.z);
        }
        if (jumpInputReleased && rb.velocity.y > 0) {
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/2, rb.velocity.z);
        }
    }

    bool isGrounded() {
        return true;
    }
}
