using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // 플레이어 오브젝트
    public Camera mainCamera1;  // 첫 번째 메인 카메라
    public Camera mainCamera2;  // 두 번째 메인 카메라
    public float transitionHeight = 5f;  // 화면 전환이 일어나는 높이(y 좌표)

    private bool isUsingCamera1 = true;  // 현재 첫 번째 카메라가 활성화 중인지 확인

    void Update()
    {
        // 플레이어가 transitionHeight 이상으로 올라가면 두 번째 카메라로 전환
        if (player.transform.position.y >= transitionHeight && isUsingCamera1)
        {
            SwitchToCamera2();
        }
        // 플레이어가 transitionHeight 이하로 내려가면 첫 번째 카메라로 전환
        else if (player.transform.position.y < transitionHeight && !isUsingCamera1)
        {
            SwitchToCamera1();
        }
    }

    void SwitchToCamera2()
    {
        mainCamera1.enabled = false;
        mainCamera2.enabled = true;
        isUsingCamera1 = false;
    }

    void SwitchToCamera1()
    {
        mainCamera2.enabled = false;
        mainCamera1.enabled = true;
        isUsingCamera1 = true;
    }
}