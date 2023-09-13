using Assets.Scripts.Shared.Constant;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelManager : MonoBehaviour
{
    [Header("========== Scene ==========")]
    [SerializeField] private string nameMenuScene;

    public void ReturnMenu()
    {
        Time.timeScale = 1.0f;
		GameData.CurrentLevel = 1;
		GameData.CurrentLife = GameData.TotalLife;
        SceneManager.LoadScene(nameMenuScene);
    }

    public void Continue()
    {
        Time.timeScale = 1.0f;
        gameObject.SetActive(false);
    }
}
