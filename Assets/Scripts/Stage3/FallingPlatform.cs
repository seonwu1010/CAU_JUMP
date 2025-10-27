using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour
{
    public float delayTime = 2.0f; // ������ ����Ǳ� �� ��� �ð�
    private Collider2D platformCollider; // ������ �浹ü
    private bool playerOnPlatform = false;
    private bool isFalling = false; // ������ ��� �������� Ȯ���ϴ� �÷���

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
            StopCoroutine(FallThroughAfterDelay()); // �÷��̾ ������ ���� �� �ڷ�ƾ ����
        }
    }

    IEnumerator FallThroughAfterDelay()
    {
        yield return new WaitForSeconds(delayTime);

        if (playerOnPlatform && !isFalling)
        {
            isFalling = true;

            platformCollider.enabled = false; // ���� �浹ü ��Ȱ��ȭ
            yield return new WaitForSeconds(1.0f); // ������ ����� ���� ����
            platformCollider.enabled = true; // �浹ü �ٽ� Ȱ��ȭ
            isFalling = false; // �ʱ� ���·� ����
        }
    }
}
