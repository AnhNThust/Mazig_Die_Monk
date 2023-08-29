using Assets.Scripts.Shared.Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerNormalLevel : MonoBehaviour
{
	[SerializeField] private int rowColNumber;
	[SerializeField] private GameObject brickNonePrefab;
	[SerializeField] private GameObject brickDiamondPrefab;
	[SerializeField] private GameObject brickItemPrefab;
	[SerializeField] private int numberDiamond;

	[SerializeField] private Transform container;

	private GameObject[,] brickArray;
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

		GetRandomDiamondSpawn();

		SpawnBrick();
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

			if (listIdDiamond.Contains(pos))
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
}
