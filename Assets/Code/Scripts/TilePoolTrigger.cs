using UnityEngine;

public class TilePoolTrigger : MonoBehaviour {

    private ScenarioGenerator scenarioGenerator;

    void Start() {
        // Encuentra el generador de escenarios en la escena
        scenarioGenerator = FindObjectOfType<ScenarioGenerator>();
    }

    private void OnTriggerEnter(Collider other) {
        // Comprueba si es la nave la que ha entrado en el trigger
        if (other.CompareTag("Tile")) {
            scenarioGenerator.GenerateTile();
        }
    }
}
