using UnityEngine;

public class Sounder : MonoBehaviour
{
    [SerializeField] float timeDelay = 0;
    [SerializeField] string soundName = null;
    [SerializeField] int percentage = 100;

    public void Play()
    {
        var random = Random.Range(0, 100);
        if (random < percentage)
        {
            AudioManager.PlaySoundStatic(soundName);
        }
    }

    private void OnEnable()
    {
        CancelInvoke("Play");
        Invoke("Play", timeDelay);
    }
}
