using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public GameObject gameObjectToFollow;
    public float smoothness;
    Vector3 offset;


    void Start() {
        offset = transform.position - gameObjectToFollow.transform.position;

    }

    void FixedUpdate() {
        // La posición objetivo de la cámara
        Vector3 posicionObjetivo = gameObjectToFollow.transform.position + offset;
        // Solo seguimos el movimiento horizontal, mantenemos la posición Y y Z de la cámara
        posicionObjetivo.y = transform.position.y;
        posicionObjetivo.z = transform.position.z;

        // Suaviza el movimiento de la cámara hacia la posición objetivo
        transform.position = Vector3.Lerp(transform.position, posicionObjetivo, smoothness * Time.deltaTime);
    }
}
