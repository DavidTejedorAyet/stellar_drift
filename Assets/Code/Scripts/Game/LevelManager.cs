using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }
    public bool isGameRunning = false;
    public int level = 0;
    [SerializeField] private UIM_GameScene uiManager;
    [SerializeField] private ScenarioGenerator scenarioGenerator;
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
        if (!GameManager.Instance.isGameRunning) return;
        elapsedTime += Time.deltaTime;
        uiManager.UpdateTimeText(elapsedTime);
    }

    void InitGame() {
        GameManager.Instance.isGameRunning = true;
        points = 0;
        scenarioGenerator.AdjustDifficult(0);
        level = 0;
    }

    public void FinishGame() {
        GameManager.Instance.isGameRunning = false;
        GameManager.Instance.SaveScore(points);
    }
    public void AddPoint() {
        if (!GameManager.Instance.isGameRunning) return;
        points++;
        uiManager.UpdatePointsText(points);
        if (points == 10 || points == 20 || points == 30) {
            level++;
            scenarioGenerator.AdjustDifficult(level);
        }
    }
}
