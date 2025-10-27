using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;   // �÷��̾� ������Ʈ
    public Camera mainCamera1;  // ù ��° ���� ī�޶�
    public Camera mainCamera2;  // �� ��° ���� ī�޶�
    public float transitionHeight = 5f;  // ȭ�� ��ȯ�� �Ͼ�� ����(y ��ǥ)

    private bool isUsingCamera1 = true;  // ���� ù ��° ī�޶� Ȱ��ȭ ������ Ȯ��

    void Update()
    {
        // �÷��̾ transitionHeight �̻����� �ö󰡸� �� ��° ī�޶�� ��ȯ
        if (player.transform.position.y >= transitionHeight && isUsingCamera1)
        {
            SwitchToCamera2();
        }
        // �÷��̾ transitionHeight ���Ϸ� �������� ù ��° ī�޶�� ��ȯ
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