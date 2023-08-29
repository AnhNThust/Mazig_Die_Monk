using Assets.Scripts.Shared.Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxMoveManager : MonoBehaviour
{
	[SerializeField] private bool checkHideBrick = false;
	[SerializeField] private List<Transform> hideBricks = new List<Transform>();

	private void OnEnable()
	{
		EventDispatcher.AddEvent(EventID.CanCheckHideBrick, OnCanCheckHideBrick);

		StartCoroutine(SetAllBoxMove());
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.CanCheckHideBrick, OnCanCheckHideBrick);
	}

	private IEnumerator SetAllBoxMove()
	{
   		yield return new WaitUntil(() => checkHideBrick);

		// Get list hide brick
		for (int i = 0; i < transform.childCount; i++)
		{
			if (transform.GetChild(i).gameObject.activeSelf) continue;

			hideBricks.Add(transform.GetChild(i));
		}

		for (int i = 0; i < hideBricks.Count; i++)
		{
			List<Transform> boxAroundHideBrick = hideBricks[i].GetComponent<Brick>().Boxes;

			Transform brick;
			do
			{
				int id = Random.Range(0, boxAroundHideBrick.Count);
				brick = boxAroundHideBrick[id];
			} while (brick.GetComponent<BoxMove>() != null);

			BoxMove move = brick.gameObject.AddComponent(typeof(BoxMove)) as BoxMove;
			move.Target = hideBricks[i].position;
		}
	}

	private void OnCanCheckHideBrick(object obj)
	{
		checkHideBrick = (bool)obj;
	}
}
