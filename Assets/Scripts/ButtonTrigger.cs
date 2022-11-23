using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public TriggerMovement obj;
    // Start is called before the first frame update
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
        if (obj.tag == "Door") {
            print("OPEN THE DAMN DOOR!!!");
            obj.Trigger();
        }
        else {
            print(obj.tag + "'s trigger is not found");
        }
    }
}
