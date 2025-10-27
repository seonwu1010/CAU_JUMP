using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera startCamera;  // ������ ī�޶� ���⿡�� ����

    void Start()
    {
        // ��� ī�޶� ��Ȱ��ȭ�ϰ� ������ ī�޶� Ȱ��ȭ
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras)
        {
            cam.enabled = false;
        }

        // ������ ���� ī�޶� Ȱ��ȭ
        if (startCamera != null)
        {
            startCamera.enabled = true;
        }
        else
        {
            Debug.LogWarning("Start camera is not assigned!");
        }
    }
}
