using UnityEngine;

public class QuitPanelManager : MonoBehaviour
{
    public void Quit()
    {
        Application.Quit();
    }

    public void Cancel()
    {
        gameObject.SetActive(false);
    }
}
