using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ProfessorInteraction : MonoBehaviour
{
    public GameObject questionUI; // 질문 UI 패널
    public TextMeshProUGUI questionText; // 질문 텍스트
    public Button[] answerButtons; // 답변 버튼 배열

    public string[] questions; // 질문 텍스트 배열
    public string[,] answers; // 답변 옵션 2D 배열 (질문별 답변)
    public int[] correctAnswerIndices; // 각 질문의 정답 인덱스 배열

    private int currentCorrectIndex; // 현재 정답 인덱스

    void Start()
    {
        questionUI.SetActive(false); // 시작 시 질문 UI 비활성화

        // 질문 초기화
        questions = new string[]
        {
            "중앙대 310관은 몇주년 기념 건물인가요?",
            "중앙대학교의 시작은 언제인가요?",
            "중앙대는 어떤 정신을 교육이념으로 하나요?"
        };

        answers = new string[,]
        {
            { "90", "100", "110" }, // 질문 1의 답변
            { "1916.10.11", "1916.10.17", "1918.10.11" }, // 질문 2의 답변
            { "의와 참", "법과 질서", "진리와 봉사" } // 질문 3의 답변
        };

        correctAnswerIndices = new int[] { 1, 1, 0 };

        // 버튼 이벤트 연결
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // 현재 인덱스 캡처
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerButtons[index]));
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ShowQuestion();
        }
    }

    void ShowQuestion()
    {
        // 랜덤으로 질문 선택
        int randomIndex = Random.Range(0, questions.Length);
        questionText.text = questions[randomIndex]; // 질문 텍스트 설정
        currentCorrectIndex = correctAnswerIndices[randomIndex]; // 현재 질문의 정답 인덱스 설정

        // 버튼 텍스트 업데이트
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[randomIndex, i];
        }

        questionUI.SetActive(true); // 질문 UI 활성화
        Time.timeScale = 0f; // 게임 일시 정지
    }

    public void OnAnswerSelected(Button selectedButton)
    {
        // 선택한 버튼이 정답인지 확인
        int selectedIndex = System.Array.IndexOf(answerButtons, selectedButton);
        bool isCorrect = selectedIndex == currentCorrectIndex;

        AnswerQuestion(isCorrect);
    }

    public void AnswerQuestion(bool isCorrect)
    {
        Time.timeScale = 1f; // 게임 재개
        questionUI.SetActive(false); // 질문 UI 비활성화

        if (isCorrect)
        {
            Debug.Log("정답! 다음 씬으로 이동합니다.");
            SceneManager.LoadScene("Stage3"); // 다음 씬으로 전환
        }
        else
        {
            Debug.Log("오답! 플레이어를 팅겨냅니다.");
            KnockbackPlayer();
        }
    }

    void KnockbackPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // 기존 속도를 초기화
            rb.velocity = Vector2.zero;

            Vector2 knockbackForce = new Vector2(-20f, 5f);
            rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }
}