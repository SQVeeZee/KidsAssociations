using UnityEngine;

public class GameController : MonoBehaviour
{
    private void Awake()
    {
        UIManager.Instance.DisableScreens();
    }

    private void Start()
    {
        UIManager.Instance.ShowScreen(EScreenType.MAIN_MENU,true);
    }
}
