using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragZone : MonoBehaviour, IDropHandler
{
    private Image _thisImage;
    [SerializeField] private List<ObjectType> _validType;

    void Start()
    {
        _thisImage = GetComponent<Image>();
    }
    public void OnDrop(PointerEventData eventData)
    {
         DraggNewSystem GetTag = eventData.pointerDrag.GetComponent<DraggNewSystem>();

        if (GetTag._type == _validType) 
        {

            GetTag.oldPosition = _thisImage.rectTransform.localPosition;   
        
        }
    }
}
