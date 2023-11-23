using System.Collections;
using UnityEngine;

public class SpaceshipController : MonoBehaviour {

    public float horizontalSpeed; //50
    public float verticalSpeed; // 30
    public float tiltAngle; // Angulo de inclinacion de la nave al subir o bajar // 30
    public float tiltSmooth; // Velocidad de suavizado de la inclinación // 3.5
    public float movementSmooth; // Velocidad de suavizado del movimiento vertical // 4
    public GameObject explosionParticle;
    public GameObject trailParticle;

    private MeshRenderer meshRenderer;
    private Rigidbody rb;
    private float targetTilt; // Ángulo objetivo de inclinación

    void Awake() {
        rb = GetComponent<Rigidbody>();
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update() {
        if (!GameManager.Instance.isGameRunning) {
            return;
        }
        MoveSpaceship();
    }

    void MoveSpaceship() {

        // Movimiento constante hacia la derecha
        Vector2 targetVelocity = new Vector2(horizontalSpeed, rb.velocity.y);

        // Control del movimiento vertical con entradas del usuario
        if (Input.GetKey(KeyCode.Space)) {
            // Aplicar fuerza hacia abajo
            targetVelocity.y = -verticalSpeed;
            targetTilt = tiltAngle;
        } else {
            targetVelocity.y = verticalSpeed;
            targetTilt = -tiltAngle;
        }

        // Suavizar el movimiento y la inclinación
        rb.velocity = Vector2.Lerp(rb.velocity, targetVelocity, movementSmooth * Time.deltaTime);
        float currentTilt = Mathf.LerpAngle(transform.eulerAngles.x, targetTilt, tiltSmooth * Time.deltaTime);
        transform.rotation = Quaternion.Euler(currentTilt, 90, 0);
    }
    public void DestroyShip() {
        horizontalSpeed = 0f;
        verticalSpeed = 0f;

        Instantiate(explosionParticle, transform.position, Quaternion.identity);

        StartCoroutine(DisableMeshRendererAfterDelay());
        GameManager.Instance.isGameRunning = false;
    }
    private IEnumerator DisableMeshRendererAfterDelay() {
        yield return new WaitForSeconds(0.15f);
        rb.velocity = Vector3.zero;
        trailParticle.SetActive(false);
        meshRenderer.enabled = false;
    }
}
