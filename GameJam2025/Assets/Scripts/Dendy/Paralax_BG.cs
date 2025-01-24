using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax_BG : MonoBehaviour {

    public float speed = 10f;
    public float endPositionX = -19.16f;
    public Vector3 startPosition;       

    void Update() {
        transform.position += Vector3.left * speed * Time.deltaTime;

        if (transform.position.x <= endPositionX) {
            transform.position = startPosition;
        }
    }
}
