using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ProfessorInteraction : MonoBehaviour
{
    public GameObject questionUI; // ���� UI �г�
    public TextMeshProUGUI questionText; // ���� �ؽ�Ʈ
    public Button[] answerButtons; // �亯 ��ư �迭

    public string[] questions; // ���� �ؽ�Ʈ �迭
    public string[,] answers; // �亯 �ɼ� 2D �迭 (������ �亯)
    public int[] correctAnswerIndices; // �� ������ ���� �ε��� �迭

    private int currentCorrectIndex; // ���� ���� �ε���

    void Start()
    {
        questionUI.SetActive(false); // ���� �� ���� UI ��Ȱ��ȭ

        // ���� �ʱ�ȭ
        questions = new string[]
        {
            "�߾Ӵ� 310���� ���ֳ� ��� �ǹ��ΰ���?",
            "�߾Ӵ��б��� ������ �����ΰ���?",
            "�߾Ӵ�� � ������ �����̳����� �ϳ���?"
        };

        answers = new string[,]
        {
            { "90", "100", "110" }, // ���� 1�� �亯
            { "1916.10.11", "1916.10.17", "1918.10.11" }, // ���� 2�� �亯
            { "�ǿ� ��", "���� ����", "������ ����" } // ���� 3�� �亯
        };

        correctAnswerIndices = new int[] { 1, 1, 0 };

        // ��ư �̺�Ʈ ����
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int index = i; // ���� �ε��� ĸó
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
        // �������� ���� ����
        int randomIndex = Random.Range(0, questions.Length);
        questionText.text = questions[randomIndex]; // ���� �ؽ�Ʈ ����
        currentCorrectIndex = correctAnswerIndices[randomIndex]; // ���� ������ ���� �ε��� ����

        // ��ư �ؽ�Ʈ ������Ʈ
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[randomIndex, i];
        }

        questionUI.SetActive(true); // ���� UI Ȱ��ȭ
        Time.timeScale = 0f; // ���� �Ͻ� ����
    }

    public void OnAnswerSelected(Button selectedButton)
    {
        // ������ ��ư�� �������� Ȯ��
        int selectedIndex = System.Array.IndexOf(answerButtons, selectedButton);
        bool isCorrect = selectedIndex == currentCorrectIndex;

        AnswerQuestion(isCorrect);
    }

    public void AnswerQuestion(bool isCorrect)
    {
        Time.timeScale = 1f; // ���� �簳
        questionUI.SetActive(false); // ���� UI ��Ȱ��ȭ

        if (isCorrect)
        {
            Debug.Log("����! ���� ������ �̵��մϴ�.");
            SceneManager.LoadScene("Stage3"); // ���� ������ ��ȯ
        }
        else
        {
            Debug.Log("����! �÷��̾ �ðܳ��ϴ�.");
            KnockbackPlayer();
        }
    }

    void KnockbackPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // ���� �ӵ��� �ʱ�ȭ
            rb.velocity = Vector2.zero;

            Vector2 knockbackForce = new Vector2(-20f, 5f);
            rb.AddForce(knockbackForce, ForceMode2D.Impulse);
        }
    }
}