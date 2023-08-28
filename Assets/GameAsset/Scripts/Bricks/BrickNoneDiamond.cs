using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BrickNoneDiamond : Brick
{
	protected override void OnEnable()
	{
		base.OnEnable();

		Button button = gameObject.GetComponent<Button>();
		button.onClick.RemoveAllListeners();
		button.onClick.AddListener(OnClickNoneDiamond);
	}

	protected override void OnClickNoneDiamond()
	{
		base.OnClickNoneDiamond();

		if (!canCheck) return;

		StartCoroutine(Defeat());		
	}

	private IEnumerator Defeat()
	{
		transform.Find("BrickNormal").gameObject.SetActive(false);
		transform.Find("BrickBreak").gameObject.SetActive(true);

		yield return new WaitForSeconds(1f);

		UIManager.ShowDefeat();
	}
}
