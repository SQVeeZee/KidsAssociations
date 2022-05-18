using System;
using Spine;
using Spine.Unity;
using Spine.Unity.AttachmentTools;
using UnityEngine;

public class AnimalController : MonoBehaviour
{
    
    [SerializeField] private SkeletonAnimation _skeletonAnimation = null;
    [SerializeField] private SkeletonRenderer _skeletonRenderer = null;

    [Header("Animations")]
    [SpineAnimation] [SerializeField]
    private string _noAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _idleAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _sadAnimationName = default;
    [SpineAnimation] [SerializeField]
    private string _happyAnimationName = default;

    public string SlotTailName { get; set; }
    
    private Spine.AnimationState _spineAnimationState;
    private Skeleton _skeleton;
    private Atlas _atlas;
    private Slot _tailSlot = null;
    private Attachment _originalAttachment = null;
    private AtlasRegion _region = null;
    
    private SpineAtlasAsset _atlasAsset;

    public void Initialize(EAnimalType animalType)
    {
        // SlotTailName = AnimalsContainer.Instance.GetTailByAnimalType(animalType);

        SlotTailName = "Tail line";
        
        SetData();
        
        SetTailVisibilityState(false);
        
        PlayAnimation(EAnimalAnimationType.IDLE);
    }

    private void SetData()
    {
        _spineAnimationState = _skeletonAnimation.AnimationState;
        _skeleton = _skeletonAnimation.Skeleton;
        
        _tailSlot = _skeleton.FindSlot(SlotTailName);
        _originalAttachment = _tailSlot.Attachment;
    }

    public void SetNewTail(EAnimalType animalType, Action callback = null)
    {
        var spineAtlasAsset = AnimalsContainer.Instance.GetAtlasByAnimalType(animalType);
        var slotTailName = AnimalsContainer.Instance.GetTailByAnimalType(animalType);

        _atlasAsset = spineAtlasAsset;
        
        _atlas = _atlasAsset.GetAtlas();
        _region = _atlas.FindRegion(slotTailName);
        
        ApplyNewTail();
        SetTailVisibilityState(true);

        PlayAnimation(EAnimalAnimationType.NO, delegate
        {
            OnIncorrectTailAnimationCompleted(); callback?.Invoke(); 
        });
    }

    public void SetRightTail(Action callback)
    {
        _skeleton.SetToSetupPose();
        
        PlayAnimation(EAnimalAnimationType.HAPPY, callback);
    }

    public void DoAnimalSad()
    {
        SetTailVisibilityState(true);

        PlayAnimation(EAnimalAnimationType.SAD);
    }

    private void ApplyNewTail () {
        if (!this.enabled) return;

        if (_atlas == null) return;
        float scale = _skeletonRenderer.skeletonDataAsset.scale;

            if (_region == null) {
                _tailSlot.Attachment = null;
            } else if (_originalAttachment != null) {
                _tailSlot.Attachment = _originalAttachment.GetRemappedClone(_region, true, true, scale);
            } else {
                var newRegionAttachment = _region.ToRegionAttachment(_region.name, scale);
                _tailSlot.Attachment = newRegionAttachment;
            }
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

    private void OnIncorrectTailAnimationCompleted()
    {
        SetTailVisibilityState(false);

        PlayAnimation(EAnimalAnimationType.IDLE);
    }

    private void SetTailVisibilityState(bool state)
    {
        _tailSlot.A = state ? 1 : 0;
    }
}