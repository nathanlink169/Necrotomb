using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipMainMenu : BaseBehaviour
{
    public CameraShake CameraObject;
    public AudioSource RattleSound;

    private readonly Vector2 TIME_UNTIL_SHAKE = new Vector2(10f, 60f);
    private const float TIME_UNTIL_ROTATION_CHANGE = 10f;
    private const float CAMERA_SHAKE_INTENSITY = 1f;
    private Quaternion m_TargetRotation;
    private bool m_Reset;

    void Start()
    {
        StartCoroutine(ChangeRotation());
        StartCoroutine(Rotate());
        StartCoroutine(Shake());
    }

    IEnumerator Shake()
    {
        while(true)
        {
            yield return YieldFactory.GetWaitForSeconds(Random.Range(TIME_UNTIL_SHAKE.x, TIME_UNTIL_SHAKE.y));

            if(RattleSound != null && RattleSound.clip != null)
            {
                CameraObject.ShakeCamera(CAMERA_SHAKE_INTENSITY, RattleSound.clip.length);
            }
            else
            {
                CameraObject.ShakeCamera(CAMERA_SHAKE_INTENSITY, 0.25f);
            }
        }
    }

    IEnumerator ChangeRotation()
    {
        while(true)
        {
            m_TargetRotation = Random.rotation;
            m_Reset = true;
            yield return YieldFactory.GetWaitForSeconds(TIME_UNTIL_ROTATION_CHANGE);
        }
    }

    IEnumerator Rotate()
    {
        Quaternion startingRotation = transform.rotation;
        float timeSinceChange = 0f;
        float equationValue = 0f;

        while (true)
        {
            if (m_Reset == true)
            {
                m_Reset = false;
                startingRotation = transform.rotation;
                timeSinceChange = 0f;
            }

            timeSinceChange += Time.deltaTime;
            equationValue = timeSinceChange / TIME_UNTIL_ROTATION_CHANGE;

            float lerpValue = (1f) / (1 + Mathf.Pow(MathUtils.e, -10 * (equationValue - 0.5f)));
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Lerp(startingRotation, m_TargetRotation, lerpValue), Time.deltaTime * 5f);

            yield return null;
        }
    }
}