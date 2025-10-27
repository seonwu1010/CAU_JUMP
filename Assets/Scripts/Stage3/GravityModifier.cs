using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityModifier : MonoBehaviour
{
    public float maxHeight = 15f; // 최대 높이
    public float minGravityScale = 0.1f; // 최소 중력 스케일 (0.1까지 줄어듦)
    private float initialGravityScale;
    private Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        initialGravityScale = playerRigidbody.gravityScale; // 초기 중력 스케일 저장
    }

    void Update()
    {
        // 특정 씬에서만 중력 감소 적용
        if (SceneManager.GetActiveScene().name == "Stage3") // "YourStageName"을 해당 스테이지 이름으로 변경
        {
            float currentHeight = transform.position.y;
            float normalizedHeight = Mathf.Clamp01(currentHeight / maxHeight); // 높이를 0~1로 정규화
            float gravityScale = Mathf.Lerp(initialGravityScale, minGravityScale, normalizedHeight); // 높이에 따라 중력 스케일 줄임

            playerRigidbody.gravityScale = gravityScale; // 중력 적용
        }
        else
        {
            playerRigidbody.gravityScale = initialGravityScale; // 기본 중력
        }
    }
}
