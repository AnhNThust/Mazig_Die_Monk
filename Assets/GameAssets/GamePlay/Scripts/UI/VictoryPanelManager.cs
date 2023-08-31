using Assets.Scripts.Shared.Constant;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryPanelManager : MonoBehaviour
{
	[SerializeField] private string nameScene;

	public void Replay()
	{
		Time.timeScale = 1f;
		GameData.CurrentLife = GameData.TotalLife;
		SceneManager.LoadSceneAsync(nameScene);
	}

	public void Next()
	{
		// Set Info Game
		GameData.CurrentMap++;
		GameData.CurrentLevel = 1;
		GameData.TotalLevel += 5;
		GameData.NumberDiamond = GameData.CurrentMap + 2;
		//GameData.Indexes.Clear();
		GameData.CurrentLife = GameData.TotalLife;
		GameData.HardLevel = Mathf.FloorToInt(0.65f * GameData.TotalLevel);

		Time.timeScale = 1f;
		SceneManager.LoadSceneAsync(nameScene);
	}
}
