using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Ending : MonoBehaviour
{
    public GameObject endGameUI; // ���� ȭ�� UI
    public TextMeshProUGUI totalTimeText; // �� Ŭ���� �ð� �ؽ�Ʈ (TextMeshProUGUI)

    private SaveLoadManager saveLoadManager; // SaveLoadManager ����
    private PauseMenu pauseMenu; // PauseMenu ����
    private bool isEnding = false; // ���� ���� �� ���� Ȯ��

    void Start()
    {
        // SaveLoadManager ã��
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
        if (saveLoadManager == null)
        {
            Debug.LogError("SaveLoadManager�� ���� �����ϴ�.");
        }

        // PauseMenu ã��
        pauseMenu = FindObjectOfType<PauseMenu>();
        if (pauseMenu == null)
        {
            Debug.LogError("PauseMenu�� ���� �����ϴ�.");
        }

        if (endGameUI != null)
        {
            endGameUI.SetActive(false); // ���� �� ���� ȭ�� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        // ���� ���¿��� ESC Ű �Է� ó��
        if (isEnding && Input.GetKeyDown(KeyCode.Escape))
        {
            GoToStartMenu();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isEnding)
        {
            isEnding = true; // �ߺ� ���� ����
            Time.timeScale = 0f; // ���� ����
            if (pauseMenu != null)
            {
                pauseMenu.SetEndingState(true); // PauseMenu�� ESC ��Ȱ��ȭ
            }
            StartCoroutine(EndGameSequence());
        }
    }

    IEnumerator EndGameSequence()
    {
        // ȭ���� End ������Ʈ�� �������� ȿ��
        Camera mainCamera = Camera.main;
        float elapsedTime = 0f;
        float duration = 2f; // ī�޶� �� ȿ�� ���� �ð�
        Vector3 startCameraPos = mainCamera.transform.position;
        Vector3 targetCameraPos = transform.position + new Vector3(0, 0, -10); // End�� ī�޶� �̵�
        float startCameraSize = mainCamera.orthographicSize;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.unscaledDeltaTime; // ���� ���¿����� ����
            mainCamera.transform.position = Vector3.Lerp(startCameraPos, targetCameraPos, elapsedTime / duration);
            mainCamera.orthographicSize = Mathf.Lerp(startCameraSize, 1f, elapsedTime / duration); // ī�޶� ����
            yield return null;
        }

        // ���� ȭ�� Ȱ��ȭ
        if (endGameUI != null)
        {
            endGameUI.SetActive(true);
        }

        DisplayTotalTime(); // �� Ŭ���� �ð� ǥ��
        Debug.Log("������ �������ϴ�. ESC�� ���� ��ŸƮ �޴��� ���ư� �� �ֽ��ϴ�.");
    }

    void DisplayTotalTime()
    {
        if (saveLoadManager == null)
        {
            Debug.LogError("SaveLoadManager�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // SaveLoadManager���� �� �÷��� �ð� ��������
        float totalTime = saveLoadManager.GetTotalPlayTime();

        // �� �÷��� �ð� ��� �� ǥ��
        int hours = Mathf.FloorToInt(totalTime / 3600f);
        int minutes = Mathf.FloorToInt((totalTime % 3600f) / 60f);
        int seconds = Mathf.FloorToInt(totalTime % 60f);

        if (totalTimeText != null)
        {
            totalTimeText.text = $"Total Time: {hours:D2}:{minutes:D2}:{seconds:D2}";
        }
        else
        {
            Debug.LogError("totalTimeText�� �������� �ʾҽ��ϴ�.");
        }
    }

    public void GoToStartMenu()
    {
        Time.timeScale = 1f; // ���� �ӵ� ����
        SceneManager.LoadScene("StartMenu"); // ��ŸƮ �޴� �� �ε�
    }
}
