using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Revenant : MonoBehaviour {

    private SpriteRenderer spriteRenderer;

    [SerializeField]float speed = 4f;
    [SerializeField]float frequency = 20.0f;  // Speed of sine movement
    [SerializeField]float magnitude = 0.5f;   // Size of sine movement
    private Vector3 axis;
    private Vector3 pos;

    void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();

        pos = transform.position;
        axis = transform.right;
    }
    void Update() {
        LookAtPlayer();
        Movement();
    }

    void LookAtPlayer() {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        Transform playerTransform = player.GetComponent<Transform>();

        if (playerTransform.position.x < gameObject.transform.position.x) {
            spriteRenderer.flipX = true;
        }
        else
        spriteRenderer.flipX = false;
    }
    void Movement(){
        pos += -transform.up * Time.deltaTime * speed;
        transform.position = pos + axis * Mathf.Sin (Time.time * frequency) * magnitude;
    }
}
