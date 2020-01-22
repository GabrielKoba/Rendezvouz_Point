using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    public GameObject[] layers;
    private Camera mainCamera;
    private Vector2 screenBounds;
    [SerializeField]float speed; 


    void Start() {
        mainCamera = Camera.main;
        screenBounds = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, mainCamera.transform.position.z));
        foreach(GameObject obj in layers){
            loadChildObjects(obj);
        }
    }
    void loadChildObjects(GameObject obj) {
        float objectHeight = obj.GetComponent<Collider>().bounds.size.y;
        int childsNeeded = (int)Mathf.Ceil(screenBounds.y * 2 / objectHeight);
        GameObject clone = Instantiate(obj) as GameObject;
        for (int i = 0; i <= childsNeeded; i++) {
            GameObject c = Instantiate(clone) as GameObject;
            c.transform.SetParent(obj.transform);
            c.transform.position = new Vector3(obj.transform.position.x, objectHeight * i, obj.transform.position.z);
            c.name = obj.name + i;
        }
        Destroy(clone);
    }
    void FixedUpdate() {
        if (Player.playerMoving) {
            transform.Translate(-Vector3.up * speed * Time.deltaTime);
        }
    }
}
