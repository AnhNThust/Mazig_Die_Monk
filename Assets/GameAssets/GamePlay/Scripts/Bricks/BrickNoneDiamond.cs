using Assets.Scripts.Shared.Constant;
using System.Collections;
using UnityEngine;

public class BrickNoneDiamond : Brick
{
	[SerializeField] private GameObject brickNormal;
	[SerializeField] private GameObject brickBreak;

	protected override void OnEnable()
	{
		base.OnEnable();
	}

	public override void OnClickNoneDiamond()
	{
		base.OnClickNoneDiamond();

		if (GameData.GameState != GameState.Ready) return;

		GameData.GameState = GameState.GameOver;
		AudioManager.PlaySoundStatic("Brick_Drop");
		StartCoroutine(Defeat());
	}

	private IEnumerator Defeat()
	{
		brickNormal.SetActive(false);
		brickBreak.SetActive(true);

		yield return new WaitForSeconds(1f);

		UIManager.ShowDefeat();
	}
}
