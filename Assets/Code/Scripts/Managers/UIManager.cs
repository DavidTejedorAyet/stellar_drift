using UnityEngine;

public class UIManager : MonoBehaviour {
    public static UIManager Instance { get; private set; }
    public GameObject inGameUIPanel;
    public GameObject pauseMenuPanel;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    public void PauseButtonPressed() {
        GameManager.Instance.isGameRunning = false;

    }
}
