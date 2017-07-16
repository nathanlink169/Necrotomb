using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameFramework;

[RequireComponent(typeof(Rigidbody))]
public class FirstPersonMovement : BaseBehaviour
{
    #region Public Variables
    public Camera Camera;
    public float WalkingSpeed = 5.0f;
    public float SprintingSpeed = 8.0f;
    public float JumpHeight = 10.0f;
    #endregion

    #region MonoBehaviour
    private void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        m_fDistanceToGround = GetComponent<Collider>().bounds.extents.y;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && m_bIsGrounded)
        {
            m_bIsJumping = true;
        }
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0.0f)
        {
            return;
        }
        getInput(out m_vInput, out m_bIsSprinting);
        translateDirectionVector(out m_vMovementVector, m_vInput);

        m_Rigidbody.MovePosition(transform.position + m_vMovementVector * Mathf.Lerp(WalkingSpeed, SprintingSpeed, m_bIsSprinting ? m_vInput.y : 0f) * Time.fixedDeltaTime);

        if (m_bIsJumping)
        {
            m_Rigidbody.AddForce(Vector3.up * JumpHeight, ForceMode.VelocityChange);
            m_bIsJumping = false;
        }

        m_bIsGrounded = Physics.Raycast(transform.position, -Vector3.up, m_fDistanceToGround + 0.1f);
    }
    #endregion

    #region Public
    public bool GetIsGrounded()
    {
        return m_bIsGrounded;
    }
    #endregion

    #region Private Functions
    private void translateDirectionVector(out Vector3 out_vMovementVector, Vector2 in_vPlayerInput)
    {
        out_vMovementVector.x = in_vPlayerInput.x;
        out_vMovementVector.y = 0.0f;
        out_vMovementVector.z = in_vPlayerInput.y;
        out_vMovementVector = Camera.transform.TransformDirection(out_vMovementVector);
        out_vMovementVector.y = 0.0f;
        out_vMovementVector.Normalize();
    }

    private void getInput(out Vector2 out_vPlayerInput, out bool out_bIsSprinting)
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        out_vPlayerInput = new Vector2(horizontal, vertical);

        if (out_vPlayerInput.sqrMagnitude > 1)
        {
            out_vPlayerInput.Normalize();
        }

        out_bIsSprinting = Input.GetKey(KeyCode.LeftShift);
    }
    #endregion

    #region Private Values
    private Rigidbody m_Rigidbody;
    private bool m_bIsJumping = false;
    private bool m_bIsGrounded = false;

    private Vector2 m_vInput = Vector2.zero;
    private bool m_bIsSprinting = false;
    private Vector3 m_vMovementVector = Vector3.zero;
    private float m_fDistanceToGround;
    #endregion
}