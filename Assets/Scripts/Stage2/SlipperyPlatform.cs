using UnityEngine;

public class SlipperyPlatform : MonoBehaviour
{
    public float slidingForceMagnitude = 3f; // 미끄러지는 힘의 크기

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // 플레이어의 현재 이동 방향을 기준으로 힘 계산
                Vector2 playerVelocity = playerRb.velocity;
                Vector2 slidingForce = new Vector2(Mathf.Sign(playerVelocity.x) * slidingForceMagnitude, 0f);

                // 힘 적용
                playerRb.AddForce(slidingForce, ForceMode2D.Force);
            }
        }
    }
}
