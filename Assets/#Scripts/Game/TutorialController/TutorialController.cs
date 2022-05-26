using UnityEngine;

public class TutorialController : Singleton<TutorialController>
{
    [SerializeField] private HandTutorialController _handTutorialController = null;

    private HandTutorialController _instanceHandTutorial = null;

    private bool _isActivated = false;

    public void PlayTutorial(Vector2 screenPosition)
    {
        if (_isActivated) return;
        
        _isActivated = true;
        
        _instanceHandTutorial = InstantiateHandController();

        _instanceHandTutorial.DoFingerAnimation(screenPosition);
    }

    public void ResetTutorial()
    {
        if (!_isActivated) return;
        
        _instanceHandTutorial.ResetPulseTween();
        
        Destroy(_instanceHandTutorial.gameObject);

        _isActivated = false;
    }

    private HandTutorialController InstantiateHandController()
    {
        return Instantiate(_handTutorialController, transform);
    }
}
