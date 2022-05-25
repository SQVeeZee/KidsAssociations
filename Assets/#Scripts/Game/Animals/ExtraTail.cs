using Spine.Unity;
using UnityEngine;

public class ExtraTail : BoneFollower
{
    [Header("Tail")]
    [SerializeField] private GameObject _tail = null;
    [SerializeField] private SpriteRenderer _tailsSpriteRenderer = null;

    public void EnableExtraTail(Sprite tailSprite)
    {
        _tailsSpriteRenderer.sprite = tailSprite;
        
        SetBone(boneName);
   
        SetTailSpriteState(true);
    }

    public void DisableExtraTail()
    {
        _tailsSpriteRenderer.sprite = null;
        
        SetTailSpriteState(false);
    }

    private void SetTailSpriteState(bool state)
    {
        _tail.SetActive(state);
    }
}
