using System;
using System.Collections;
using UnityEngine;

public class RandomLightController : MonoBehaviour
{
    public UnityEngine.Rendering.Universal.Light2D spotLight;
    public float minInterval = 1.0f;    // 최소 간격 (초)
    public float maxInterval = 3.0f;    // 최대 간격 (초)
    public float transitionTime = 1.0f; // 서서히 켜지고 꺼지는 시간

    void Start()
    {
        if (spotLight == null)
        {
            spotLight = GetComponent<UnityEngine.Rendering.Universal.Light2D>();
        }

        spotLight.intensity = 0f;  // 처음에 빛을 꺼진 상태로 시작

        // 랜덤한 초기 대기 시간을 준 후 코루틴 시작
        float initialDelay = UnityEngine.Random.Range(0, maxInterval);
        StartCoroutine(DelayedStart(initialDelay));
    }

    IEnumerator DelayedStart(float delay)
    {
        yield return new WaitForSeconds(delay);  // 초기 랜덤 대기 시간
        StartCoroutine(ToggleLightSmoothly());    // 불이 켜지고 꺼지게 하는 코루틴 시작
    }

    IEnumerator ToggleLightSmoothly()
    {
        while (true)
        {
            // Light 켜기
            yield return StartCoroutine(ChangeLightIntensity(0f, 1f));

            // 랜덤 간격 대기
            float randomInterval = UnityEngine.Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomInterval);

            // Light 끄기
            yield return StartCoroutine(ChangeLightIntensity(1f, 0f));

            // 다시 랜덤 간격 대기
            randomInterval = UnityEngine.Random.Range(minInterval, maxInterval);
            yield return new WaitForSeconds(randomInterval);
        }
    }

    IEnumerator ChangeLightIntensity(float startIntensity, float endIntensity)
    {
        float elapsedTime = 0f;

        while (elapsedTime < transitionTime)
        {
            elapsedTime += Time.deltaTime;
            float newIntensity = Mathf.Lerp(startIntensity, endIntensity, elapsedTime / transitionTime);
            spotLight.intensity = newIntensity;
            yield return null;
        }

        spotLight.intensity = endIntensity;
    }
}
