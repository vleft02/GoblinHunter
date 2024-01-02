using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    private bool _fadeEffect = false;

    public Color FadeColor = Color.black;
    public bool StartFadeOut = false;
    public AnimationCurve Curve = new AnimationCurve(new Keyframe(0, 1), new Keyframe(0.5f, 0.5f, -1.5f, -1.5f), new Keyframe(1, 0));
    public float _speedScale = 1f;

    [SerializeField] private Image image;
    private Texture2D _texture;
    private float _alpha = 0f;
    private int _direction = 0; // 1 -> FadeIn, -1 -> FadeOut
    private float _time = 0f;

    void Start()
    {
        //_texture = new Texture2D(1, 1);
        //_texture.SetPixel(0, 0, new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha));
        //_texture.Apply();
    }

    public void StartFadeInEffect()
    {
        _fadeEffect = true;
        _alpha = 1f;
        image.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha);
    }

    public void StartFadeOutEffect()
    {
        _fadeEffect = true;
        _alpha = 0f;
        image.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha);
    }

    void Update()
    {
        if (_direction == 0 & _fadeEffect)
        {
            if (_alpha >= 1f)
            {
                _alpha = 1f;
                _time = 0f;
                _direction = 1;
            }
            else
            {
                _alpha = 0f;
                _time = 1f;
                _direction = -1;
            }
        }

        if (_direction != 0)
        {
            _time += _direction * Time.deltaTime * _speedScale;
            _alpha = Curve.Evaluate(_time);
            //_texture.SetPixel(0, 0, new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha));
            //_texture.Apply();
            image.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha);
            if (_alpha <= 0f || _alpha >= 1f)
            {
                _direction = 0;
                _fadeEffect = false;
            }

        }
    }

    /*
    public void OnGUI()
    {
        //if (_alpha > 0f) GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _texture);        

        if (_direction != 0)
        {
            _time += _direction * Time.deltaTime * _speedScale;
            _alpha = Curve.Evaluate(_time);
            //_texture.SetPixel(0, 0, new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha));
            //_texture.Apply();
            image.color = new Color(FadeColor.r, FadeColor.g, FadeColor.b, _alpha);
            if (_alpha <= 0f || _alpha >= 1f)
            {
                _direction = 0;
                _fadeEffect = false;
            }

        }
    }
    */









}
