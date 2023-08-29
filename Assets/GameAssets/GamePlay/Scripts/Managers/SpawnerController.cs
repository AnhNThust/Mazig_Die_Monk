using Assets.Scripts.Shared.Constant;
using System.Collections;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
	[SerializeField] private SpawnerEasyLevel easyLevel;
	[SerializeField] private SpawnerNormalLevel normalLevel;
	[SerializeField] private SpawnerHardLevel hardLevel;

	private void OnEnable()
	{
		StartCoroutine(ActiveGameLevel());
	}

	private IEnumerator ActiveGameLevel()
	{
		yield return new WaitUntil(() => GameData.GameState == GameState.PreReady);

		easyLevel.enabled = GameData.CurrentLevel <= GameData.NormalLevel;
		normalLevel.enabled = GameData.CurrentLevel > GameData.NormalLevel && 
			GameData.CurrentLevel <= GameData.HardLevel;
		hardLevel.enabled = GameData.CurrentLevel > GameData.HardLevel;
	}
}
