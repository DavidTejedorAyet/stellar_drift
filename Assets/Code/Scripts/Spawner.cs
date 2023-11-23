using UnityEngine;

public class Spawner : MonoBehaviour {

    public GameObject pointPrefab;    // Prefab del punto a spawnear
    public float spawnRate;           // Tasa de spawn en segundos
    public float maxHeight;           // Altura máxima para spawnear los puntos
    public float minHeight;           // Altura mínima para spawnear los puntos
    public float checkRadius;         // Ajustar este valor según el tamaño de los objetos


    private float nextSpawnTime;

    void Update() {
        if (Time.time >= nextSpawnTime) {
            SpawnPoint();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnPoint() {
        Vector3 playerPosition = GameObject.FindGameObjectWithTag("Player").gameObject.transform.position;
        Vector3 spawnPosition = new Vector3(playerPosition.x + 200, Random.Range(minHeight, maxHeight), playerPosition.z); // Asegúrate de ajustar el valor en Z según tu configuración de juego

        if (!IsPositionOccupied(spawnPosition)) {
            Instantiate(pointPrefab, spawnPosition, Quaternion.identity);
        }
    }

    bool IsPositionOccupied(Vector3 position) {
        // Realiza un pequeño raycast para comprobar si el espacio está ocupado
        Collider[] colliders = Physics.OverlapSphere(position, checkRadius);
        return colliders.Length > 0; // Retorna true si hay algún objeto en la posición
    }
}
