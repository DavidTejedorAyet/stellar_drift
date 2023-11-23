using UnityEngine;

public class ScenarioTile : MonoBehaviour {

    public int weight;


    private void OnTriggerExit(Collider other) {
        // Comprueba si es la nave la que ha entrado en el trigger
        if (other.CompareTag("TilePoolTrigger")) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.gameObject.GetComponent<SpaceshipController>().DestroyShip();
        }
    }

}
