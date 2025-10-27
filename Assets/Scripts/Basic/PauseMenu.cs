using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenuUI; // �Ͻ� ���� �޴� UI
    private bool isPaused = false; // �Ͻ� ���� ���� �÷���
    private bool isEnding = false; // ���� ���� ���� �÷���
    private SaveLoadManager saveLoadManager;

    void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
    }

    void Update()
    {
        // ���� �߿��� ESC �Է� ����
        if (isEnding)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Esc key pressed"); // ESC Ű ���� Ȯ��
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

        float elapsedTime = Time.timeSinceLevelLoad; // ���� �ҿ� �ð�
        Vector3 playerPosition = FindObjectOfType<PlayerController>().transform.position; // ���� �÷��̾� ��ġ
        int currentStage = SceneManager.GetActiveScene().buildIndex; // ���� �������� ��ȣ

        saveLoadManager.SaveGame(elapsedTime, playerPosition, currentStage); // ���� ������ ����
    }

    public void MainMenu()
    {
        Debug.Log("Returning to main menu.");
        Time.timeScale = 1f; // ���� �ӵ� ����ȭ
        SceneManager.LoadScene("StartMenu"); // ���� �޴� �� �ε�
    }

    void Resume()
    {
        if (!isPaused) return; // ���� ȣ�� ����

        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f; // ���� �ӵ� ����ȭ
        isPaused = false;
        Debug.Log("Game resumed.");
    }

    void Pause()
    {
        if (isPaused) return; // ���� ȣ�� ����

        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f; // ���� �Ͻ� ����
        isPaused = true;
        Debug.Log("Game paused and menu displayed.");
    }

    // ���� ���� ���� �Լ�
    public void SetEndingState(bool state)
    {
        isEnding = state;
        if (state)
        {
            Debug.Log("Ending state activated. ESC is disabled.");
        }
    }
}
