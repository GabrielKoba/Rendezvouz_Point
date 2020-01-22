using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public static bool playerMoving;
    private Rigidbody rb;
    private Animator myAnimator;
    [SerializeField] GameObject playerAudio;

    int idle = Animator.StringToHash("Idle");
    int movingLeft = Animator.StringToHash("MovingLeft");
    int movingRight = Animator.StringToHash("MovingRight");
    int fire = Animator.StringToHash("Fire");

    [SerializeField] float speed = 8f;
    [SerializeField] float turnSensitivity = 5f;
    [SerializeField] float mouseDeadzone = 15f;

    [SerializeField] GameObject laserSource0;
    [SerializeField] GameObject laserSource1;
    [SerializeField] GameObject laser;
    [SerializeField] float rateOfFireCooldown;
    private float rateOfFire;
    private float laserPitch;

    void Awake() {
        rb = gameObject.GetComponent<Rigidbody>();
        myAnimator = gameObject.GetComponent<Animator>();

        playerMoving = true;

        speed = speed * 50;
        turnSensitivity = turnSensitivity * 50;
    }

    void FixedUpdate() {
        Movement();
        Animation();
        Combat();
    }

    void Movement() {
        Vector3 mousePosition = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        Vector2 direction = (mousePosition - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * turnSensitivity * Time.deltaTime, direction.y * speed * Time.deltaTime);
    }

    void Animation() {
        var playerScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mousePos = Input.mousePosition;

        //Horizontal
        if (mousePos.x > playerScreenPoint.x && Mathf.Abs(mousePos.x - playerScreenPoint.x) > 20) {
            myAnimator.SetBool(movingRight, true);
            myAnimator.SetBool(idle, false);
            myAnimator.SetBool(movingLeft, false);
        }
        else if (mousePos.x < playerScreenPoint.x && Mathf.Abs(mousePos.x - playerScreenPoint.x) > 20) {
            myAnimator.SetBool(movingLeft, true);
            myAnimator.SetBool(idle, false);
            myAnimator.SetBool(movingRight, false);
        }
        //Idle
        else if (Mathf.Abs(mousePos.x - playerScreenPoint.x) > mouseDeadzone) {
            myAnimator.SetBool(idle, true);
            myAnimator.SetBool(movingLeft, false);
            myAnimator.SetBool(movingRight, false);
        }
    }

    void Combat() {
        if (Input.GetMouseButton(0) && rateOfFire <= Time.time) {
            rateOfFire = Time.time + rateOfFireCooldown;
            myAnimator.SetTrigger(fire);

            FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Ektor/Ektor_Laser", playerAudio.GetComponent<Transform>().position);

            Instantiate(laser, laserSource0.transform.position, laserSource0.transform.localRotation);
            Instantiate(laser, laserSource1.transform.position, laserSource1.transform.localRotation);
        }
    }
}
