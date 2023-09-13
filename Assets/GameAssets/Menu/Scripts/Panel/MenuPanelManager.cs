using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPanelManager : MonoBehaviour
{
    [SerializeField] private string nameGamePlayScene;
    [SerializeField] private GameObject _quitPanel;

    private void Start()
    {
        AudioManager.PlayMusicStatic("bg_music");
    }

    public void ShowQuitPanel()
    {
        _quitPanel.SetActive(true);
    }

    public void Play()
    {
        SceneManager.LoadScene(nameGamePlayScene);
    }
}
