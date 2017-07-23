using GameFramework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckForInteractable : BaseBehaviour
{
    public const string LAYER_NAME = "Interactable";
    public Text HelperText = null;
    public float DistanceToCheck = 5.0f;
    public float TextFadeSpeed = 0.5f;

    private void Start()
    {
        m_cHelperColour = HelperText.color;
    }

    void FixedUpdate()
    {
        RaycastHit rHit;
        bool bIsHovering = MathUtils.RaycastFirstObjectHit(out rHit, vWorldPosition, vForward, DistanceToCheck, null, LayerMask.GetMask(LAYER_NAME));


        if (bIsHovering)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                IInteractable interactable = rHit.transform.GetComponent<IInteractable>();
                if (interactable != null)
                {
                    if (interactable.IsInteractable())
                        interactable.OnInteract();
                }
            }

            if (!m_bPreviouslyHovering)
            {
                StopAllCoroutines();
                StartCoroutine(fadeHelperTextIn());
            }
        }
        else if (m_bPreviouslyHovering)
        {
            StopAllCoroutines();
            StartCoroutine(fadeHelperTextOut());
        }


        m_bPreviouslyHovering = bIsHovering;
    }

    private IEnumerator fadeHelperTextIn()
    {
        float fTime = m_cHelperColour.a;
        while (m_cHelperColour.a < 1.0f)
        {
            fTime += Time.fixedDeltaTime / TextFadeSpeed;
            m_cHelperColour.a = Mathf.Lerp(0.0f, 1.0f, fTime);
            HelperText.color = m_cHelperColour;
            yield return YieldFactory.GetWaitForFixedUpdate();
        }
    }

    private IEnumerator fadeHelperTextOut()
    {
        float fTime = 1 - m_cHelperColour.a;
        while (m_cHelperColour.a > 0.0f)
        {
            fTime += Time.fixedDeltaTime / TextFadeSpeed;
            m_cHelperColour.a = Mathf.Lerp(1.0f, 0.0f, fTime);
            HelperText.color = m_cHelperColour;
            yield return YieldFactory.GetWaitForFixedUpdate();
        }
    }

    private Color m_cHelperColour;
    private bool m_bPreviouslyHovering = false;
}
