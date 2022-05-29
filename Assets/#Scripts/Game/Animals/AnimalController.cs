using System;
using Spine;
using Spine.Unity;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    private const int track_index = 0;
    
    [SerializeField] private SkeletonAnimation _skeletonAnimation = null;
    [SerializeField] private ExtraTail _extraTail = null;
    [SerializeField] private InputTracker _inputTracker = null;
    
    [Header("Animations")]
    [SpineAnimation] [SerializeField]
    private string _noAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _idleAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _sadAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _happyAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _tapStoyachiAnimationName = default;

    private Spine.AnimationState _spineAnimationState;
    private Skeleton _skeleton;
    private Slot _tailSlot = null;

    private void OnEnable()
    {
        _inputTracker.onPointerDown += OnAnimalClicked;
    }

    private void OnDisable()
    {
        _inputTracker.onPointerDown -= OnAnimalClicked;
    }

    public void Initialize(string slotTail)
    {
        SetData(slotTail);
        
        SetTailVisibilityState(false);

        PlayAnimation(EAnimalAnimationType.IDLE);
    }

    private void SetData(string slotTail)
    {
        _spineAnimationState = _skeletonAnimation.AnimationState;
        
        _skeleton = _skeletonAnimation.Skeleton;
        
        _tailSlot = _skeleton.FindSlot(slotTail);
    }

    public void SetNewTail(EAnimalType animalType, Action callback = null)
    {
        AnimalsContainer.Instance.GetAnimalsTail(animalType, OnLoadTail);

        void OnLoadTail(Sprite tailSprite)
        {
            _extraTail.EnableExtraTail(tailSprite);

            PlayAnimation(EAnimalAnimationType.NO, delegate
            {
                _extraTail.DisableExtraTail();

                callback?.Invoke();
            });
        }
    }

    public void SetRightTail(Action callback)
    {
        _extraTail.DisableExtraTail();
        
        SetTailVisibilityState(true);

        _skeleton.SetToSetupPose();
        
        PlayAnimation(EAnimalAnimationType.HAPPY, callback);
    }

    public void DoAnimalSad()
    {
        PlayAnimation(EAnimalAnimationType.SAD);
    }
    
    private void OnAnimalClicked()
    {
        PlayAnimation(EAnimalAnimationType.CLICKED);
    }

    private void PlayAnimation(EAnimalAnimationType animalAnimationType, Action callback)
    {
        PlayAnimation(animalAnimationType);
        
        TrackEntry trackEntry = _spineAnimationState.GetCurrent(0);
        trackEntry.Complete += Complete;
        
        // -= ???
        
        void Complete(TrackEntry trackEntryCallback)
        {
            callback?.Invoke();
        }
    }

    private void PlayAnimation(EAnimalAnimationType animalAnimationType)
    {
        switch (animalAnimationType)
        {
            case EAnimalAnimationType.IDLE:
                PlayAnimation(_idleAnimationName,true);
                break;

            case EAnimalAnimationType.NO:
                PlayAnimation(_noAnimationName,false);
                AddAnimation(_idleAnimationName,true);
                break;

            case EAnimalAnimationType.SAD:
                PlayAnimation(_sadAnimationName,false);
                AddAnimation(_idleAnimationName,true);
                break;

            case EAnimalAnimationType.HAPPY:
                PlayAnimation(_happyAnimationName,false);
                break;
            
            case EAnimalAnimationType.CLICKED:
                PlayAnimation(_tapStoyachiAnimationName,false);
                break;
        }
    }

    private void PlayAnimation(string animationName, bool isLooping)
    {
        _spineAnimationState.SetAnimation(0, animationName, isLooping);
    }

    private void AddAnimation(string animationName, bool isLooping, float delay = 0f)
    {
        _spineAnimationState.AddAnimation(0, animationName, isLooping, delay);
    }

    private void SetTailVisibilityState(bool state)
    {
        _tailSlot.A = state ? 1 : 0;
    }
}