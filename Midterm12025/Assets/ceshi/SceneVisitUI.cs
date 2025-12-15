using UnityEngine;
using TMPro;

public class SceneVisitUI : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private TMP_Text counterText;
    [SerializeField] private TMP_Text messageText;
    [SerializeField] private TMP_Text timeText;

    [Header("PlayerPrefs Key")]
    [SerializeField] private string prefsKey = "SceneOneIndex";

    public GameObject ThreeCanvas;

    private void Start()
    {
        int count = PlayerPrefs.GetInt(prefsKey, 0);
        Refresh(count);

        if (count == 3 && ThreeCanvas != null)
        {
            ThreeCanvas.gameObject.SetActive(true);
        }
    }

    public void Refresh(int count)
    {
        if (counterText != null)
            counterText.text = $"Visits: {count + 1}";

        if (messageText != null)
            messageText.text = GetMessage(count);

        if (timeText != null)
            timeText.text = GetTimeText(count);
    }

    private string GetMessage(int count)
    {
        switch (count)
        {
            case 0: return "Welcome. Take a ticket, take a drink, and follow what calls you.";
            case 1: return "Magnetic Rose ！ Are you still living inside a memory?";
            case 2: return "The Handmaiden ！ Do you run, the way Izumi and Sook-hee once did?";
            case 3: return "Robot Dreams ！ The door is open. It¨s time to move on.";
            default: return "You keep coming back. The space keeps its record of you.";
        }
    }

    private string GetTimeText(int count)
    {
        switch (count)
        {
            case 0: return "17:40 ！ Lobby";
            case 1: return "18:15 ！ Magnetic Rose";
            case 2: return "19:15 ！ The Handmaiden";
            case 3: return "20:15 ！ Robot Dreams";
            default: return "！";
        }
    }
}
