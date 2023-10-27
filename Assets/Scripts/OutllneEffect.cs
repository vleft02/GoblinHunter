using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class OutlineEffect : MonoBehaviour
{

    public bool _isSelected = false;
    private Material material;

    Color _colorTransparent = new Color(0, 0, 0, 0);

    void Start()
    {
        material = GetComponent<SpriteRenderer>().material;

    }


    void Update()
    {
        
        if (!_isSelected)
        {
            // Make It Transparent
            //material.SetColor("_Color", new Color(255, 255, 255, 0));
            material.SetColor("_Color", _colorTransparent);
        }

        if (_isSelected)
        {
            material.SetColor("_Color", Color.white);
        }

    }

}
