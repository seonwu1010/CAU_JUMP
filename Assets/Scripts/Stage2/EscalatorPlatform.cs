using UnityEngine;

public class EscalatorPlatform : MonoBehaviour
{
    public Vector2 diagonalStep = new Vector2(1, 1); // 대각선으로 이동할 거리
    public float moveSpeed = 2f; // 이동 속도
    public string playerTag = "Player"; // 플레이어 태그
    private Vector2 initialPosition; // 초기 위치
    private Vector2 targetPosition; // 목표 위치
    private bool isMoving = false; // 움직이는 상태 확인

    private Transform player; // 발판 위에 있는 플레이어 참조
    private bool playerOnPlatform = false; // 플레이어가 발판 위에 있는지 확인

    void Start()
    {
        // 초기 위치 설정
        initialPosition = transform.position;
        targetPosition = initialPosition;
    }

    void Update()
    {
        if (isMoving)
        {
            // 목표 위치로 부드럽게 이동
            Vector2 currentPosition = transform.position;
            transform.position = Vector2.MoveTowards(currentPosition, targetPosition, moveSpeed * Time.deltaTime);

            // 플레이어 위치 유지
            if (playerOnPlatform && player != null)
            {
                Vector2 movement = (Vector2)transform.position - currentPosition;
                player.transform.position += (Vector3)movement;
            }

            // 목표 위치에 도달하면 움직임 멈춤
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
            // 플레이어가 발판 위에 있을 때
            player = collision.transform;
            playerOnPlatform = true;

            // 목표 위치 설정
            targetPosition += diagonalStep;
            isMoving = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(playerTag))
        {
            // 플레이어가 발판에서 떨어질 때
            player = null;
            playerOnPlatform = false;

            // 발판 원위치 복귀
            targetPosition = initialPosition;
            isMoving = true;
        }
    }
}
