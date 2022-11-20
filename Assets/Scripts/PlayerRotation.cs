using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{

    public float speed = 200f;
    public float target = 0f;
    public float margin = 1f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");
        if (Mathf.Abs(hInput) > 0.01) hInput = Mathf.Sign(hInput);
        else hInput = 0;
        if (Mathf.Abs(vInput) > 0.01) vInput = Mathf.Sign(vInput);
        else vInput = 0;
        Vector2 direction = new Vector2(hInput, vInput);
        if (direction.magnitude > 0.01) {
            float angle = Mathf.Atan(direction.x/direction.y);
            if (direction.y < 0) angle = angle+Mathf.PI;
            target = (360+angle * 180 / Mathf.PI)%360;
        }
        Vector3 current = transform.eulerAngles;
        float difference = (360+target - current.y)%360;
        float multiplier = 1f;
        if (difference > 120) { multiplier = 1.5f; }
        if (Mathf.Abs(difference) < margin) {
            transform.eulerAngles = new Vector3(current.x, target, current.z);
        }
        else if (difference<180) {
            transform.eulerAngles = new Vector3(current.x, (360+current.y+speed*multiplier*Time.deltaTime)%360, current.z);
        }
        else {
            transform.eulerAngles = new Vector3(current.x, (360+current.y-speed*multiplier*Time.deltaTime)%360, current.z);
        }
    }
}
