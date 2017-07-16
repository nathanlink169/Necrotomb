using UnityEngine;
using GameFramework;

public class Headbob : BaseBehaviour 
{
    #region MonoBehaviour
    private void Start()
    {
        m_Midpoint = transform.localPosition.y;
    }
 
	void Update () 
	{
        float waveSlice = 0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 localPosition = transform.localPosition;

        if(Mathf.Abs(horizontal) == 0f && Mathf.Abs(vertical) == 0f)
        {
            m_Timer = 0f;
        }
        else
        {
            waveSlice = Mathf.Sin(m_Timer);
            m_Timer = Mathf.Repeat(m_Timer + BOBBING_SPEED, Mathf.PI * 2);
        }

        if(waveSlice != 0f)
        {
            float translationChanges = waveSlice * BOBBING_AMOUNT;
            float totalAxes = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            translationChanges *= totalAxes;
            localPosition.y = m_Midpoint + translationChanges;
        }
        else
        {
            localPosition.y = m_Midpoint;
        }

        transform.localPosition = localPosition;
	}
    #endregion

    #region Private Variables
    private float m_Timer = 0f;
    private float m_Midpoint;
    #endregion

    #region Private Constants
    private const float BOBBING_SPEED = 0.18f;
    private const float BOBBING_AMOUNT = 0.05f;
    #endregion
}
