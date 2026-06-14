using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverUI; // 게임오버 시 띄울 UI 오브젝트 (선택 사항)

    private int score = 0;
    public float timeRemaining = 60f; // 제한 시간 1분
    private bool isGameOver = false;

    void Awake() { instance = this; }

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        if (isGameOver)
        {
            // 게임오버 상태에서 R키를 누르면 재시작
            if (Input.GetKeyDown(KeyCode.R)) RestartGame();
            return;
        }

        // 타이머 감소
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();
        }
        else
        {
            TriggerGameOver();
        }

        // 추락 감지 (플레이어 Y 좌표가 -10 이하로 떨어지면 게임오버)
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && player.transform.position.y < -10f)
        {
            TriggerGameOver();
        }
    }

    public void AddScore(int value)
    {
        if (isGameOver) return;
        score += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Debug.Log("Game Over! Press R to Restart.");
    }

    public void RestartGame()
    {
        // 현재 씬을 다시 로드
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
