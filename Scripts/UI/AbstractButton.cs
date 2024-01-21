using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class AbstractButton : MonoBehaviour, IPointerUpHandler, IPointerEnterHandler, IPointerDownHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField, Range(0, 1)] protected float pointHighlight;
    [SerializeField, Range(0, 1)] protected float clickHighlight;

    private Image image;
    private RawImage rawImage;
    public virtual void Start()
    {
        image = GetComponent<Image>();
        rawImage = GetComponent<RawImage>();
        MatchImage(0);
    }
    private void MatchImage(float highlight)
    {
        if (image) image.color = Color.white * (1 - highlight) + Color.black * highlight;
        else rawImage.color = Color.white * (1 - highlight) + Color.black * highlight;
    }
    public virtual void OnPointerDown(PointerEventData eventData) => MatchImage(clickHighlight);
    public virtual void OnPointerUp(PointerEventData eventData) => MatchImage(0);
    public virtual void OnPointerEnter(PointerEventData eventData) => MatchImage(pointHighlight);
    public virtual void OnPointerExit(PointerEventData eventData) => MatchImage(0);
    public virtual void OnPointerClick(PointerEventData eventData) { }
}
