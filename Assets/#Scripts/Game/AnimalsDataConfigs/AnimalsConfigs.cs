using UnityEngine;

[CreateAssetMenu(fileName = "AnimalsConfigs", menuName = MenuPath, order = MenuOrder)]
public class AnimalsConfigs : ScriptableObject
{
    private const string MenuPath = "Configs/AnimalsConfigs";
    private const int MenuOrder = int.MinValue + 121;
    
    [SerializeField] private string _tailsPath = null;

    public string TailsPath => _tailsPath;
}
