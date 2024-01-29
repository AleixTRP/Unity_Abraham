using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using static Grabable;

public class DropPlace : MonoBehaviour
{


    [System.Flags]
    public enum CheckMode
    {
        CheckObject = 1,
        CheckObjectType = 2
    }


    [Header("Setup")]
    [SerializeField] private CheckMode _checkMode;
    [SerializeField] private List<Grabable> _validGrabables = new();
    [SerializeField] private List<ObjectType> _validObjectsTypes = new();

    [Header("Events")]
    public UnityEvent<GameObject> OnObjectDropped;
    public UnityEvent<GameObject> OnObjectGrabed;


    public bool IsValid(Grabable grabable)
    {

        if(_checkMode.HasFlag(CheckMode.CheckObject))
        {
            if(_validGrabables.Contains(grabable))
            {

                return true;
            }

        }
        if(_checkMode.HasFlag(CheckMode.CheckObjectType)) 
        { 
            foreach(ObjectType objectType in grabable.objectTypes)
            { 
                if(_validObjectsTypes.Contains(objectType)) 
                {

                    return false;
                
                }  
                   
            
            }
        
        }    

        return false;
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
