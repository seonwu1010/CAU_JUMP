using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class StartMenu : MonoBehaviour
{
    private SaveLoadManager saveLoadManager;

    void Start()
    {
        saveLoadManager = FindObjectOfType<SaveLoadManager>();
    }

    public void NewGame()
    {
        SceneManager.LoadScene("Stage1");
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("StageNumber"))
        {
            saveLoadManager.LoadGame();
            SceneManager.LoadScene("Stage" + saveLoadManager.stageNumber);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.Log("No saved game found.");
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        var player = FindObjectOfType<PlayerController>();
        player.transform.position = saveLoadManager.playerPosition;
        SceneManager.sceneLoaded -= OnSceneLoaded; // 이벤트 해제
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Game is exiting.");
    }
}
