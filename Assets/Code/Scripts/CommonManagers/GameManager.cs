using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager Instance { get; private set; }
    public bool isGameRunning = false;


    void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void SaveScore(int score) {
        if (score > GetScore()) {
            PlayerPrefs.SetInt("HighScore", score);
            PlayerPrefs.Save();
        }
    }

    public int GetScore() {
        return PlayerPrefs.GetInt("HighScore", 0);
    }
}
