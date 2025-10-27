using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float delayTime = 2.0f; // 발판이 통과되기 전 대기 시간
    private Collider2D platformCollider; // 발판의 충돌체
    private bool playerOnPlatform = false;
    private bool isFalling = false; // 발판이 통과 상태인지 확인하는 플래그

    void Start()
    {
        platformCollider = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && !isFalling && !playerOnPlatform)
        {
            playerOnPlatform = true;
            StartCoroutine(FallThroughAfterDelay());
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerOnPlatform = false;
            StopCoroutine(FallThroughAfterDelay()); // 플레이어가 발판을 떠날 때 코루틴 중지
        }
    }

    IEnumerator FallThroughAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        if (playerOnPlatform && !isFalling)
        {
            isFalling = true;

            platformCollider.enabled = false; // 발판 충돌체 비활성화
            yield return new WaitForSeconds(1.0f); // 발판이 사라진 상태 유지
            platformCollider.enabled = true; // 충돌체 다시 활성화
            isFalling = false; // 초기 상태로 리셋
        }
    }
}
