using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;

public class PlayerRotate : MonoBehaviour
{
    [SerializeField] private Transform _cameraHolder;
    [SerializeField] private float sensitivity;
    [SerializeField] private float _rotationLimitUp = -35;
    [SerializeField] private float _rotationLimitDown = 45;

    protected Vector3 _lookDir;

    protected float _holderRotation;

    private Quaternion initialRotation;
    private Quaternion currentRotationHorizontal;
    private Quaternion nextRotation;

    public bool _rotateToAngles;
    public Vector3 _anglesToRotate;

    [SerializeField] private float tiltAmount;
    [SerializeField] private float tiltSpeed;


    private void Start()
    {
        initialRotation = _cameraHolder.localRotation;
        currentRotationHorizontal = transform.localRotation;
        tiltAmount = 2f;
        tiltSpeed = 10f;
        _rotateToAngles = false;
    }

    public virtual void Rotate(Vector2 input)
    {
        _lookDir.x = input.x * sensitivity * Time.deltaTime;
        _lookDir.y = input.y * sensitivity * Time.deltaTime;

        _holderRotation -= _lookDir.y;
        _holderRotation = Mathf.Clamp(_holderRotation, _rotationLimitDown, _rotationLimitUp);

        rotateVertical();
        rotateHorizontal();
        
        initialRotation = _cameraHolder.localRotation;
        //if (!_rotateToAngles) currentRotationHorizontal = transform.localRotation;
        currentRotationHorizontal = transform.localRotation;
        PlayerProfile.UpdateRotation(gameObject.transform.localRotation.eulerAngles);

    }

    protected virtual void rotateVertical()
    {
        Vector3 vec = _cameraHolder.localRotation.eulerAngles;
        _cameraHolder.localRotation = Quaternion.Euler(_holderRotation, 0f, vec.z);
    }


    protected virtual void rotateHorizontal()
    {
        transform.Rotate(Vector3.up* _lookDir.x);
    }

    public void CameraTilt(Vector2 input)
    {
        float rotationZ = input.x;
        Quaternion rotation = Quaternion.Euler(initialRotation.eulerAngles.x, initialRotation.eulerAngles.y, rotationZ * -tiltAmount);
        _cameraHolder.localRotation = Quaternion.Slerp(initialRotation, rotation, tiltSpeed * Time.deltaTime);

    }

    public void RotateCameraGameEvent(float x, float y, float z, float seconds)
    {
        StartCoroutine(WaitAndRotate(x, y, z, seconds));
    }

    public IEnumerator WaitAndRotate(float x, float y, float z, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _rotateToAngles = true;
        _anglesToRotate = new Vector3(x, y, z);
    }

    public void RotateToAngles()
    {
        Quaternion rotation = Quaternion.Euler(transform.localRotation.x, _anglesToRotate.y, transform.localRotation.z);

        if (_rotateToAngles)
        {
            float delta = 0.01f;
            if (Mathf.Abs(transform.localRotation.y - rotation.y) > delta)
            {
                transform.localRotation = Quaternion.Slerp(currentRotationHorizontal, rotation, 5f*Time.deltaTime);
            }
            else _rotateToAngles = false;
        }
    }

}
