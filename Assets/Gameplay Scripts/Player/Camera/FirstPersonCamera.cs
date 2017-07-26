/******************************************** File Header ********************************************\
 *                                                                                                   *
 * FileName:        FirstPersonCamera                                                                *
 * FileExtension:   .cs                                                                              *
 * Author:          Nathan Pringle                                                                   *
 * Date:            September 16th, 2016                                                             *
 *                                                                                                   *
 * This file should be attached to a Camera Object in order to replicate a first person point of     *
 * view. For basic shapes (such as capsules), the camera should be a child of the capsule, and its   *
 * transform should be at (0, 0, 0)                                                                  *
 *                                                                                                   *
 * THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, EITHER EXPRESSED OR   *
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS    *
 * FOR A PARTICULAR PURPOSE.                                                                         *
 *                                                                                                   *
 * V 1.0 - Created File (Nathan Pringle) - September 16th, 2016                                      *
 * V 1.1 - Added public values for x/y axis sensitivity (Jon Roffey) - October 10th, 2016            *
 *       - Added cursor lock functionality from ThirdPersonCamera (Jon Roffey) - October 10th, 2016  *
 *       - Updated inputs to work with the new InputManager (Jon Roffey) - October 10th, 2016        *
\*****************************************************************************************************/

using GameFramework;
using UnityEngine;

public class FirstPersonCamera : BaseBehaviour
{
    // 
    // Public Fields
    // 
    [Tooltip("Check this if you would like to lock the mouse to the middle of the screen and disable it. Note: To undo this in the editor, press ESCAPE.")]
    public bool LockMouse = true;

    [Tooltip("Don't let the mouse look too high or too low, otherwise you get weird results. Keep this number low, but not too low.")]
    public float LockVerticalRotationValue = 0.5f;

    [Tooltip("The higher this number, the faster the mouse will move on the X axis.")]
    public float XMouseSensitivity = 1f;

    [Tooltip("The higher this number, the faster the mouse will move on the Y axis.")]
    public float YMouseSensitivity = 1f;

    public bool MoveParent = true;

    [Tooltip("Turn this on if the player should be able to look around while their player is stunned.")]
    public bool AllowRotationIfStunned = false;
    
    // Gets a point along the forward vector of the camera where the first raycast collision is.
    public Vector3 RaycastPoint
    {
        get { return _RaycastPoint; }
    }
    protected Vector3 _RaycastPoint;

    [Tooltip("Use this to control how far the camera raycast will travel")]
    public float RaycastPointHitRange = 500.0f;

    [Tooltip("Setting a minimum value will prevent odd gun rotation issues")]
    public float RaycastPointHitMinCutoff = 5.0f;

    //
    // Private Fields
    //
    private Vector3 m_Rotation = Vector3.zero;
    private int m_ID;
    private int m_RaycastIgnoreLayer;

    void Start()
    {
        // The camera raycast will ignore the player layer.
        _RaycastPoint = transform.position + transform.forward * RaycastPointHitRange;
        m_RaycastIgnoreLayer = LayerMask.NameToLayer("Player");
    }

    void Update()
    {
        if (IsPaused)
            return;

        if (Input.GetKeyDown(KeyCode.Escape))
            GCore.Instance.SetCursorLockmode(false);
        else
            GCore.Instance.SetCursorLockmode(LockMouse);

        // Takes the inputs and moves the mouse
        HandleRotation();

        // Makes sure we aren't looking too high or too low
        LockRotation();

        // Finally, sets the actual angle of the camera
        if (MoveParent && transform.parent != null)
        {
            Vector3 rotation = m_Rotation;
            rotation.x = 0;
            transform.parent.transform.eulerAngles = rotation;
            transform.eulerAngles = m_Rotation;
        }
        else
            transform.eulerAngles = m_Rotation;

        UpdateRaycastAiming();
    }

    // Rotates the equipped gun to face the camera's raycast hit point.
    void UpdateRaycastAiming()
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, RaycastPointHitRange, m_RaycastIgnoreLayer))
        {
            _RaycastPoint = hitInfo.point;
        }
        else
        {
            _RaycastPoint = transform.position + transform.forward * RaycastPointHitRange;
        }
        float RaycastDistance = Vector3.Distance(transform.position, _RaycastPoint);
        if (RaycastDistance < RaycastPointHitMinCutoff)
        {
            _RaycastPoint = transform.position + transform.forward * RaycastPointHitMinCutoff;
        }
        
        GameObject weapon = GPlayerManager.Instance.PlayerWeaponManager.GetCurrentWeaponGameObject();
        if (weapon != null)
        {
            weapon.transform.LookAt(RaycastPoint);

            // Correct angle
            Vector3 cameraAngle = transform.rotation.eulerAngles;

            // Z is forward so need to rotate the weapon
            weapon.transform.Rotate(new Vector3(0.0f, 90.0f, 0.0f));

            // Need to clamp X and Z rotation so that the gun doesn't rotate too far around
            Vector3 weaponAngle = weapon.transform.localEulerAngles;
            weaponAngle.x = Mathf.Clamp(weaponAngle.x, -2.0f, 2.0f);
            weapon.transform.localEulerAngles = weaponAngle;
        }
    }

    void HandleRotation()
    {
        // Get the actual values of the mouse movement
        float x = 0.0f;
        float y = 0.0f;

        bool canRotate = true;

        if (canRotate)
        {
            x = Input.GetAxis("Mouse X");
            y = Input.GetAxis("Mouse Y");
        }

        // Gets the cameras current rotation. m_Rotation should still be set to the cameras rotation, so this is just a safety thing
        m_Rotation = transform.rotation.eulerAngles;

        // If you're confused as to why we're changing the x axis with the y input and vise versa, take a look at the editor.
        // Take a look at where the three axis are pointed, then press "E" to swap to rotation version. Take a look at the colours of each circle.
        m_Rotation.x -= y * XMouseSensitivity;
        m_Rotation.y += x * YMouseSensitivity;
        m_Rotation.z = 0;
    }

    void LockRotation()
    {
        // Make sure we don't look too low.
        if (m_Rotation.x > 90 - LockVerticalRotationValue && m_Rotation.x < 180)
            m_Rotation.x = 90 - LockVerticalRotationValue;

        // Makes sure we don't look too high.
        if (m_Rotation.x < 270 + LockVerticalRotationValue && m_Rotation.x > 180)
            m_Rotation.x = 270 + LockVerticalRotationValue;
    }
}
