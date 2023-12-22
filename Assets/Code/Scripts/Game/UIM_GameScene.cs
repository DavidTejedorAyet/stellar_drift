using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIM_GameScene : MonoBehaviour {
    // UI MANAGER
    public GameObject inGameUIPanel;
    public GameObject pauseMenuPanel;
    public GameObject gameOverPanel;

    public TextMeshProUGUI[] pointsTexts;
    public TextMeshProUGUI timeText;

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (pauseMenuPanel.activeInHierarchy) {
                ContinueButtonPressed();
            } else {
                PauseButtonPressed();
            }
        }
    }

    public void ShowGameOverPanel() {
        inGameUIPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void ShowInGameUI() {
        inGameUIPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void PauseButtonPressed() {
        GameManager.Instance.isGameRunning = false;
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0.0f;
    }

    public void ContinueButtonPressed() {
        GameManager.Instance.isGameRunning = true;
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void RetryButtonPressed() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ExitButtonPressed() {
        SceneManager.LoadScene("MainMenuScene");

    }

    public void UpdateTimeText(float time) {
        TimeSpan timeSpan = TimeSpan.FromSeconds(time);
        timeText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }

    public void UpdatePointsText(int points) {
        foreach (var text in pointsTexts) {
            text.text = points.ToString();
        }
    }
    public void ChangeMusicVolume(float volume) {
        AudioManager.Instance.ChangeMusicVolume(volume);
    }

    public void ChangeEffectVolume(float volume) {
        AudioManager.Instance.ChangeEffectVolume(volume);
    }
}
