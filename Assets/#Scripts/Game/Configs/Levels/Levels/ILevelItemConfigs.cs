public interface ILevelItemConfigs
{
    string LevelName { get; }
    EAnimalType AnimalType { get; }
    TailsPanel TailsPanel { get; }
    AnimalController AnimalController { get; }
}
