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
    private TrackEntry _trackEntry = null;
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

        PlayIdleAnimation();
    }

    private void SetData(string slotTail)
    { 
        _spineAnimationState = _skeletonAnimation.AnimationState;
        _trackEntry = _spineAnimationState.GetCurrent(track_index);
        
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

                PlayIdleAnimation();
                
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
        PlayAnimation(EAnimalAnimationType.CLICKED, PlayIdleAnimation);
    }

    private void PlayIdleAnimation()
    {
        PlayAnimation(EAnimalAnimationType.IDLE);
    }


    private void PlayAnimation(EAnimalAnimationType animalAnimationType, Action callback)
    {
//        UnSubscribe(_trackEntry);
        _trackEntry.Start += OnStartAnimation;

        PlayAnimation(animalAnimationType);

        void OnStartAnimation(TrackEntry trackEntry)
        {
            _trackEntry.Complete += OnCompleteAnimation;
        }
        
        void OnCompleteAnimation(TrackEntry trackEntry)
        {
            _trackEntry.End += UnSubscribe;

            callback?.Invoke();
        }

        void UnSubscribe(TrackEntry trackEntry)
        {
            Debug.Log("Unsubscribe");
            _trackEntry.Start -= OnStartAnimation;
            _trackEntry.Complete -= OnCompleteAnimation;
            _trackEntry.End -= UnSubscribe;
        }
    }

    private void PlayAnimation(EAnimalAnimationType animalAnimationType)
    {
        switch (animalAnimationType)
        {
            case EAnimalAnimationType.IDLE:
                _spineAnimationState.SetAnimation(0, _idleAnimationName, true);
                break;

            case EAnimalAnimationType.NO:
                _spineAnimationState.SetAnimation(0, _noAnimationName, false);
                break;

            case EAnimalAnimationType.SAD:
                _spineAnimationState.SetAnimation(0, _sadAnimationName, true);
                break;

            case EAnimalAnimationType.HAPPY:
                _spineAnimationState.SetAnimation(0, _happyAnimationName, false);
                break;
            
            case EAnimalAnimationType.CLICKED:
                _spineAnimationState.SetAnimation(0, _tapStoyachiAnimationName, false);
                break;
        }
    }

    private void SetTailVisibilityState(bool state)
    {
        _tailSlot.A = state ? 1 : 0;
    }
}