using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public bool isGameRunning = false;
    public int points = 0;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    void Start() {
        isGameRunning = true;
    }

    public void AddPoint() {
        if (isGameRunning) {
            points = 0;
        }
    }


}
