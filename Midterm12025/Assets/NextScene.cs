using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class NextScene : MonoBehaviour
{
    [Header("≥°æ∞…Ë÷√")]
    public string targetSceneName;



    public void NextScenes()
    {
        SceneManager.LoadScene(targetSceneName);
    }
}