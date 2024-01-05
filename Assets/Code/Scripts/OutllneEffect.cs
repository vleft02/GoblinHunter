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
            material.SetColor("_Color", _colorTransparent);
        }

        if (_isSelected)
        {
            material.SetColor("_Color", Color.white);
        }

    }

}
