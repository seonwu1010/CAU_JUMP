using UnityEngine;

public class EscalatorPlatform : MonoBehaviour
{
    public Vector2 diagonalStep = new Vector2(1, 1); // �밢������ �̵��� �Ÿ�
    public float moveSpeed = 2f; // �̵� �ӵ�
    public string playerTag = "Player"; // �÷��̾� �±�
    private Vector2 initialPosition; // �ʱ� ��ġ
    private Vector2 targetPosition; // ��ǥ ��ġ
    private bool isMoving = false; // �����̴� ���� Ȯ��

    private Transform player; // ���� ���� �ִ� �÷��̾� ����
    private bool playerOnPlatform = false; // �÷��̾ ���� ���� �ִ��� Ȯ��

    void Start()
    {
        // �ʱ� ��ġ ����
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            // ��ǥ ��ġ�� �ε巴�� �̵�
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

            // �÷��̾� ��ġ ����
            if (playerOnPlatform && player != null)
            {
                Vector2 movement = (Vector2)transform.position - currentPosition;
                player.transform.position += (Vector3)movement;
            }

            // ��ǥ ��ġ�� �����ϸ� ������ ����
            if (Vector2.Distance(transform.position, targetPosition) < 0.01f)
            {
                isMoving = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // �÷��̾ ���� ���� ���� ��
            player = collision.transform;
            playerOnPlatform = true;

            // ��ǥ ��ġ ����
            targetPosition += diagonalStep;
            isMoving = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // �÷��̾ ���ǿ��� ������ ��
            player = null;
            playerOnPlatform = false;

            // ���� ����ġ ����
            targetPosition = initialPosition;
            isMoving = true;
        }
    }
}
