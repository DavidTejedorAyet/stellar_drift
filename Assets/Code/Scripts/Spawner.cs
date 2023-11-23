using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject pointPrefab;    // Prefab del punto a spawnear
    public float spawnRate;           // Tasa de spawn en segundos
    public float maxHeight;           // Altura m�xima para spawnear los puntos
    public float minHeight;           // Altura m�nima para spawnear los puntos
    public float checkRadius;         // Ajustar este valor seg�n el tama�o de los objetos


    private float nextSpawnTime;

    void Update() {
        if (Time.time >= nextSpawnTime) {
            SpawnPoint();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnPoint() {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(playerPosition.x + 200, Random.Range(minHeight, maxHeight), playerPosition.z); // Aseg�rate de ajustar el valor en Z seg�n tu configuraci�n de juego

        if (!IsPositionOccupied(spawnPosition)) {
            Instantiate(pointPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsPositionOccupied(Vector3 position) {
        // Realiza un peque�o raycast para comprobar si el espacio est� ocupado
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius);
        return colliders.Length > 0; // Retorna true si hay alg�n objeto en la posici�n
    }
}
