using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // ���� �� �̸� (�Ǵ� �ε��� ��ȣ) ����
    public string nextSceneName;

    // �÷��̾ Ʈ���ſ� ������ ȣ���
    private void OnTriggerEnter2D(Collider2D other)
    {
        // ���� �浹�� ������Ʈ�� �±װ� "Player"���
        if (other.CompareTag("Player"))
        {
            // ���� �� �ε�
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
