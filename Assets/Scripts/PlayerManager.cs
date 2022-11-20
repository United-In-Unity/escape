using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance = null;

    GameObject player;
    Animator anim;
    PlayerMovement pm;


    void Awake() {
        if (instance == null) {
            instance = this;
            player = this.gameObject.transform.GetChild(0).gameObject;
        }
        else if (instance != this) {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
        pm = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPosition(Vector3 p) {
        transform.position = p;
    }

    public void Die() {
        anim.SetBool("isDead", true);
        pm.alive = false;
    }
}
