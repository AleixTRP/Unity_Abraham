using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static Grabable;

public class DropPlace : MonoBehaviour
{


    [System.Flags]
    public enum CheckMode
    {
        CheckObject = 1,
        CheckGrabableType = 2
    }


    [Header("Setup")]
    [SerializeField] private CheckMode _checkMode;
    [SerializeField] private List<Grabable> _validGrabables = new();
    [SerializeField] private GrabableType _validGrabableType = new();

    [Header("Events")]
    public UnityEvent<GameObject> OnObjectDropped;
    public UnityEvent<GameObject> OnObjectGrabed;


    public bool IsValid(Grabable grabable)
    {
        bool isValid = true;

        if(_checkMode.HasFlag(CheckMode.CheckObject))
        {
            if(_validGrabables.Count != 0 && !_validGrabables.Contains(grabable))
            {
                isValid = false;
            }

        }
        if(_checkMode.HasFlag(CheckMode.CheckGrabableType)) 
        { 
            if(_validGrabableType.HasFlag(grabable.grabableType))
            {
                isValid = false;
            }
        
        }

        return isValid;
    }

    public void OnDrop(Grabable grabable)
    {
        OnObjectDropped.Invoke(grabable.gameObject);
        grabable.OnStartGrab.AddListener(OnGrab);
    }

    private void OnGrab(GameObject grabableObject,GameObject parent) 
    { 
        if(grabableObject.TryGetComponent(out Grabable grabable)) 
        {
            grabable.OnStartGrab.RemoveListener(OnGrab);
        }
        OnObjectGrabed.Invoke(grabable.gameObject);
    
    }
}
