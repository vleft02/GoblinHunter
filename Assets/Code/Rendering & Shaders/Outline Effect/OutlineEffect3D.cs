using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineEffect3D : MonoBehaviour
{

    public bool _isSelected = false;
    private Material material;

    Color _colorTransparent = new Color(0, 0, 0, 0);

    void Start()
    {
        MeshRenderer renderer = GetComponentInChildren<MeshRenderer>();

        material = renderer.materials[1];


    }


    void Update()
    {

        if (!_isSelected)
        {
            // Make It Transparent
            //material.SetColor("_Color", new Color(255, 255, 255, 0));
            material.SetColor("_OutlineColor", _colorTransparent);
        }

        if (_isSelected)
        {
            material.SetColor("_OutlineColor", Color.white);
        }

    }



}
