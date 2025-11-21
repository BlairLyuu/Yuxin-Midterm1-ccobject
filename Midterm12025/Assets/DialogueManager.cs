using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject dialoguePanel;  // 对话Panel
    public GameObject optionPanel;    // 选项Panel
    public GameObject resultPanel;    // 结果Panel（选择后显示的panel）

    [Header("Dialogue UI")]
    public TextMeshProUGUI dialogueText;
    public Button nextButton;
    public Button closeButton;

    [Header("Option UI")]
    public Button option1Button;  // 选项1按钮
    public Button option2Button;  // 选项2按钮
    public Button option3Button;  // 选项3按钮（可选）
    public TextMeshProUGUI option1Text;
    public TextMeshProUGUI option2Text;
    public TextMeshProUGUI option3Text;

    [Header("Result UI")]
    public TextMeshProUGUI resultText;  // 结果文本
    public Button confirmButton;  // 确认按钮

    // 对话数据
    private string[] currentDialogue;
    private int currentLineIndex = 0;

    // 选项数据
    private string selectedOption = "";

    // 打字机效果
    public float typingSpeed = 0.05f;
    private bool isTyping = false;

    void Start()
    {
        // 初始化：隐藏所有panel
        dialoguePanel.SetActive(false);
        optionPanel.SetActive(false);
        resultPanel.SetActive(false);

        // 绑定对话按钮
        nextButton.onClick.AddListener(ShowNextLine);
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(EndDialogue);
        }

        // 绑定选项按钮
        option1Button.onClick.AddListener(() => OnOptionSelected("选项1"));
        option2Button.onClick.AddListener(() => OnOptionSelected("选项2"));
        if (option3Button != null)
        {
            option3Button.onClick.AddListener(() => OnOptionSelected("选项3"));
        }

        // 绑定确认按钮
        confirmButton.onClick.AddListener(OnConfirmResult);
    }

    // 开始对话
    public void StartDialogue(string[] lines)
    {
        currentDialogue = lines;
        currentLineIndex = 0;

        // 显示对话Panel
        ShowPanel(dialoguePanel);

        // 显示第一句
        ShowNextLine();
    }

    // 显示下一句对话
    public void ShowNextLine()
    {
        if (isTyping)
        {
            StopAllCoroutines();
            dialogueText.text = currentDialogue[currentLineIndex];
            isTyping = false;
            return;
        }

        if (currentLineIndex < currentDialogue.Length)
        {
            StartCoroutine(TypeSentence(currentDialogue[currentLineIndex]));
            currentLineIndex++;
        }
        else
        {
            // 对话结束，显示选项
            ShowOptions();
        }
    }

    // 打字机效果
    System.Collections.IEnumerator TypeSentence(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    // 显示选项Panel
    public void ShowOptions()
    {
        dialoguePanel.SetActive(false);
        optionPanel.SetActive(true);

        // 设置选项文本（可以从外部传入）
        option1Text.text = "选项1：接受任务";
        option2Text.text = "选项2：拒绝任务";
        if (option3Text != null)
        {
            option3Text.text = "选项3：稍后再说";
        }
    }

    // 当玩家选择一个选项
    void OnOptionSelected(string optionName)
    {
        selectedOption = optionName;

        // 隐藏选项Panel，显示结果Panel
        optionPanel.SetActive(false);
        resultPanel.SetActive(true);

        // 根据选择显示不同结果
        ShowResult(optionName);
    }

    // 显示结果
    void ShowResult(string option)
    {
        switch (option)
        {
            case "选项1":
                resultText.text = "你接受了任务！冒险即将开始。";
                break;
            case "选项2":
                resultText.text = "你拒绝了任务。NPC看起来有些失望。";
                break;
            case "选项3":
                resultText.text = "你决定稍后再考虑。";
                break;
            default:
                resultText.text = "未知选择";
                break;
        }
    }

    // 确认结果后关闭
    void OnConfirmResult()
    {
        resultPanel.SetActive(false);
        // 这里可以添加其他逻辑，比如给玩家奖励、解锁任务等
        Debug.Log("玩家选择了：" + selectedOption);
    }

    // 显示指定Panel，隐藏其他
    void ShowPanel(GameObject panelToShow)
    {
        dialoguePanel.SetActive(false);
        optionPanel.SetActive(false);
        resultPanel.SetActive(false);

        panelToShow.SetActive(true);
    }

    // 结束对话
    public void EndDialogue()
    {
        dialoguePanel.SetActive(false);
        optionPanel.SetActive(false);
        resultPanel.SetActive(false);
        currentLineIndex = 0;
        StopAllCoroutines();
        isTyping = false;
    }
}