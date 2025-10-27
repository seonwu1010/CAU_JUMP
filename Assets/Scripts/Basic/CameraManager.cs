using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera startCamera;  // 시작할 카메라를 여기에서 지정

    void Start()
    {
        // 모든 카메라를 비활성화하고 시작할 카메라만 활성화
        Camera[] allCameras = Camera.allCameras;
        foreach (Camera cam in allCameras)
        {
            cam.enabled = false;
        }

        // 지정된 시작 카메라를 활성화
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
