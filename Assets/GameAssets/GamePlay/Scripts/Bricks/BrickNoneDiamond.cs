using Assets.Scripts.Shared.Constant;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

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

		brickNormal.SetActive(false);
		brickBreak.SetActive(true);
		AudioManager.PlaySoundStatic("Brick_Drop");
		diaAnim.SetTrigger("Broken");
		EventDispatcher.PostEvent(EventID.LifeChanged, 1);

		if (GameData.CurrentLife <= 0)
		{
			GameData.GameState = GameState.GameOver;
			StartCoroutine(Defeat());
		}

		Destroy(GetComponent<Button>());
	}

	private IEnumerator Defeat()
	{
		yield return new WaitForSeconds(2f);

		UIManager.ShowDefeat();
	}
}
