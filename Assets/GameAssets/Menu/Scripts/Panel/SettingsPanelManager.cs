using UnityEngine;

public class SettingsPanelManager : MonoBehaviour
{
    public void Unmute()
    {
        AudioListener.volume = 1f;
    }

    public void Mute()
    {
        AudioListener.volume = 0f;
    }

    public void TurnOnOffVolume()
    {
        if (AudioListener.volume > 0)
            AudioListener.volume = 0;
        else
            AudioListener.volume = 1;
    }
}
