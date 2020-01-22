using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour {

    [SerializeField]int health;
    [SerializeField]int maxHealth = 2;

    public int damage = 1;

    void Start() {
        health = maxHealth;
    }

    void Update() {
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    void TakeDamage(int damage) {
        Destroy(GetComponent<Collider>().gameObject);

        if (health >= 1) {
            health -= damage;
        }
        else if (health <= 0) {
            Die();
        }
    }

    void Die() {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter(Collider collider){
        if(collider.gameObject.tag == "PlayerLaser") {
            Laser laser = collider.gameObject.GetComponent<Laser>();

            TakeDamage(laser.damage);
        }
    }
}
