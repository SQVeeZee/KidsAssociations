using UnityEngine;

public class TutorialController : Singleton<TutorialController>
{
    [SerializeField] private HandTutorialController _handTutorialController = null;

    private CameraManager _cameraManager = null;
    private HandTutorialController _instanceHandTutorial = null;

    private bool _isActivated = false;

    protected override void Awake()
    {
        base.Awake();

        Initialize();
    }

    public void PlayTutorial(Vector2 screenPosition)
    {
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

    private void Initialize()
    {
        _cameraManager = CameraManager.Instance;
    }
}
