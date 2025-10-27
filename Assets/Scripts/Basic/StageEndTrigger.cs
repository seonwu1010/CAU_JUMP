using UnityEngine;

public class StageEndTrigger : MonoBehaviour
{
    public SaveLoadManager saveLoadManager; // SaveLoadManager ����
    public int currentStageNumber; // ���� �������� ��ȣ

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
            // ���� ������������ �÷����� �ð��� ���
            float elapsedTime = Time.timeSinceLevelLoad;
            Vector3 playerPosition = collision.transform.position;

            // ���� ���� ȣ��
            saveLoadManager.SaveGame(elapsedTime, playerPosition, currentStageNumber);

            Debug.Log($"Stage {currentStageNumber} saved!");
        }
    }
}
