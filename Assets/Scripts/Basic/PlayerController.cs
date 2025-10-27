using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rbody;
    Animator animator;
    float axisH = 0.0f;
    public float speed = 3.0f;
    public float jump = 9.0f;
    public LayerMask groundLayer;
    bool onGround = false;

    public float chargeTime = 0f;
    public float maxChargeTime = 0.6f;
    public int frameSteps = 35;
    private bool isCharging = false;
    private bool canJump = true;
    private bool isJumping = false;

    public Vector2 boxCastSize = new Vector2(0.5f, 0.1f);
    public float knockbackForce = 5.0f;
    public float knockbackDuration = 0.5f;
    private bool isKnockedBack = false;

    public AudioClip walkSound;
    public AudioClip jumpLandSound;
    private AudioSource audioSource;
    private bool isWalkingSoundPlaying = false;

    private float jumpStartTime; // 점프 시작 시간

    void Start()
    {
        rbody = this.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        chargeTime = 0f;
        animator.SetBool("isCharging", false);
    }

    void Update()
    {
        // 피격 중이라면 조작을 불가
        if (isKnockedBack) return;

        // 점프 충전 중이더라도 방향키 입력 값을 저장
        axisH = Input.GetAxisRaw("Horizontal");

        // 충전 상태가 아닐 때만 수평 이동 처리
        if (onGround && !isCharging)
        {
            if (axisH > 0.0f)
            {
                transform.localScale = new Vector2(1, 1);
                animator.SetBool("isWalking", true);
                PlayWalkingSound();
            }
            else if (axisH < 0.0f)
            {
                transform.localScale = new Vector2(-1, 1);
                animator.SetBool("isWalking", true);
                PlayWalkingSound();
            }
            else
            {
                animator.SetBool("isWalking", false);
                rbody.velocity = new Vector2(0, rbody.velocity.y);
                StopWalkingSound();
            }
        }
        else
        {
            StopWalkingSound();
        }

        // 충전 시작 및 점프 수행 로직
        if (onGround && canJump && Input.GetKey(KeyCode.Space))
        {
            rbody.velocity = new Vector2(0, rbody.velocity.y); // 점프 중 미끄러짐 방지
            isCharging = true;
            chargeTime += Time.deltaTime;
            chargeTime = Mathf.Clamp(chargeTime, 0f, maxChargeTime);
            animator.SetBool("isCharging", true);
        }

        if (onGround && isCharging && Input.GetKeyUp(KeyCode.Space))
        {
            PerformJump();
            PlayJumpLandSound();
            chargeTime = 0f;
            isCharging = false;
            canJump = false;
            isJumping = true;
            animator.SetBool("isCharging", false);
            animator.SetBool("isJumping", true);
        }
    }


    void FixedUpdate()
    {
        bool wasOnGround = onGround;
        onGround = Physics2D.BoxCast(transform.position, boxCastSize, 0f, Vector2.down, 0.1f, groundLayer);

        // 점프 중일 때 착지 처리 방지
        if (isJumping && (Time.time - jumpStartTime) < 0.2f)
        {
            onGround = false;
        }

        if (onGround && !wasOnGround) // 착지 시 처리
        {
            canJump = true;
            isJumping = false;
            animator.SetBool("isJumping", false);
            PlayJumpLandSound();
        }

        if (!isKnockedBack && onGround && axisH != 0 && !isCharging)
        {
            rbody.velocity = new Vector2(speed * axisH, rbody.velocity.y);
        }
    }

    void PerformJump()
    {
        if (onGround && canJump)
        {
            float jumpPower = Mathf.Lerp(0, jump, chargeTime / maxChargeTime); // 충전된 점프력 계산

            // 점프 방향 결정: 충전 중 마지막 방향키 입력값 반영
            Vector2 jumpPw = new Vector2(axisH * speed, jumpPower);

            rbody.velocity = new Vector2(0, rbody.velocity.y); // 기존 수평 속도 초기화
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); // 점프력 적용
        }
    }


    void PlayWalkingSound()
    {
        if (!isWalkingSoundPlaying)
        {
            audioSource.clip = walkSound;
            audioSource.loop = true;
            audioSource.Play();
            isWalkingSoundPlaying = true;
        }
    }

    void StopWalkingSound()
    {
        if (isWalkingSoundPlaying)
        {
            audioSource.Stop();
            isWalkingSoundPlaying = false;
        }
    }

    void PlayJumpLandSound()
    {
        audioSource.PlayOneShot(jumpLandSound);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Vector2 knockbackDirection = (transform.position - other.transform.position).normalized;
            Knockback(knockbackDirection);
        }
    }

    void Knockback(Vector2 direction)
    {
        if (!isKnockedBack)
        {
            isKnockedBack = true;
            rbody.velocity = Vector2.zero;
            rbody.AddForce(direction * knockbackForce, ForceMode2D.Impulse);
            StartCoroutine(EndKnockback());
        }
    }

    IEnumerator EndKnockback()
    {
        yield return new WaitForSeconds(knockbackDuration);
        isKnockedBack = false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.down * 0.1f, boxCastSize);
    }
}
