using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    public GameObject gameOverUI; // 게임오버 시 띄울 UI 오브젝트
    public GameObject finishUI;   // [추가] 스테이지 클리어(Finish) 시 띄울 UI 오브젝트

    private int score = 0;
    public float timeRemaining = 60f; // 제한 시간 1분
    private bool isGameOver = false;
    private bool isGameFinished = false; // [추가] 클리어 상태 체크

    void Awake() { instance = this; }

    void Start()
    {
        UpdateScoreUI();
    }

    void Update()
    {
        // 게임오버나 클리어 상태일 때 R키를 누르면 재시작
        if (isGameOver || isGameFinished)
        {
            if (Input.GetKeyDown(KeyCode.R)) RestartGame();
            return;
        }

        // 타이머 감소 및 시간초과 체크
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining).ToString();

            if (timeRemaining <= 0)
            {
                timeRemaining = 0;
                timerText.text = "Time: 0";
                TriggerGameOver();
            }
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
        if (isGameOver || isGameFinished) return;
        score += value;
        UpdateScoreUI();
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score: " + score;
    }

    public void TriggerGameOver()
    {
        if (isGameOver || isGameFinished) return;

        isGameOver = true;
        if (gameOverUI != null) gameOverUI.SetActive(true);
        Debug.Log("Game Over! Press R to Restart.");
    }

    // [추가] 목표 지점에 도달했을 때 실행될 함수
    public void TriggerGameFinish()
    {
        if (isGameOver || isGameFinished) return;

        isGameFinished = true;
        if (finishUI != null) finishUI.SetActive(true); // Finish UI 화면에 띄우기
        Debug.Log("Stage Clear! Finish! Press R to Restart.");
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}