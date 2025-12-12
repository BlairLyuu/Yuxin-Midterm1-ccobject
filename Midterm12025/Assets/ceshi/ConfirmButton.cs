using UnityEngine;
using UnityEngine.UI;

public class ConfirmButton : MonoBehaviour
{
    public MinePlayerController minePlayerControllerScript;
    private Button button;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        minePlayerControllerScript.BanPlayerMoving(true);
    }
}
