using Assets.Scripts.Shared.Constant;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BrickHaveDiamond : Brick
{
	[SerializeField] private GameObject diamond;
	[SerializeField] private float timeAnimFade;

	protected override void OnEnable()
	{
		base.OnEnable();

		AnimationClip[] diaAnimClips = diaAnim.runtimeAnimatorController.animationClips;
		foreach (var cl in diaAnimClips)
		{
			if (cl.name == "Fade")
			{
				timeAnimFade = cl.length; // Get length of time start animation hide
				break;
			}
		}

		StartCoroutine(BeginShow());
	}

	private IEnumerator BeginShow()
	{
		yield return new WaitForSeconds(0.2f); // Time show
		diamond.SetActive(true);

		yield return new WaitForSeconds(1f); // Time begin animation hide
		diaAnim.SetBool("isFade", true);

		yield return new WaitForSeconds(timeAnimFade); // wait when anim stop
		diamond.SetActive(false);

		if (GameData.CurrentLevel <= GameData.HardLevel)
			GameData.GameState = GameState.Ready;
		else
			isDiamondHide = true;
	}

	public override void OnClickHaveDiamond()
	{
		base.OnClickHaveDiamond();

		if (GameData.GameState != GameState.Ready) return;

		AudioManager.PlaySoundStatic("Gem_Ping");
		diamond.SetActive(true);
		diaAnim.Play("Idle");
		diaAnim.SetBool("isFade", false);
		diaAnim.SetTrigger("Appear");

		EventDispatcher.PostEvent(EventID.CountDiamond, 1);
		Destroy(GetComponent<Button>());
	}
}
