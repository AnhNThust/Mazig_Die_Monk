using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanelManager : MonoBehaviour
{
    [SerializeField] private string name;
    [SerializeField] private GameObject _quitPanel;

    public void ShowQuitPanel()
    {
        _quitPanel.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(name);
    }
}
