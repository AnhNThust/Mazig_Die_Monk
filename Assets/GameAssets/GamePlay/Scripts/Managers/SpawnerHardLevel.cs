using Assets.Scripts.Shared.Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerHardLevel : MonoBehaviour
{
	[SerializeField] private int rowColNumber;
	[SerializeField] private GameObject brickNonePrefab;
	[SerializeField] private GameObject brickDiamondPrefab;
	[SerializeField] private GameObject brickItemPrefab;
	[SerializeField] private int numberDiamond;

	[SerializeField] private Transform container;

	private GameObject[,] brickArray;
	private List<string> listIdHide = new();
	private List<string> listIdDiamond = new();

	private void OnEnable()
	{
		CalculateMap();

		StartCoroutine(Spawn());
	}

	private void CalculateMap()
	{
		rowColNumber = GameData.CurrentMap + 2;
		brickArray = new GameObject[rowColNumber, rowColNumber];

		if (GameData.CurrentLevel < GameData.TotalLevel && GameData.CurrentLevel % rowColNumber == 1)
		{
			GameData.NumberDiamond = rowColNumber + GameData.CurrentLevel / rowColNumber;
		}
		numberDiamond = GameData.NumberDiamond;

		EventDispatcher.PostEvent(EventID.TotalDiamond, numberDiamond);
	}

	private IEnumerator Spawn()
	{
		yield return new WaitUntil(() => GameData.GameState == GameState.PreReady);

		// Get Random brick hide
		GetRandomBrickHide();

		// Get Random brick diamond
		GetRandomDiamondSpawn();

		// Spawn all brick
		SpawnBrick();

		yield return new WaitForSeconds(0.5f);

		EventDispatcher.PostEvent(EventID.CanDetachGridLayout, true);

		yield return new WaitForSeconds(0.5f);

		// Hide some brick
		HideBrick();

		// Show diamond

		// hide diamond

		// move brick
	}

	private void GetRandomBrickHide()
	{
		int count = 0;

		while (true)
		{
			// main logic
			int iRand = Random.Range(0, rowColNumber);
			int jRand = Random.Range(0, rowColNumber);
			string pos = $"{iRand}{jRand}";

			if (listIdHide.Contains(pos))
				continue;
			else
				listIdHide.Add(pos);

			count++;
			if (count >= rowColNumber)
				break;
		}
	}

	private void GetRandomDiamondSpawn()
	{
		int count = 0;

		while (true)
		{
			// main logic
			int iRand = Random.Range(0, rowColNumber);
			int jRand = Random.Range(0, rowColNumber);
			string pos = $"{iRand}{jRand}";

			if (listIdDiamond.Contains(pos) || listIdHide.Contains(pos))
				continue;
			else
				listIdDiamond.Add(pos);

			count++;
			if (count >= numberDiamond)
				break;
		}
	}

	private void SpawnBrick()
	{
		for (int i = 0; i < rowColNumber; i++)
		{
			for (int j = 0; j < rowColNumber; j++)
			{
				GameObject brick;
				string pos = $"{i}{j}";
				if (!listIdDiamond.Contains(pos))
				{
					brick = Instantiate(brickNonePrefab);
				}
				else
				{
					brick = Instantiate(brickDiamondPrefab);
				}

				brick.name = $"Brick_{i}_{j}";
				brick.transform.SetParent(container);
				brick.transform.localPosition = Vector3.zero;
				brick.transform.localScale = Vector3.one;
				brick.SetActive(true);

				brickArray[i, j] = brick;
			}
		}
	}

	private void HideBrick()
	{
		for (int i = 0; i < rowColNumber; i++)
		{
			for (int j = 0; j < rowColNumber; j++)
			{
				string pos = $"{i}{j}";
				if (!listIdHide.Contains(pos)) continue;

				Brick brick = brickArray[i, j].GetComponent<Brick>();
				SetInfoHideBrick(pos, brick);
				brickArray[i, j].SetActive(false);
			}
		}
	}

	private void SetInfoHideBrick(string id, Brick brick)
	{
		Transform left, top, right, down;

		int td = (int)char.GetNumericValue(id[0]); // i - top down
		int lr = (int)char.GetNumericValue(id[1]); // j - left right

		if (lr - 1 < 0)
			left = null;
		else
			left = brickArray[td, lr - 1].transform;

		if (lr + 1 >= rowColNumber)
			right = null;
		else
			right = brickArray[td, lr + 1].transform;

		if (td - 1 < 0)
			top = null;
		else
			top = brickArray[td - 1, lr].transform;

		if (td + 1 >= rowColNumber)
			down = null;
		else
			down = brickArray[td + 1, lr].transform;

		brick.SetInfo(id, left, right, top, down);
	}
}
