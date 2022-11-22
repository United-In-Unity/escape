using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    CapsuleCollider cl;
    Animator anim;

    public bool alive = true;

    public float walkSpeed = 4.0f;
    public float jumpSpeed = 8.0f;
    // Start is called before the first frame update

    public BoxMovement box = null;
    public bool canPush = false;
    
    float pushTimer = 0f;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cl = GetComponent<CapsuleCollider>();
        anim = this.gameObject.transform.GetChild(0).GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive){
            WalkHandler();
            JumpHandler();
            PushHandler();
        }
        if (Input.GetButtonDown("Fire1")) {
            print("die!!!!!!!!!!!!!");
            // PlayerManager.instance.Die();
        }
    }

    void WalkHandler() {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        Vector2 direction = new Vector2(hInput, vInput);
        anim.SetBool("isWalking",  direction.magnitude> 0.01);
        direction.Normalize();
        if (direction.magnitude > 0.01 && !isPushing()) {
            rb.velocity = new Vector3(direction.x*walkSpeed, rb.velocity.y, direction.y*walkSpeed);
            anim.SetTrigger("walk");
        }
        else {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            anim.ResetTrigger("walk");
        }
    }
    

    void JumpHandler() {
        var jumpInput = Input.GetButtonDown("Jump");
        var jumpInputReleased = Input.GetButtonUp("Jump");
        bool grounded = isGrounded();
        anim.SetBool("isGrounded", grounded);
        anim.SetFloat("upVelocity", rb.velocity.y);
        if (jumpInput && isGrounded() && !isPushing()){
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

    void PushHandler() {
        var pushInput = Input.GetKeyDown(KeyCode.E);
        Vector3 start = cl.bounds.center;
        float angle = anim.gameObject.transform.eulerAngles.y/180*Mathf.PI+Mathf.PI/2;
        Vector3 direction = new Vector3(Mathf.Cos(angle),0,-Mathf.Sin(angle)).normalized;
        if (Mathf.Abs(direction.x) + Mathf.Abs(direction.z) > 1.1f) return;
        Ray ray = new Ray(start, direction);
        RaycastHit hit;
        canPush = box != null && Physics.Raycast(ray, out hit) && hit.transform.GetComponent<BoxMovement>()==box;
        if (pushInput && canPush){
            anim.SetTrigger("bump");
            box.Push(direction);
            pushTimer = 0f;
        }
        else {
            anim.ResetTrigger("bump");
        }
        anim.SetBool("isBumping", isPushing());
        pushTimer += Time.deltaTime;
    }

    public bool isPushing() {
        return pushTimer < 1f;
    }
}
