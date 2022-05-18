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
        
        _cameraManager = CameraManager.Instance;
    }

    public void PlayTutorial(Vector2 screenPosition)
    {
        _isActivated = true;
        
        _instanceHandTutorial = InstantiateHandController();

        Vector3 worldPosition = ConvertScreenToWorldPosition(screenPosition);

        _instanceHandTutorial.DoFingerAnimation(worldPosition);
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

    private Vector3 ConvertScreenToWorldPosition(Vector2 screenPosition)
    {
        var camera = _cameraManager.GetCameraItem(ECameraType.GAME).Camera;
        
        Vector3 worldPosition = camera.ScreenToWorldPoint(screenPosition);
        worldPosition.z = 0;
        
        return worldPosition;
    }
}
