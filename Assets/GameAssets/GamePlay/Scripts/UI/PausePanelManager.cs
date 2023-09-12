using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PausePanelManager : MonoBehaviour
{
    [Header("========== Scene ==========")]
    [SerializeField] private string nameMenuScene;

    public void ReturnMenu()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(nameMenuScene);
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
