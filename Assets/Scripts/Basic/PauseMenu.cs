using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // 일시 정지 메뉴 UI
    private bool isPaused = false; // 일시 정지 상태 플래그
    private bool isEnding = false; // 엔딩 진행 여부 플래그
    private SaveLoadManager saveLoadManager;

    void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
    }

    void Update()
    {
        // 엔딩 중에는 ESC 입력 무시
        if (isEnding)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc key pressed"); // ESC 키 눌림 확인
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void SaveGame()
    {
        Debug.Log("Saving game...");

        float elapsedTime = Time.timeSinceLevelLoad; // 예제 소요 시간
        Vector3 playerPosition = FindObjectOfType<PlayerController>().transform.position; // 예제 플레이어 위치
        int currentStage = SceneManager.GetActiveScene().buildIndex; // 현재 스테이지 번호

        saveLoadManager.SaveGame(elapsedTime, playerPosition, currentStage); // 게임 데이터 저장
    }

    public void MainMenu()
    {
        Debug.Log("Returning to main menu.");
        Time.timeScale = 1f; // 게임 속도 정상화
        SceneManager.LoadScene("StartMenu"); // 메인 메뉴 씬 로드
    }

    void Resume()
    {
        if (!isPaused) return; // 이중 호출 방지

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // 게임 속도 정상화
        isPaused = false;
        Debug.Log("Game resumed.");
    }

    void Pause()
    {
        if (isPaused) return; // 이중 호출 방지

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // 게임 일시 정지
        isPaused = true;
        Debug.Log("Game paused and menu displayed.");
    }

    // 엔딩 상태 설정 함수
    public void SetEndingState(bool state)
    {
        isEnding = state;
        if (state)
        {
            Debug.Log("Ending state activated. ESC is disabled.");
        }
    }
}
