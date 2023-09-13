using System;
using Assets.Scripts.Shared.Constant;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapProgressBarManager : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject lvlSplit;
    [SerializeField] private TextMeshProUGUI currMap;
    [SerializeField] private TextMeshProUGUI nextMap;
    [SerializeField] private Transform lvlPlitParent;
    [SerializeField] private int maxVal;
    [SerializeField] private int val;

    private void Start()
    {
        slider = GetComponent<Slider>();

        slider.maxValue = GameData.TotalLevel;
        slider.value = GameData.CurrentLevel;

        CreateAllLevelSplit();

        currMap.text = $"MAP {GameData.CurrentMap}";
        nextMap.text = $"MAP {GameData.CurrentMap + 1}";
    }

    private void CreateAllLevelSplit()
    {
        float width = lvlPlitParent.GetComponent<RectTransform>().rect.width;
        float beginPos = - width / 2;
        float distance = width / GameData.TotalLevel;

        for (int i = 0; i < GameData.TotalLevel - 1; i++)
        {
            GameObject spObject = Instantiate(lvlSplit, lvlPlitParent, false);
            spObject.name = $"lvl_split_{i + 1}";
            spObject.transform.localPosition = new(beginPos + (i + 1) * distance, 0, 0);
            spObject.SetActive(true);
        }
    }
}
