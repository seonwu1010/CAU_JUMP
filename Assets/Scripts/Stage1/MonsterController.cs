using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float moveSpeed = 2.0f; // 이동 속도
    public LayerMask groundLayer; // 플랫폼 레이어
    public float detectionDistance = 0.5f; // 바닥 감지 거리
    private Rigidbody2D rbody;
    private bool movingRight = true;

    public float castOffset = 0.7f; // 레이캐스트 시작 위치 오프셋

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.freezeRotation = true; // 회전 고정
    }

    void Update()
    {
        Move();
        CheckPlatformEdge();
    }

    void Move()
    {
        // 이동 방향 설정
        float moveDirection = movingRight ? 1 : -1;
        rbody.velocity = new Vector2(moveDirection * moveSpeed, rbody.velocity.y);

        // 이동 방향에 따라 몬스터의 스케일을 전환하여 시각적으로 방향 조정
        transform.localScale = new Vector2(moveDirection, 1);
    }

    void CheckPlatformEdge()
    {
        // 양쪽 끝에서 바닥을 감지
        Vector2 rightCastOrigin = new Vector2(transform.position.x + castOffset, transform.position.y);
        Vector2 leftCastOrigin = new Vector2(transform.position.x - castOffset, transform.position.y);

        // 각 방향에 대해 레이캐스트 발사
        RaycastHit2D rightGroundInfo = Physics2D.Raycast(rightCastOrigin, Vector2.down, detectionDistance, groundLayer);
        RaycastHit2D leftGroundInfo = Physics2D.Raycast(leftCastOrigin, Vector2.down, detectionDistance, groundLayer);

        // 이동 방향에 맞는 레이캐스트가 바닥을 감지하지 못하면 방향 전환
        if ((movingRight && !rightGroundInfo.collider) || (!movingRight && !leftGroundInfo.collider))
        {
            movingRight = !movingRight;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        // 양쪽 레이캐스트 시각화
        Vector2 rightCastOrigin = new Vector2(transform.position.x + castOffset, transform.position.y);
        Vector2 leftCastOrigin = new Vector2(transform.position.x - castOffset, transform.position.y);

        Gizmos.DrawLine(rightCastOrigin, rightCastOrigin + Vector2.down * detectionDistance);
        Gizmos.DrawLine(leftCastOrigin, leftCastOrigin + Vector2.down * detectionDistance);
    }
}
