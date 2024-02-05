using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DraggNewSystem : MonoBehaviour, IDragHandler, IEndDragHandler, IBeginDragHandler
{
    private Image _image;
    public Vector3 oldPosition;

    [SerializeField] public ObjectType _type;

    void Start()
    {
        _image = GetComponent<Image>();
        oldPosition = _image.rectTransform.localPosition;
    }


    public void ResetPosition()
    {
        _image.rectTransform.localPosition = oldPosition;  
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        ResetPosition();
        _image.raycastTarget = true;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _image.raycastTarget = false;
    }
}
