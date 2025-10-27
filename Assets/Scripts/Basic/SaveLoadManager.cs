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

    public Vector3 playerPosition; // 플레이어 위치
    public int stageNumber; // 현재 스테이지 번호

    public void SaveGame(float currentStageTime, Vector3 playerPosition, int stageNumber)
    {
        // 현재 스테이지의 시간 저장
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

        // 플레이어 위치 및 현재 스테이지 번호 저장
        PlayerPrefs.SetFloat(PlayerPositionXKey, playerPosition.x);
        PlayerPrefs.SetFloat(PlayerPositionYKey, playerPosition.y);
        PlayerPrefs.SetFloat(PlayerPositionZKey, playerPosition.z);
        PlayerPrefs.SetInt(StageNumberKey, stageNumber);

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        // 플레이어 위치 및 현재 스테이지 번호 로드
        float posX = PlayerPrefs.GetFloat(PlayerPositionXKey, 0f);
        float posY = PlayerPrefs.GetFloat(PlayerPositionYKey, 0f);
        float posZ = PlayerPrefs.GetFloat(PlayerPositionZKey, 0f);
        playerPosition = new Vector3(posX, posY, posZ);
        stageNumber = PlayerPrefs.GetInt(StageNumberKey, 1);
    }

    public float GetTotalPlayTime()
    {
        // 각 스테이지의 시간을 불러와 총합 계산
        float stage1Time = PlayerPrefs.GetFloat(Stage1TimeKey, 0f);
        float stage2Time = PlayerPrefs.GetFloat(Stage2TimeKey, 0f);
        float stage3Time = PlayerPrefs.GetFloat(Stage3TimeKey, 0f);

        return stage1Time + stage2Time + stage3Time;
    }

    public float GetStageTime(int stageNumber)
    {
        // 특정 스테이지 시간 반환
        if (stageNumber == 1)
            return PlayerPrefs.GetFloat(Stage1TimeKey, 0f);
        else if (stageNumber == 2)
            return PlayerPrefs.GetFloat(Stage2TimeKey, 0f);
        else if (stageNumber == 3)
            return PlayerPrefs.GetFloat(Stage3TimeKey, 0f);

        return 0f;
    }
}
