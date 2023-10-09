using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float sensitivity;
    [SerializeField] private float _rotationLimitUp = -35;
    [SerializeField] private float _rotationLimitDown = 45;

    protected Vector3 _lookDir;

    protected float _holderRotation;


    public virtual void Rotate(Vector2 input)
    {
        _lookDir.x = input.x * sensitivity * Time.deltaTime;
        _lookDir.y = input.y * sensitivity * Time.deltaTime;

        _holderRotation -= _lookDir.y;
        _holderRotation = Mathf.Clamp(_holderRotation, _rotationLimitDown, _rotationLimitUp);

        rotateVertical();
        rotateHorizontal();


    }

    protected virtual void rotateVertical() => _cameraHolder.localRotation = Quaternion.Euler(_holderRotation, 0f, 0f);

    protected virtual void rotateHorizontal() => transform.Rotate(Vector3.up * _lookDir.x);
}
