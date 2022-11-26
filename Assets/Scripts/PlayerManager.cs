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

    float deathTime = 1f;
    float deathTimer = 1f;
    float intensity;
    
    public GameObject character;
    public Light backup;
    public Renderer point;
    public Renderer leg1;
    public Renderer leg2;
    public Renderer leg3;
    public Renderer leg4;
    public Light halo;

    public GameObject explosion;

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
        intensity = halo.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (point != null && deathTimer < deathTime) {  
            float r, b, g, a = r = b = g = 1f - deathTimer / deathTime;
            Color customColor = new Color(r, b, g, a);
            point.material.SetColor("_EmissionColor", customColor);
            leg1.material.SetColor("_EmissionColor", customColor);
            leg2.material.SetColor("_EmissionColor", customColor);
            leg3.material.SetColor("_EmissionColor", customColor);
            leg4.material.SetColor("_EmissionColor", customColor);
            halo.intensity = intensity - deathTimer / deathTime * intensity / 2;
            deathTimer += Time.deltaTime;
            player.transform.position += new Vector3(0, Time.deltaTime, 0);
            if (deathTimer >= deathTime) {
                Instantiate(explosion, player.transform.position, Quaternion.identity);
                character.SetActive(false);
                backup.enabled = true;
            }
        }
    }

    public void SetPosition(Vector3 p) {
        transform.position = p;
    }

    public bool PlayerCanPushBox(){
        return pm.canPushBox;
    }
    
    public bool PlayerHasPushBox(){
        return pm.hasPushedBox;
    }
    public bool PlayerCanPushButton(){
        return pm.canPushButton;
    }
    public bool PlayerHasPushButton(){
        return pm.hasPushedButton;
    }

    public bool PlayerHasDied(){
        return pm.alive;
    }

    public void Die() {
        print("Player is dead!!! Dead animation is playing...");
        // TODO: Dead animation 
        // TODO: GAME UI for options
        // Options: MENU | RESTART
            // UI: choose levels
            // GameManager.instance.LoadLevel();
        anim.SetBool("isDead", true);
        deathTimer = 0f;
        pm.alive = false;
        pm.StopMoving();
    }
}
