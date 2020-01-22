using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounder : MonoBehaviour {

    private Vector3 screenBounds;

    void Awake() {
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void LateUpdate() {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x, screenBounds.x * -1);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y, screenBounds.y * -1);
        transform.position = viewPos;
    }
}
