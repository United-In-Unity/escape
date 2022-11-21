using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    PlayerMovement pm;
    Vector3 target;
    public float speed = 0.02f;
    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.instance.GetComponent<PlayerMovement>();
        target = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((pm.transform.position - transform.position).magnitude < 4) {
            pm.box = this;
        }
        else if (pm.box == this) {
            pm.box = null;
        }
        if ((target - transform.position).magnitude>0.05f) {
            Vector3 direction = (target - transform.position).normalized;
            transform.position = transform.position + speed*direction;
        } else {
            transform.position = target;
        }
    }

    public void Push(Vector3 direction) {
        target = transform.position + direction*3;
    }
}
