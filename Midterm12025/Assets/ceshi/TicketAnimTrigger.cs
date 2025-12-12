using UnityEngine;
using UnityEngine.UI;

public class TicketAnimTrigger : MonoBehaviour
{
    private Animator animator;
    private Button button;
    private bool isClicked = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(TriggerAnimation);
        }
    }
    void TriggerAnimation()
    {
        if (isClicked) return;
       
        if (animator != null)
        {
            animator.SetTrigger("on");
            isClicked = true;
            Debug.Log("Animation triggered");
        }
    }
}

