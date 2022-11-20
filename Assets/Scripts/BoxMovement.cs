using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMovement : MonoBehaviour
{
    PlayerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        pm = PlayerManager.instance.GetComponent<PlayerMovement>();
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
    }

    public void Push() {
        print("box pushed");
    }
}
