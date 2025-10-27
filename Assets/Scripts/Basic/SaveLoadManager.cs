using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private const string PlayerPositionXKey = "PlayerPositionX";
    private const string PlayerPositionYKey = "PlayerPositionY";
    private const string PlayerPositionZKey = "PlayerPositionZ";
    private const string StageNumberKey = "StageNumber";
    private const string Stage1TimeKey = "Stage1Time";
    private const string Stage2TimeKey = "Stage2Time";
    private const string Stage3TimeKey = "Stage3Time";

    public Vector3 playerPosition; // �÷��̾� ��ġ
    public int stageNumber; // ���� �������� ��ȣ

    public void SaveGame(float currentStageTime, Vector3 playerPosition, int stageNumber)
    {
        // ���� ���������� �ð� ����
        if (stageNumber == 1)
        {
            PlayerPrefs.SetFloat(Stage1TimeKey, currentStageTime);
        }
        else if (stageNumber == 2)
        {
            PlayerPrefs.SetFloat(Stage2TimeKey, currentStageTime);
        }
        else if (stageNumber == 3)
        {
            PlayerPrefs.SetFloat(Stage3TimeKey, currentStageTime);
        }

        // �÷��̾� ��ġ �� ���� �������� ��ȣ ����
        PlayerPrefs.SetFloat(PlayerPositionXKey, playerPosition.x);
        PlayerPrefs.SetFloat(PlayerPositionYKey, playerPosition.y);
        PlayerPrefs.SetFloat(PlayerPositionZKey, playerPosition.z);
        PlayerPrefs.SetInt(StageNumberKey, stageNumber);

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        // �÷��̾� ��ġ �� ���� �������� ��ȣ �ε�
        float posX = PlayerPrefs.GetFloat(PlayerPositionXKey, 0f);
        float posY = PlayerPrefs.GetFloat(PlayerPositionYKey, 0f);
        float posZ = PlayerPrefs.GetFloat(PlayerPositionZKey, 0f);
        playerPosition = new Vector3(posX, posY, posZ);
        stageNumber = PlayerPrefs.GetInt(StageNumberKey, 1);
    }

    public float GetTotalPlayTime()
    {
        // �� ���������� �ð��� �ҷ��� ���� ���
        float stage1Time = PlayerPrefs.GetFloat(Stage1TimeKey, 0f);
        float stage2Time = PlayerPrefs.GetFloat(Stage2TimeKey, 0f);
        float stage3Time = PlayerPrefs.GetFloat(Stage3TimeKey, 0f);

        return stage1Time + stage2Time + stage3Time;
    }

    public float GetStageTime(int stageNumber)
    {
        // Ư�� �������� �ð� ��ȯ
        if (stageNumber == 1)
            return PlayerPrefs.GetFloat(Stage1TimeKey, 0f);
        else if (stageNumber == 2)
            return PlayerPrefs.GetFloat(Stage2TimeKey, 0f);
        else if (stageNumber == 3)
            return PlayerPrefs.GetFloat(Stage3TimeKey, 0f);

        return 0f;
    }
}
