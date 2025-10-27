using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ending : MonoBehaviour
{
    public GameObject endGameUI; // 엔딩 화면 UI
    public TextMeshProUGUI totalTimeText; // 총 클리어 시간 텍스트 (TextMeshProUGUI)

    private SaveLoadManager saveLoadManager; // SaveLoadManager 참조
    private PauseMenu pauseMenu; // PauseMenu 참조
    private bool isEnding = false; // 엔딩 진행 중 여부 확인

    void Start()
    {
        // SaveLoadManager 찾기
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager == null)
        {
            Debug.LogError("SaveLoadManager가 씬에 없습니다.");
        }

        // PauseMenu 찾기
        pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu가 씬에 없습니다.");
        }

        if (endGameUI != null)
        {
            endGameUI.SetActive(false); // 시작 시 엔딩 화면 비활성화
        }
    }

    void Update()
    {
        // 엔딩 상태에서 ESC 키 입력 처리
        if (isEnding && Input.GetKeyDown(KeyCode.Escape))
        {
            GoToStartMenu();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isEnding)
        {
            isEnding = true; // 중복 실행 방지
            Time.timeScale = 0f; // 게임 정지
            if (pauseMenu != null)
            {
                pauseMenu.SetEndingState(true); // PauseMenu의 ESC 비활성화
            }
            StartCoroutine(EndGameSequence());
        }
    }

    IEnumerator EndGameSequence()
    {
        // 화면이 End 오브젝트로 빨려가는 효과
        Camera mainCamera = Camera.main;
        float elapsedTime = 0f;
        float duration = 2f; // 카메라 줌 효과 지속 시간
        Vector3 startCameraPos = mainCamera.transform.position;
        Vector3 targetCameraPos = transform.position + new Vector3(0, 0, -10); // End로 카메라 이동
        float startCameraSize = mainCamera.orthographicSize;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // 정지 상태에서도 진행
            mainCamera.transform.position = Vector3.Lerp(startCameraPos, targetCameraPos, elapsedTime / duration);
            mainCamera.orthographicSize = Mathf.Lerp(startCameraSize, 1f, elapsedTime / duration); // 카메라 줌인
            yield return null;
        }

        // 엔딩 화면 활성화
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);
        }

        DisplayTotalTime(); // 총 클리어 시간 표시
        Debug.Log("엔딩이 끝났습니다. ESC를 눌러 스타트 메뉴로 돌아갈 수 있습니다.");
    }

    void DisplayTotalTime()
    {
        if (saveLoadManager == null)
        {
            Debug.LogError("SaveLoadManager가 설정되지 않았습니다.");
            return;
        }

        // SaveLoadManager에서 총 플레이 시간 가져오기
        float totalTime = saveLoadManager.GetTotalPlayTime();

        // 총 플레이 시간 계산 및 표시
        int hours = Mathf.FloorToInt(totalTime / 3600f);
        int minutes = Mathf.FloorToInt((totalTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);

        if (totalTimeText != null)
        {
            totalTimeText.text = $"Total Time: {hours:D2}:{minutes:D2}:{seconds:D2}";
        }
        else
        {
            Debug.LogError("totalTimeText가 설정되지 않았습니다.");
        }
    }

    public void GoToStartMenu()
    {
        Time.timeScale = 1f; // 게임 속도 복원
        SceneManager.LoadScene("StartMenu"); // 스타트 메뉴 씬 로드
    }
}
