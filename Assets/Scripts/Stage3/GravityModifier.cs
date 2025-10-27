using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GravityModifier : MonoBehaviour
{
    public float maxHeight = 15f; // �ִ� ����
    public float minGravityScale = 0.1f; // �ּ� �߷� ������ (0.1���� �پ��)
    private float initialGravityScale;
    private Rigidbody2D playerRigidbody;

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        initialGravityScale = playerRigidbody.gravityScale; // �ʱ� �߷� ������ ����
    }

    void Update()
    {
        // Ư�� �������� �߷� ���� ����
        if (SceneManager.GetActiveScene().name == "Stage3") // "YourStageName"�� �ش� �������� �̸����� ����
        {
            float currentHeight = transform.position.y;
            float normalizedHeight = Mathf.Clamp01(currentHeight / maxHeight); // ���̸� 0~1�� ����ȭ
            float gravityScale = Mathf.Lerp(initialGravityScale, minGravityScale, normalizedHeight); // ���̿� ���� �߷� ������ ����

            playerRigidbody.gravityScale = gravityScale; // �߷� ����
        }
        else
        {
            playerRigidbody.gravityScale = initialGravityScale; // �⺻ �߷�
        }
    }
}
