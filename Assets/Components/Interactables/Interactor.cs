using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactor : MonoBehaviour
{
    [Header("Debug Support")]
    [SerializeField] private Interactable _currentInteractable;


    [Header("Setuo")]
    [SerializeField] private float _raiousRange = 0.5f;
    public float RadiusRange { get => _raiousRange; }

    private IEnumerator _checkBreakDistanceCorroutine;
    
    public void OnInteract(InputValue value)
    {
        if(value.isPressed)
        {
            TryStartInteract();
        }
        else
        {
            TryEndInteract();
        }
    }

    private void TryStartInteract()
    {
        Vector3 position = transform.position;
        int inverseMask = ~(gameObject.layer);
        List<Collider> colliders = Physics.OverlapSphere(position, _raiousRange, inverseMask, QueryTriggerInteraction.Collide).ToList();

        colliders.Sort((a,b) =>
        {

            float dA = Vector3.Distance(a.ClosestPoint(position),position);
            float dB = Vector3.Distance(b.ClosestPoint(position), position);
            return dA.CompareTo(dB);
        });

        foreach (Collider collider in colliders) 
        {
            if (collider.gameObject.TryGetComponent(out Interactable interactable))
            {
                _currentInteractable = interactable;
                _currentInteractable.StartInteract(this.gameObject);

                if(interactable.breakWithDistance)
                {
                    _checkBreakDistanceCorroutine = CheckBreakDistanceCorroutine(collider);
                    StartCoroutine(_checkBreakDistanceCorroutine);
                }

                return; //Early Exit
            }
        }
    }
    private void TryEndInteract()
    {
        if(_currentInteractable != null)
        {
            _currentInteractable.EndInteract(this.gameObject);
            _currentInteractable = null;
        }
        if(_checkBreakDistanceCorroutine != null)
        {
            StopCoroutine(_checkBreakDistanceCorroutine);
            _checkBreakDistanceCorroutine = null;
        }
    }

    
    private IEnumerator CheckBreakDistanceCorroutine(Collider targetCollider)
    {
        while (Vector3.Distance(targetCollider.ClosestPoint(transform.position),transform.position) <= _raiousRange) 
        {
            yield return null;
        
        }
        TryEndInteract();
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, _raiousRange); 


    }
#endif
   







}
