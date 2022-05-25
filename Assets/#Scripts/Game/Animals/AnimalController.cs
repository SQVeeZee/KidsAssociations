using System;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using Tools.Resources;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    [SerializeField] private SkeletonAnimation _skeletonAnimation = null;
    [SerializeField] private SkeletonRenderer _skeletonRenderer = null;
    [SerializeField] private ExtraTail _extraTail = null;
    
    [Header("Animations")]
    [SpineAnimation] [SerializeField]
    private string _noAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _idleAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _sadAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _happyAnimationName = default;

    private Spine.AnimationState _spineAnimationState;
    private Skeleton _skeleton;
    private Slot _tailSlot = null;

    public void Initialize(string slotTail)
    {
        SetData(slotTail);
        
        SetTailVisibilityState(false);
        
        PlayAnimation(EAnimalAnimationType.IDLE);
    }

    private void SetData(string slotTail)
    {
        var slotTailName = slotTail;
        
        _spineAnimationState = _skeletonAnimation.AnimationState;
        _skeleton = _skeletonAnimation.Skeleton;
        
        _tailSlot = _skeleton.FindSlot(slotTailName);
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
                
                PlayAnimation(EAnimalAnimationType.IDLE);
                
                callback?.Invoke(); 
            });
        }
    }

    public void SetRightTail(Action callback)
    {
        SetTailVisibilityState(true);

        _skeleton.SetToSetupPose();
        
        PlayAnimation(EAnimalAnimationType.HAPPY, callback);
    }

    public void DoAnimalSad()
    {
        PlayAnimation(EAnimalAnimationType.SAD);
    }

    private void PlayAnimation(EAnimalAnimationType animalAnimationType, Action callback = null)
    {
        _spineAnimationState.Complete -= OnCompleteAnimation;
        
        switch (animalAnimationType)
        {
            case EAnimalAnimationType.IDLE: 
                _spineAnimationState.SetAnimation(0, _idleAnimationName, true);
                break;
            
            case EAnimalAnimationType.NO: 
                _spineAnimationState.SetAnimation(0, _noAnimationName, false);
                break;

            case EAnimalAnimationType.SAD: 
                _spineAnimationState.SetAnimation(0, _sadAnimationName, false);
                break;
            
            case EAnimalAnimationType.HAPPY: 
                _spineAnimationState.SetAnimation(0, _happyAnimationName, false);
                break;
        }

        if (callback == null) return;
        
        _spineAnimationState.Complete += OnCompleteAnimation;

        void OnCompleteAnimation(TrackEntry trackEntry)
        {
            _spineAnimationState.Complete -= OnCompleteAnimation;
            
            callback?.Invoke();
        }
    }

    private void SetTailVisibilityState(bool state)
    {
        _tailSlot.A = state ? 1 : 0;
    }
}