using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5.0f; // ��ֹ��� �̵� �ӵ�
    private float leftBound;
    private float rightBound;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateSpriteDirection();
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // ��踦 ����� ��ֹ� �ı�
        if (transform.position.x < leftBound || transform.position.x > rightBound)
        {
            Destroy(gameObject);
        }
    }

    public void SetBounds(float left, float right)
    {
        leftBound = left;
        rightBound = right;
    }

    void UpdateSpriteDirection()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = speed < 0;
        }
    }
}
