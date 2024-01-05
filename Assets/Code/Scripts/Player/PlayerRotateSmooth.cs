using UnityEngine;

public class PlayerRotateSmooth : PlayerRotate
{
    [Header("SmoothCamera")]
    [SerializeField] private float _smoothTime;
    [SerializeField] private Transform _horizontalRotationHelper;

    private float _oldHolderRotation;
    private float _horiAngularVelocity;
    private float _vertAngularVelocity;

    private void Start() => _horizontalRotationHelper.localRotation = transform.localRotation;

    public override void Rotate(Vector2 input)
    {
        _oldHolderRotation = _holderRotation;
        base.Rotate(input);
    }

    protected override void rotateHorizontal()
    {
        _horizontalRotationHelper.Rotate(Vector3.up * _lookDir.x, Space.Self);
        transform.localRotation = Quaternion.Euler(0f, Mathf.SmoothDampAngle(
                                                                        transform.localEulerAngles.y,
                                                                        _horizontalRotationHelper.localEulerAngles.y,
                                                                        ref _horiAngularVelocity,
                                                                        _smoothTime), 0f);
    }

    protected override void rotateVertical()
    {
        _holderRotation = Mathf.SmoothDampAngle(_oldHolderRotation, _holderRotation, ref _vertAngularVelocity, _smoothTime);
        base.rotateVertical();
    }
}
