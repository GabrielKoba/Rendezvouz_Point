using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    private Animator anim;
    
    [SerializeField]int health;
    [SerializeField]int maxHealth = 2;

    public int damage = 1;

    [SerializeField]float deathAnimationDuration = 0.75f;

    [FMODUnity.EventRef]
    [SerializeField]string deathSFX = "";

    void Awake() {
        anim = gameObject.GetComponent<Animator>();

        health = maxHealth;
    }

    void Update() {
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    void TakeDamage(int damage) {
        if (health >= 1) {
            health -= damage;
        }
        else if (health <= 0) {
            StartCoroutine (Die());
        }
    }

    void OnTriggerEnter(Collider col){
        if(col.gameObject.tag == "PlayerLaser") {
            Laser laser = col.gameObject.GetComponent<Laser>();
            TakeDamage(laser.damage);
            
            Destroy(col.gameObject);
        }
    }

    public IEnumerator Die() {
        anim.Play("Death");
        this.GetComponent<Collider>().enabled = false;

        FMODUnity.RuntimeManager.PlayOneShot(deathSFX, transform.position);

        yield return new WaitForSeconds(0.5f);

        gameObject.SetActive(false);
    }
}
