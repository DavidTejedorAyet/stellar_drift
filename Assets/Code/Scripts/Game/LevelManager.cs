using System.Collections;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    public static LevelManager Instance { get; private set; }
    public int dificult = 0;
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
        uiManager.ShowInGameUI();
    }
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && !GameManager.Instance.isGameRunning) {
            InitGame();
        }
        if (!GameManager.Instance.isGameRunning) return;
        elapsedTime += Time.deltaTime;
        uiManager.UpdateTimeText(elapsedTime);
    }

    void InitGame() {
        GameManager.Instance.isGameRunning = true;
        points = 0;
        scenarioGenerator.AdjustDifficult(0);
        dificult = 0;
    }

    public IEnumerator FinishGame() {
        GameManager.Instance.isGameRunning = false;
        GameManager.Instance.SaveScore(points);


        yield return new WaitForSeconds(5);
        uiManager.ShowGameOverPanel();
    }
    public void AddPoint() {
        if (!GameManager.Instance.isGameRunning) return;
        points++;
        uiManager.UpdatePointsText(points);
        if (points == 10 || points == 20 || points == 30) {
            dificult++;
            scenarioGenerator.AdjustDifficult(dificult);
        }
    }
}
