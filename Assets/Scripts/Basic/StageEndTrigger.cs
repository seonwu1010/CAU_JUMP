using UnityEngine;

public class StageEndTrigger : MonoBehaviour
{
    public SaveLoadManager saveLoadManager; // SaveLoadManager 연결
    public int currentStageNumber; // 현재 스테이지 번호

    private void Start()
    {
        if (saveLoadManager == null)
        {
            saveLoadManager = FindObjectOfType<SaveLoadManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // 현재 스테이지에서 플레이한 시간을 계산
            float elapsedTime = Time.timeSinceLevelLoad;
            Vector3 playerPosition = collision.transform.position;

            // 게임 저장 호출
            saveLoadManager.SaveGame(elapsedTime, playerPosition, currentStageNumber);

            Debug.Log($"Stage {currentStageNumber} saved!");
        }
    }
}
