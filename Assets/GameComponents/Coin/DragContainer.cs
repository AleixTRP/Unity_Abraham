using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragContainer : MonoBehaviour
{
    [NonSerialized] public RectTransform Rect;
    void Start()
    {
        Rect = GetComponent<RectTransform>();
    }

  
}
