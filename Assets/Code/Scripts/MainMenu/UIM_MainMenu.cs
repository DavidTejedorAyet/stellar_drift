using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIM_MainMenu : MonoBehaviour {

    [SerializeField] TextMeshProUGUI scoreText;

    private void Start() {
        scoreText.text = GameManager.Instance.GetScore().ToString();
    }
    public void PlayButtonPressed() {
        SceneManager.LoadScene("GameScene");
    }
}
