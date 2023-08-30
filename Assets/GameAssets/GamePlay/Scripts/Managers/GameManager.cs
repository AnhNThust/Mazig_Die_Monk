using Assets.Scripts.Shared.Constant;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	[SerializeField] private int totalDiamond = 0;
	[SerializeField] private int counter = 0;

	private bool canCheckCount = false;

	private void OnEnable()
	{
		GameData.GameState = GameState.None;
		EventDispatcher.AddEvent(EventID.TotalDiamond, OnGetTotalDiamond);
		EventDispatcher.AddEvent(EventID.CountDiamond, OnCountDiamondChanged);

		StartCoroutine(SetGameState());
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.TotalDiamond, OnGetTotalDiamond);
		EventDispatcher.RemoveEvent(EventID.CountDiamond, OnCountDiamondChanged);
	}

	private IEnumerator SetGameState()
	{
		yield return new WaitUntil(() => GameData.GameState == GameState.Ready);

		while (true)
		{
			yield return new WaitForSeconds(1f);

			if (GameData.GameState == GameState.Continue)
			{
				GameData.CurrentLevel++;
				SceneManager.LoadSceneAsync("GamePlay");
			}
			else if (GameData.GameState == GameState.Victory)
			{
				AudioManager.PlaySoundStatic("Victory");
				UIManager.ShowVictory();
			}
		}
	}

	private void OnCountDiamondChanged(object obj)
	{
		counter += (int)obj;

		if (!canCheckCount || counter < totalDiamond) return;

		if (GameData.CurrentLevel < GameData.TotalLevel)
		{
			GameData.GameState = GameState.PreContinue; // De chay anim hoac tinh toan neu co
		}
		else
		{
			GameData.GameState = GameState.Victory;
		}
	}

	private void OnGetTotalDiamond(object obj)
	{
		totalDiamond = (int)obj;
		canCheckCount = true;
	}
}
