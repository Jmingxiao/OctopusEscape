using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class CustomButtonScale : CustomButtonBase
{
    #region Variables

    private const float OriginalScale = 1.0f;
    [SerializeField] private float toScale;
    [SerializeField] private float duration;

    #endregion
    
    public override void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("OnPointerEnter");
        base.OnPointerEnter(eventData);
        transform.DOScale(new Vector3(1.3f,1.3f,1.3f), duration);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        base.OnPointerExit(eventData);
       transform.DOScale(OriginalScale, duration)
            .SetEase(Ease.InOutSine);
    }
}