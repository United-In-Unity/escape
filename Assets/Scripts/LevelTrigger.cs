using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelTrigger : MonoBehaviour
{
    public string levelName;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider cl) {
        if (cl.gameObject.tag == "Player") {
            GameManager.instance.LoadLevel(levelName);
        }
    }
}
