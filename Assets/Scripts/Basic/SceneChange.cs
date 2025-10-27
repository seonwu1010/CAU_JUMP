using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    // 다음 씬 이름 (또는 인덱스 번호) 설정
    public string nextSceneName;

    // 플레이어가 트리거에 들어오면 호출됨
    private void OnTriggerEnter2D(Collider2D other)
    {
        // 만약 충돌한 오브젝트의 태그가 "Player"라면
        if (other.CompareTag("Player"))
        {
            // 다음 씬 로드
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
