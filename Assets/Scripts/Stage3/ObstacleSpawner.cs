using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab; // ��ֹ� ������
    public float spawnInterval = 2.0f; // ���� ����
    public float spawnYMin = -2.5f; // ��ֹ��� ������ Y�� �ּҰ�
    public float spawnYMax = 2.5f; // ��ֹ��� ������ Y�� �ִ밪
    public GameObject stage3Background; // stage3 ��� ������Ʈ

    private float leftBound;
    private float rightBound;

    void Start()
    {
        if (stage3Background != null)
        {
            SpriteRenderer sr = stage3Background.GetComponent<SpriteRenderer>();
            Vector3 stage3Position = stage3Background.transform.position;
            float stage3Width = sr.bounds.size.x;

            leftBound = stage3Position.x - (stage3Width / 2);
            rightBound = stage3Position.x + (stage3Width / 2);
        }

        StartCoroutine(SpawnObstacle());
    }

    IEnumerator SpawnObstacle()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);

            // ��ֹ��� ���ʿ��� ������ ���̷� ����
            bool spawnFromRight = Random.Range(0, 2) == 0;
            float xPosition = spawnFromRight ? rightBound : leftBound;
            Vector3 spawnPosition = new Vector3(xPosition, Random.Range(spawnYMin, spawnYMax), 0);

            GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, Quaternion.identity);
            Obstacle obstacleScript = obstacle.GetComponent<Obstacle>();
            obstacleScript.speed = spawnFromRight ? -Random.Range(3.0f, 6.0f) : Random.Range(3.0f, 6.0f);

            // ��ֹ����� ��� ���� ����
            obstacleScript.SetBounds(leftBound, rightBound);
        }
    }
}
