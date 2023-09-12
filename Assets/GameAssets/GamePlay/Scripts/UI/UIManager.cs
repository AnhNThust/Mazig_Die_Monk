using UnityEngine;

public class UIManager : MonoBehaviour
{
	[SerializeField] private GameObject _uiVictory;
	[SerializeField] private GameObject _uiDefeat;

    static UIManager instance;

	private void OnEnable()
	{
		if (instance != null) Debug.LogError("Only 1 UIManager allow exists");

		instance = this;
	}

	// =================================================

	private void EnableVictory()
	{
		Time.timeScale = 0f;
		_uiVictory.SetActive(true);
	}

	private void EnableDefeat()
	{
		Time.timeScale = 0f;
		_uiDefeat.SetActive(true);
	}

	// =================================================

	public static void ShowVictory()
	{
		instance.EnableVictory();
	}

	public static void ShowDefeat()
	{
		instance.EnableDefeat();
	}
}
