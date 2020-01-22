using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursor : MonoBehaviour {

    void Update() {
        transform.position = Input.mousePosition;
    }
}
