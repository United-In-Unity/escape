using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    PlayerMovement pm;
    Vector2 target;
    public float speed = 2f;
    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.instance.GetComponent<PlayerMovement>();
        target = new Vector2(transform.position.x, transform.position.z);
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
        Vector2 current = new Vector2(transform.position.x, transform.position.z);
        if ((target - current).magnitude>0.05f) {
            Vector2 direction = (target - current).normalized;
            transform.position = transform.position + speed*new Vector3(direction.x, 0, direction.y)*Time.deltaTime;
        } else {
            transform.position = new Vector3(target.x, transform.position.y, target.y);
        }
    }

    public void Push(Vector3 direction) {
        Vector3 temp = direction*transform.localScale.x;
        target = target + new Vector2(temp.x, temp.z);
    }
}
