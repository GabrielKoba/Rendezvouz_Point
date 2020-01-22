using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    private Rigidbody rb;

    [SerializeField] float laserSpeed;
    [SerializeField] float laserDecay = 3;

    public int damage = 1;

    void Start() {
        rb = gameObject.GetComponent<Rigidbody>();

        rb.AddForce(0, 10 * laserSpeed, 0);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        yield return new WaitForSeconds(laserDecay);
        Destroy(this.gameObject);
    }
}