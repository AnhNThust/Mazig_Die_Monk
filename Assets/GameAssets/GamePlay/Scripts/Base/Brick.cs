using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Shared.Constant;
using UnityEngine;
using UnityEngine.UI;

public class Brick : MonoBehaviour
{
	[SerializeField] private string id;

	[SerializeField] private Transform leftBox;
	[SerializeField] private Transform aboveBox;
	[SerializeField] private Transform rightBox;
	[SerializeField] private Transform underBox;
	[SerializeField] private List<Transform> boxes = new();
	[SerializeField] protected Animator diaAnim;

	public List<Transform> Boxes { get => boxes; set => boxes = value; }
	protected bool isDiamondHide = false;

	protected virtual void OnEnable()
	{
		StartCoroutine(SetBoxMove(transform.position));
	}

	protected virtual void OnDisable()
	{
	}

	public virtual void OnClickHaveDiamond()
	{
	}

	public virtual void OnClickNoneDiamond()
	{
	}

	public void SetInfo(string id, Transform left, Transform right, Transform above, Transform under)
	{
		this.id = id;
		leftBox = left;
		rightBox = right;
		aboveBox = above;
		underBox = under;

		if (left != null)
			boxes.Add(left);
		if (right != null)
			boxes.Add(right);
		if (above != null)
			boxes.Add(above);
		if (under != null)
			boxes.Add(under);
	}

	public IEnumerator SetBoxMove(Vector3 posTarget)
	{
		yield return new WaitUntil(() => isDiamondHide);

		EventDispatcher.PostEvent(EventID.CanCheckHideBrick, true);
	}
}
