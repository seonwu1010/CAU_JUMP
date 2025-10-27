using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 2.0f; // �̵� �ӵ�
    public LayerMask groundLayer; // �÷��� ���̾�
    public float detectionDistance = 0.5f; // �ٴ� ���� �Ÿ�
    private Rigidbody2D rbody;
    private bool movingRight = true;

    public float castOffset = 0.7f; // ����ĳ��Ʈ ���� ��ġ ������

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true; // ȸ�� ����
    }

    void Update()
    {
        Move();
        CheckPlatformEdge();
    }

    void Move()
    {
        // �̵� ���� ����
        float moveDirection = movingRight ? 1 : -1;
        rbody.velocity = new Vector2(moveDirection * moveSpeed, rbody.velocity.y);

        // �̵� ���⿡ ���� ������ �������� ��ȯ�Ͽ� �ð������� ���� ����
        transform.localScale = new Vector2(moveDirection, 1);
    }

    void CheckPlatformEdge()
    {
        // ���� ������ �ٴ��� ����
        Vector2 rightCastOrigin = new Vector2(transform.position.x + castOffset, transform.position.y);
        Vector2 leftCastOrigin = new Vector2(transform.position.x - castOffset, transform.position.y);

        // �� ���⿡ ���� ����ĳ��Ʈ �߻�
        RaycastHit2D rightGroundInfo = Physics2D.Raycast(rightCastOrigin, Vector2.down, detectionDistance, groundLayer);
        RaycastHit2D leftGroundInfo = Physics2D.Raycast(leftCastOrigin, Vector2.down, detectionDistance, groundLayer);

        // �̵� ���⿡ �´� ����ĳ��Ʈ�� �ٴ��� �������� ���ϸ� ���� ��ȯ
        if ((movingRight && !rightGroundInfo.collider) || (!movingRight && !leftGroundInfo.collider))
        {
            movingRight = !movingRight;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // ���� ����ĳ��Ʈ �ð�ȭ
        Vector2 rightCastOrigin = new Vector2(transform.position.x + castOffset, transform.position.y);
        Vector2 leftCastOrigin = new Vector2(transform.position.x - castOffset, transform.position.y);

        Gizmos.DrawLine(rightCastOrigin, rightCastOrigin + Vector2.down * detectionDistance);
        Gizmos.DrawLine(leftCastOrigin, leftCastOrigin + Vector2.down * detectionDistance);
    }
}
