using Assets.Scripts.Shared.Constant;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
	[SerializeField] private int rowColNumber;
	[SerializeField] private Transform parentBrick;
	[SerializeField] private Button brickPrefab;
	[SerializeField] private Image diamondPrefab;
	[SerializeField] private int numberDiamond;

	[SerializeField] private List<Button> brickList = new List<Button>();
	[SerializeField] private List<int> idHaveDiamondList = new List<int>();
	[SerializeField] private List<Transform> listBrickHide = new List<Transform>();
	[SerializeField] private bool canSpawn = false;

	private void OnEnable()
	{
		CalculateLevel();

		StartCoroutine(Spawn());
	}

	private void CalculateLevel()
	{
		rowColNumber = GameData.CurrentMap + 2;

		if (GameData.CurrentLevel < GameData.TotalLevel && GameData.CurrentLevel % rowColNumber == 1)
		{
			GameData.NumberDiamond = rowColNumber + (GameData.CurrentLevel / rowColNumber);
		}

		numberDiamond = GameData.NumberDiamond;

		EventDispatcher.PostEvent(EventID.TotalDiamond, numberDiamond);
		EventDispatcher.AddEvent(EventID.CanSpawnBrick, OnCanSpawnBrick);
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.CanSpawnBrick, OnCanSpawnBrick);
	}

	private void OnCanSpawnBrick(object obj)
	{
		canSpawn = (bool)obj;
	}

	private IEnumerator Spawn()
	{
		yield return new WaitUntil(() => canSpawn);

		// Spawn and Show Brick
		SpawnBrick();

		yield return new WaitForSeconds(0.5f);

		if (GameData.CurrentLevel >= GameData.HardLevel)
		{
			// An brick
			GetRandomBrickHide();
		}

		yield return new WaitForSeconds(0.5f);

		if (GameData.CurrentLevel <= rowColNumber)
		{
			GetBrickHaveDiamond();
		}
		else
		{
			// Get index brick contain diamond
			GetRandomBrick();
		}

		// Set listener for button
		SetTypeOfBrick();

		// Spawn and Show Diamond in Brick with index in index list
		SpawnDiamond();

		yield return new WaitForSeconds(2f);

		// Hide Diamond
		HideDiamond();
	}

	private void SpawnBrick()
	{
		for (int i = 0; i < rowColNumber * rowColNumber; i++)
		{
			Button brick = Instantiate(brickPrefab);
			brick.name = $"Brick_{i}";
			brick.transform.SetParent(parentBrick);
			brick.transform.localPosition = Vector3.zero;
			brick.transform.localScale = Vector3.one;

			brick.gameObject.SetActive(true);
			brickList.Add(brick);
		}

		EventDispatcher.PostEvent(EventID.CanDetachGridLayout, true);
	}

	// Cap do de, Brick chua diamond la cac duong cheo, hang doc hoac hang ngang
	private void GetBrickHaveDiamond()
	{
		int n = 2 + 2 * rowColNumber;
		int id;
		do
		{
			id = Random.Range(0, n);
		} while (GameData.Indexes.Contains(id));
		GameData.Indexes.Add(id);

		// doc, ngang, cheo
		if (id >= 0 && id < rowColNumber)
		{
			for (int i = 0; i < rowColNumber; i++)
			{
				idHaveDiamondList.Add(id + rowColNumber * i);
			}
		}
		else if (id >= rowColNumber && id < rowColNumber * 2)
		{
			int id0 = (id - rowColNumber) * rowColNumber;

			for (int i = 0; i < rowColNumber; i++)
			{
				idHaveDiamondList.Add(id0 + i);
			}
		}
		else
		{
			int id0 = (id % rowColNumber == 0) ? 0 : rowColNumber - 1;

			if (id0 == 0)
			{
				for (int i = 0; i < rowColNumber; i++)
				{
					idHaveDiamondList.Add(i + i * rowColNumber);
				}
			}
			else
			{
				for (int i = 0; i < rowColNumber; i++)
				{
					idHaveDiamondList.Add(i * (rowColNumber - 1) + (rowColNumber - 1));
				}
			}
		}
	}

	// TODO: Them logic ngau nhien chon cac brick co ngoc
	private void GetRandomBrick()
	{
		int count = 0;
		int index = 0;

		while (count < numberDiamond)
		{
			index = Random.Range(0, brickList.Count);

			if (idHaveDiamondList.Contains(index) || listBrickHide.Contains(brickList[index].transform)) continue;

			idHaveDiamondList.Add(index);
			count++;
		}
	}

	// Set loai brick
	private void SetTypeOfBrick()
	{
		for (int i = 0; i < brickList.Count; i++)
		{
			Brick b = idHaveDiamondList.Contains(i) ?
				brickList[i].gameObject.AddComponent(typeof(BrickHaveDiamond)) as BrickHaveDiamond :
				brickList[i].gameObject.AddComponent(typeof(BrickNoneDiamond)) as BrickNoneDiamond;

			SetBrickInfo(i, b);
		}
	}

	// Tao diamond
	private void SpawnDiamond()
	{
		for (int i = 0; i < idHaveDiamondList.Count; i++)
		{
			Image diamond = Instantiate(diamondPrefab) as Image;
			diamond.name = $"Diamond";
			diamond.transform.SetParent(brickList[idHaveDiamondList[i]].transform);
			diamond.transform.localPosition = Vector3.zero;
			diamond.transform.localScale = 1.75f * Vector3.one;

			diamond.gameObject.SetActive(true);
		}
	}

	// An diamond
	private void HideDiamond()
	{
		for (int i = 0; i < idHaveDiamondList.Count; i++)
		{
			Transform diamond = brickList[idHaveDiamondList[i]].transform.Find("Diamond");
			diamond.gameObject.SetActive(false);
		}

		if (GameData.CurrentLevel < GameData.HardLevel)
		{
			EventDispatcher.PostEvent(EventID.CanCheckChanged, true);
		}
		else
		{
			EventDispatcher.PostEvent(EventID.CanCheckHideBrick, true);
		}
	}

	private void SetBrickInfo(int id, Brick brick)
	{
		Transform left, right, above, under;

		// Check left
		if (id % rowColNumber == 0)
		{
			left = null;
		}
		else
		{
			left = brickList[id - 1].transform;
		}

		// Check right
		if (id % rowColNumber == (rowColNumber - 1))
		{
			right = null;
		}
		else
		{
			right = brickList[id + 1].transform;
		}

		// Check above
		if ((id - rowColNumber) >= 0)
		{
			above = brickList[id - rowColNumber].transform;
		}
		else
		{
			above = null;
		}

		// Check under
		if ((id + rowColNumber) <= (brickList.Count - 1))
		{
			under = brickList[id + rowColNumber].transform;
		}
		else
		{
			under = null;
		}

		brick.SetInfo(id, left, right, above, under);
	}

	private void GetRandomBrickHide()
	{
		int count = 0;

		while (count < rowColNumber)
		{
			int id = Random.Range(0, brickList.Count);

			if (listBrickHide.Contains(brickList[id].transform) || idHaveDiamondList.Contains(id))
				continue;

			listBrickHide.Add(brickList[id].transform);

			count++;
		}

		for (int i = 0; i < listBrickHide.Count; i++)
		{
			listBrickHide[i].gameObject.SetActive(false);
		}
	}
}
