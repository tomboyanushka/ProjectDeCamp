using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{

    public void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
        m_brakingInput = Input.GetKey(KeyCode.Space);
        
    }

    private void Steer()
    {
        m_steeringAngle = maxSteerAngle * m_horizontalInput;
        frontDriverW.steerAngle = m_steeringAngle;
        frontPassengerW.steerAngle = m_steeringAngle;
    }

    private void Accelerate()
    {
        frontDriverW.motorTorque = m_verticalInput * motorForce;
        frontPassengerW.motorTorque = m_verticalInput * motorForce;
    }

    private void UpdateWheelPoses()
    {
        UpdateWheelPose(frontDriverW, frontDriverT);
        UpdateWheelPose(frontPassengerW, frontPassengerT);
        UpdateWheelPose(rearDriverW, rearDriverT);
        UpdateWheelPose(rearPassengerW, rearPassengerT);
    }

    private void UpdateWheelPose(WheelCollider _collider, Transform _transform)
    {
        Vector3 _pos = _transform.position;
        Quaternion _quat = _transform.rotation;

        _collider.GetWorldPose(out _pos, out _quat);

        _transform.position = _pos;
        _transform.rotation = _quat;
    }

    private void Brake()
    {
        if (m_brakingInput)
        {


            frontDriverW.motorTorque = 0;
            frontPassengerW.motorTorque = 0;
            //brakeForce += brakeForce * Time.deltaTime;
            frontDriverW.brakeTorque = brakeForce; //* motorForce;
            frontPassengerW.brakeTorque = brakeForce; //* motorForce;
            rearDriverW.brakeTorque = brakeForce; //* motorForce;
            rearPassengerW.brakeTorque = brakeForce; //* motorForce;
        }
        else
        {
            brakeForce = 1000;
            frontDriverW.brakeTorque = 0;
            frontPassengerW.brakeTorque = 0;
            rearDriverW.brakeTorque = 0;
            rearPassengerW.brakeTorque = 0;
        }

    }

    private void FixedUpdate()
    {
        GetInput();
        Steer();
        Accelerate();
        UpdateWheelPoses();
        Brake();
    }

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_steeringAngle;
    private bool m_brakingInput = false;

    public WheelCollider frontDriverW, frontPassengerW;  //W = Wheel 
    public WheelCollider rearDriverW, rearPassengerW;    //T = Transform
    public Transform frontDriverT, frontPassengerT;
    public Transform rearDriverT, rearPassengerT;
    public float maxSteerAngle = 30;
    public float motorForce = 50;
    public float brakeForce = 1000;
}
