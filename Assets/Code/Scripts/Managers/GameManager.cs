using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }

    public bool isGameRunning = false;
    private int points = 0;
    private float elapsedTime = 0f;

    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    void Start() {
        InitGame();
    }
    void Update() {
        if (isGameRunning) {
            elapsedTime += Time.deltaTime;
            UIManager.Instance.UpdateTimeText(elapsedTime);
        }
    }

    void InitGame() {
        isGameRunning = true;
        points = 0;

    }
    public void AddPoint() {
        if (isGameRunning) {
            points++;
            UIManager.Instance.UpdatePointsText(points);
        }
    }


}
