using UnityEngine;

public class SlipperyPlatform : MonoBehaviour
{
    public float slidingForceMagnitude = 3f; // �̲������� ���� ũ��

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D playerRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (playerRb != null)
            {
                // �÷��̾��� ���� �̵� ������ �������� �� ���
                Vector2 playerVelocity = playerRb.velocity;
                Vector2 slidingForce = new Vector2(Mathf.Sign(playerVelocity.x) * slidingForceMagnitude, 0f);

                // �� ����
                playerRb.AddForce(slidingForce, ForceMode2D.Force);
            }
        }
    }
}
