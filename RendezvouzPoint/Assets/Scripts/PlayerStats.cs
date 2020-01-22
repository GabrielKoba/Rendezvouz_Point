using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    private SpriteRenderer spriteRenderer;
    [SerializeField]int health;
    [SerializeField]int maxHealth = 3;
    
    [SerializeField]int shields;
    [SerializeField]int maxShield = 5;
    [SerializeField]int shieldCooldown = 6;

    [SerializeField]float invincibilityTime = 1;
    [SerializeField]bool isInvincible = false;

    void Awake() {
        SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }
    void Start() {        
        health = maxHealth;
        shields = maxShield;
    }

    void Update() {
        //Limits Health and Shields
        health = Mathf.Clamp(health, 0, maxHealth);
        shields = Mathf.Clamp(shields, 0, maxShield);
    }

    void TakeDamage(int damage) {
        //spriteRenderer.material.SetFloat("_FlashAmount", 0.7f);
        StartCoroutine(Invulnerability());

        if (shields >= 1) {
            StartCoroutine(ShieldRecovery());
            shields -= damage;
        }
        else if (health >= 1) {
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
        if(collider.gameObject.tag == "RepairKit") {
            health += 3;
            Destroy(collider.gameObject);
        }
        else if(collider.gameObject.tag == "Enemy" && !isInvincible) {
            EnemyStats enemyStats = collider.gameObject.GetComponent<EnemyStats>();

            TakeDamage(enemyStats.damage);
        }
    }

    IEnumerator ShieldRecovery() {
        while (true) {
            yield return new WaitForSeconds(shieldCooldown); 

            if (shields <= maxShield) {
                shields += 1;
            }
            
            StopCoroutine(ShieldRecovery());
        }
    }

    IEnumerator Invulnerability() {
        isInvincible = true;
        
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
        
        StopCoroutine(Invulnerability());
    }
}