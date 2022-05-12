using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsConfigs", menuName = MenuPath, order = MenuOrder)]
public class LevelsConfigs : ScriptableObject
{
    private const string MenuPath = "Configs/LevelsConfigs";
    private const int MenuOrder = int.MinValue + 121;

    [SerializeField] private List<LevelItemConfigs> _levelItemConfigs = new List<LevelItemConfigs>();

    public List<LevelItemConfigs> LevelItemConfigs => _levelItemConfigs;
    public int GetLevelConfigsCount => LevelItemConfigs.Count;
}