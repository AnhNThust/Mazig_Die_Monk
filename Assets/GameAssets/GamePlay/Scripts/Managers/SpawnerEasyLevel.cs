using Assets.Scripts.Shared.Constant;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnerEasyLevel : MonoBehaviour
{
	[SerializeField] private int rowColNumber;
	[SerializeField] private GameObject brickNonePrefab;
	[SerializeField] private GameObject brickDiamondPrefab;
	[SerializeField] private GameObject brickItemPrefab;
	[SerializeField] private int numberDiamond;

	[SerializeField] private Transform container;

	private GameObject[,] brickArray;

	private void OnEnable()
	{
		CalculateMap();

		StartCoroutine(Spawn());
	}

	private void CalculateMap()
	{
		rowColNumber = GameData.CurrentMap + 2;
		brickArray = new GameObject[rowColNumber, rowColNumber];

		GameData.NumberDiamond = rowColNumber;
		numberDiamond = rowColNumber;

		EventDispatcher.PostEvent(EventID.TotalDiamond, numberDiamond);
	}

	private IEnumerator Spawn()
	{
		yield return new WaitUntil(() => GameData.GameState == GameState.PreReady);

		GetRandomSpawnType();
	}

	private void GetRandomSpawnType()
	{
		int rand = Random.Range(0, 3);

		switch (rand)
		{
			case 0: // Spawn doc
				SpawnType1();
				break;
			case 1: // Spawn ngang
				SpawnType2();
				break;
			case 2: // Spawn cheo
				SpawnType3();
				break;
		}
	}

	private void SpawnType1()
	{
		int iId = Random.Range(0, 3);

		for (int i = 0; i < rowColNumber; i++)
		{
			for (int j = 0; j < rowColNumber; j++)
			{
				GameObject brick;

				if (i != iId)
				{
					brick = Instantiate(brickNonePrefab);
				}
				else
				{
					brick = Instantiate(brickDiamondPrefab);
				}

				brick.name = $"Brick_{i}_{j}";
				brick.transform.SetParent(container, false);
				brick.transform.localPosition = Vector3.zero;
				brick.transform.localScale = Vector3.one;
				brick.SetActive(true);

				brickArray[i, j] = brick;
			}
		}
	}

	private void SpawnType2()
	{
		int jId = Random.Range(0, 3);

		for (int i = 0; i < rowColNumber; i++)
		{
			for (int j = 0; j < rowColNumber; j++)
			{
				GameObject brick;

				if (j != jId)
				{
					brick = Instantiate(brickNonePrefab);
				}
				else
				{
					brick = Instantiate(brickDiamondPrefab);
				}

				brick.name = $"Brick_{i}_{j}";
				brick.transform.SetParent(container, false);
				brick.transform.localPosition = Vector3.zero;
				brick.transform.localScale = Vector3.one;
				brick.SetActive(true);

				brickArray[i, j] = brick;
			}
		}
	}

	// TODO: Làm 1 Template về điều kiện giữa i và j để tối ưu code

	private void SpawnType3()
	{
		int rand = Random.Range(0, 2);

		if (rand == 0)
		{
			for (int i = 0; i < rowColNumber; i++)
			{
				for (int j = 0; j < rowColNumber; j++)
				{
					GameObject brick;

					if (i != j)
					{
						brick = Instantiate(brickNonePrefab);
						
					}
					else
					{
						brick = Instantiate(brickDiamondPrefab);
					}

					brick.name = $"Brick_{i}_{j}";
					brick.transform.SetParent(container, false);
					brick.transform.localPosition = Vector3.zero;
					brick.transform.localScale = Vector3.one;
					brick.SetActive(true);

					brickArray[i, j] = brick;
				}
			}
		}
		else
		{
			for (int i = 0; i < rowColNumber; i++)
			{
				for (int j = 0; j < rowColNumber; j++)
				{
					GameObject brick;

					if (i + j != rowColNumber - 1)
					{
						brick = Instantiate(brickNonePrefab);
					}
					else
					{
						brick = Instantiate(brickDiamondPrefab);
					}

					brick.name = $"Brick_{i}_{j}";
					brick.transform.SetParent(container, false);
					brick.transform.localPosition = Vector3.zero;
					brick.transform.localScale = Vector3.one;
					brick.SetActive(true);

					brickArray[i, j] = brick;
				}
			}
		}
	}
}
