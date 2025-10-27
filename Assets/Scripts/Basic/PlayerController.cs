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

    private float jumpStartTime; // ���� ���� �ð�

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
        // �ǰ� ���̶�� ������ �Ұ�
        if (isKnockedBack) return;

        // ���� ���� ���̴��� ����Ű �Է� ���� ����
        axisH = Input.GetAxisRaw("Horizontal");

        // ���� ���°� �ƴ� ���� ���� �̵� ó��
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

        // ���� ���� �� ���� ���� ����
        if (onGround && canJump && Input.GetKey(KeyCode.Space))
        {
            rbody.velocity = new Vector2(0, rbody.velocity.y); // ���� �� �̲����� ����
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

        // ���� ���� �� ���� ó�� ����
        if (isJumping && (Time.time - jumpStartTime) < 0.2f)
        {
            onGround = false;
        }

        if (onGround && !wasOnGround) // ���� �� ó��
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
            float jumpPower = Mathf.Lerp(0, jump, chargeTime / maxChargeTime); // ������ ������ ���

            // ���� ���� ����: ���� �� ������ ����Ű �Է°� �ݿ�
            Vector2 jumpPw = new Vector2(axisH * speed, jumpPower);

            rbody.velocity = new Vector2(0, rbody.velocity.y); // ���� ���� �ӵ� �ʱ�ȭ
            rbody.AddForce(jumpPw, ForceMode2D.Impulse); // ������ ����
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
