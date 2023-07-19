using System;
using UnityEngine;

public class Car : MonoBehaviour
{
    [SerializeField]
    private float _speed = 1f;
    [SerializeField]
    private float _maxSteeringAngle = 30;
   
    [Header("Wheels")]
    [SerializeField]
    private WheelCollider _frontDriverWheel;
    [SerializeField]
    private WheelCollider _rearDriverWheel;
    [SerializeField]
    private WheelCollider _frontPassengerWheel;
    [SerializeField]
    private WheelCollider _rearPassengerWheel;

    [Header("Wheels transforms")]
    [SerializeField]
    private Transform _frontDriverWheelTransform;
    [SerializeField]
    private Transform _rearDriverWheelTransform;
    [SerializeField]
    private Transform _frontPassengerWheelTransform;
    [SerializeField]
    private Transform _rearPassengerWheelTransform;

    private Rigidbody _sphereRigidbody;
    private Vector3 _direction;
    private float _steeringAngle;
    private float _horizontalInput;
    private float _verticalInput;
  
    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
    }
    private void GetInput()
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        _verticalInput = Input.GetAxis("Vertical");
    }

    private void Steer()
    {
        _steeringAngle = _maxSteeringAngle * _horizontalInput;
        _frontDriverWheel.steerAngle = _steeringAngle;
        _frontPassengerWheel.steerAngle = _steeringAngle;
    }

    private void Accelerate()
    {
        _frontDriverWheel.motorTorque = _verticalInput * _speed;
        _frontPassengerWheel.motorTorque = _verticalInput * _speed;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(_frontPassengerWheel, _frontPassengerWheelTransform);
        UpdateWheelPose(_frontDriverWheel, _frontDriverWheelTransform);
        UpdateWheelPose(_rearPassengerWheel, _rearPassengerWheelTransform);
        UpdateWheelPose(_rearDriverWheel, _rearDriverWheelTransform);
    }

    private void UpdateWheelPose(WheelCollider collider, Transform _transform)
    {
        Vector3 pos = _transform.position;
        Quaternion quat = _transform.rotation;

        collider.GetWorldPose(out pos, out quat);

        _transform.position = pos;
        _transform.rotation = quat;
    }
}
