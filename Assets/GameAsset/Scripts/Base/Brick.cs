using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Brick : MonoBehaviour
{
	[SerializeField] private int id;

	[SerializeField] private Transform leftBox;
	[SerializeField] private Transform aboveBox;
	[SerializeField] private Transform rightBox;
	[SerializeField] private Transform underBox;
	[SerializeField] private List<Transform> boxes = new();
	[SerializeField] protected bool canCheck = false;

	public List<Transform> Boxes { get => boxes; set => boxes = value; }

	protected virtual void OnEnable()
	{
		EventDispatcher.AddEvent(EventID.CanCheckChanged, OnCanCheckChanged);
	}

	protected virtual void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.CanCheckChanged, OnCanCheckChanged);
	}

	private void OnCanCheckChanged(object obj)
	{
		canCheck = (bool)obj;
	}

	protected virtual void OnClickHaveDiamond()
	{
	}

	protected virtual void OnClickNoneDiamond()
	{
	}

	public void SetInfo(int id, Transform left, Transform right, Transform above, Transform under)
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
}
