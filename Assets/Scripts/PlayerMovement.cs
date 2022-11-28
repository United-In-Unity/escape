using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    CapsuleCollider cl;
    Animator anim;

    public bool alive = true;

    public float walkSpeed = 4.0f;
    public float jumpSpeed = 8.0f;
    // Start is called before the first frame update

    public BoxMovement box = null;
    public bool canPushBox = false;

    public ButtonTrigger button = null;
    public bool canPushButton = false;

    public bool hasPushedButton = false;
    public bool hasPushedBox = false;


    float pushTimer = 1f;
    
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
        else {
            if (!PlayerManager.instance.isDying){
                RebornHandler();
            }
        }
        // if (Input.GetButtonDown("Fire1")) {
        //     // PlayerManager.instance.Die();
        // }
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
        pushTimer += Time.deltaTime;
        if (isPushing()) return;
        var pushInput = Input.GetKeyDown(KeyCode.E);
        Vector3 start = cl.bounds.center;
        float angle = -(anim.gameObject.GetComponent<PlayerRotation>().angle-90)/180*Mathf.PI;
        Vector3 direction = new Vector3(Mathf.Cos(angle),0,Mathf.Sin(angle)).normalized;
        if (Mathf.Abs(direction.x) + Mathf.Abs(direction.z) > 1.1f) return;
        Ray ray = new Ray(start, direction);
        RaycastHit hit;
        canPushBox = box != null && Physics.Raycast(ray, out hit) && hit.transform.GetComponent<BoxMovement>()==box;
        if (pushInput && canPushBox){
            anim.SetTrigger("kick");
            box.Push(direction);
            hasPushedBox = true;
            pushTimer = 0f;
            GameManager.instance.gs.PlayerBump();
        }
        else {
            anim.ResetTrigger("kick");
        }

        RaycastHit hit2;
        canPushButton = button != null && Physics.Raycast(ray, out hit2) && hit2.transform.GetComponent<ButtonTrigger>() == button;
        if (pushInput && canPushButton)
        {
            anim.SetTrigger("bump");
            button.Push();
            hasPushedButton = true;
            pushTimer = 0f;
            GameManager.instance.gs.ClickButton();
        }
        else {
            anim.ResetTrigger("bump");
        }
        bool playerIsPushing = isPushing();
        anim.SetBool("isBumping", playerIsPushing);
    }

    public bool isPushing() {
        return pushTimer < 1f;
    }

    public void StopMoving() {
        rb.velocity = new Vector3(0,0,0);
        rb.isKinematic = true;
    }

    void RebornHandler() {
        var pushInput = Input.GetKeyDown(KeyCode.R);
        if (pushInput) {
            PlayerManager.instance.Reborn();
            rb.isKinematic = false;
        }
    }
}
