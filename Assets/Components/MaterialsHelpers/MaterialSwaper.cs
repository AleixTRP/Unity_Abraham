using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class MaterialSwaper : MonoBehaviour
{
    private MeshRenderer m_renderer;

    [SerializeField] private Material _materialToSwap;
    private Material _initialMaterial;

    private void Start()
    {
        m_renderer = GetComponent<MeshRenderer>();
        _initialMaterial = m_renderer.material;
    }
    public void SetSecondaryMaterial()
    {

        if(_materialToSwap != null)
        {
            m_renderer.material = _materialToSwap;
        }
    }
    public void SetPrimaryMaterial()
    {
        m_renderer.material = _initialMaterial;
    }
}
