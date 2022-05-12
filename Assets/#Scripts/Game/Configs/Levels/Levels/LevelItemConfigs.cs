using UnityEngine;

[CreateAssetMenu(fileName = "LevelItem", menuName = MenuPath, order = MenuOrder)]
public class LevelItemConfigs : ScriptableObject, ILevelItemConfigs
{
    private const string MenuPath = "Configs/LevelItemConfigs";
    private const int MenuOrder = int.MinValue + 121;
    
    [SerializeField] private string _levelName = default;
    [SerializeField] private EAnimalType _animalType = EAnimalType.NONE;

    public string LevelName => _levelName;
    public EAnimalType AnimalType => _animalType;
}
