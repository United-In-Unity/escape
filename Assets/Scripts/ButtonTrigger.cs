using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{

    public TriggerMovement obj;
    // Start is called before the first frame update
    public GameObject buttonCenter;

    bool pushed = false;
    PlayerMovement pm;
    void Start()
    {
        pm = PlayerManager.instance.GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (((pm.transform.position - transform.position)).magnitude < 5) {
            pm.button = this;
        }
        else if (pm.button == this) {
            pm.button = null;
        }
    }

    public void Push() {
        if (pushed) return;
        Vector3 current = buttonCenter.transform.position;
        buttonCenter.transform.position = new Vector3(current.x, current.y, current.z+0.4f);
        pushed = true;
        if (obj.tag == "Door") {
            print("OPEN THE DAMN DOOR!!!");
            obj.Trigger();
        }
        else {
            print(obj.tag + "'s trigger is not found");
        }
    }
}
