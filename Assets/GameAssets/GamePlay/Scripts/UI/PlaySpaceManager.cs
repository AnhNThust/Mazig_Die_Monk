using Assets.Scripts.Shared.Constant;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaySpaceManager : MonoBehaviour
{
	[SerializeField] private int brickSize;
	[SerializeField] private GridLayoutGroup grid;
	[SerializeField] private TextMeshProUGUI mapTitle;
	[SerializeField] private TextMeshProUGUI levelTitle;
	[SerializeField] private Text numberDiamondTitle;
	[SerializeField] private Text lifeTitle;
	[SerializeField] private GameObject _uiPause;

	private int numberRowCol = 0;
	private bool canDetach = false;
	private int counterNumberDiamond = 0;

	private void OnEnable()
	{
		EventDispatcher.AddEvent(EventID.CanDetachGridLayout, OnCanDetachGridLayout);
		EventDispatcher.AddEvent(EventID.CountDiamond, OnCountDiamondChanged);
		EventDispatcher.AddEvent(EventID.LifeChanged, OnLifeChanged);

		numberRowCol = GameData.CurrentMap + 2;
		mapTitle.text = $"Map: {GameData.CurrentMap}";
		levelTitle.text = $"Level: {GameData.CurrentLevel}/{GameData.TotalLevel}";
		numberDiamondTitle.text = $": {counterNumberDiamond}/{GameData.NumberDiamond}";
		lifeTitle.text = $": {GameData.CurrentLife}/{GameData.TotalLife}";

		StartCoroutine(CalCulatePlaySpace());
		StartCoroutine(DetachGridLayout());
	}

	private void OnDisable()
	{
		EventDispatcher.RemoveEvent(EventID.CanDetachGridLayout, OnCanDetachGridLayout);
	}

	private void OnCanDetachGridLayout(object obj)
	{
		canDetach = (bool)obj;
	}

	private IEnumerator CalCulatePlaySpace()
	{
		// Calculate size of Brick
		int size = SpacePlayInfo.PlaySpaceSize;
		int padding = SpacePlayInfo.Padding;
		int spacing = SpacePlayInfo.Spacing;
		brickSize = (size - 2 * padding - (numberRowCol - 1) * spacing) / numberRowCol;

		grid.cellSize = new Vector2(brickSize, brickSize);

		yield return new WaitForSeconds(0.5f);

		GameData.GameState = GameState.PreReady;
	}

	private IEnumerator DetachGridLayout()
	{
		yield return new WaitUntil(() => canDetach);

		grid.GetComponent<GridLayoutGroup>().enabled = false;
	}

	private void OnCountDiamondChanged(object obj)
	{
		counterNumberDiamond++;
		numberDiamondTitle.text = $": {counterNumberDiamond}/{GameData.NumberDiamond}";
	}

	private void OnLifeChanged(object obj)
	{
		GameData.CurrentLife -= (int)obj;
		lifeTitle.text = $": {GameData.CurrentLife}/{GameData.TotalLife}";
	}

	public void ShowPausePanel()
	{
		Time.timeScale = 0f;
		_uiPause.SetActive(true);
	}
}
