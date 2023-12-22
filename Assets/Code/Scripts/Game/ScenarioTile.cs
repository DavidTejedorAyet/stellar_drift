using UnityEngine;

public class ScenarioTile : MonoBehaviour {
    public bool isDestroyerTile = true;
    public int weight;




    void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && isDestroyerTile) {
            other.gameObject.GetComponent<SpaceshipController>().DestroyShip();
        }
    }

}
