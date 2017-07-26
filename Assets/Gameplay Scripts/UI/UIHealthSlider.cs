using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using GameFramework;

public class UIHealthSlider : BaseBehaviour
{
    public Color LowColour = Color.red;
    public Color FullColour = Color.green;
    public Text UIText;
    public Image InnerRadialImage;
    public Image OuterRadialImage;

    private void Start()
    {
        GEventManager.StartListening(GEventManager.ON_PLAYER_TAKE_DAMAGE, updateDisplay);
        GEventManager.StartListening(GEventManager.ON_PLAYER_HEAL_DAMAGE, updateDisplay);

        updateDisplay();
    }

    private void updateDisplay()
    {
        GPlayerHealth playerHealth = GPlayerHealth.Instance;
        int sections = playerHealth.Upgrades;

        if (sections > 4)
        {
            if (playerHealth.Current > 100)
            {
                float totalFillAmount = (playerHealth.Current - 100) / 200;
                float totalColourAmount = (playerHealth.Current - 100) / (playerHealth.Maximum - 100);
                OuterRadialImage.fillAmount = totalFillAmount;
                OuterRadialImage.color = Color.Lerp(LowColour, FullColour, totalColourAmount);
                InnerRadialImage.fillAmount = 1.0f;
                InnerRadialImage.color = FullColour;

            }
            else
            {
                float totalAmount = playerHealth.Current / (playerHealth.Maximum - 25 * (sections - 4));
                InnerRadialImage.fillAmount = totalAmount;
                InnerRadialImage.color = Color.Lerp(LowColour, FullColour, totalAmount);
                OuterRadialImage.fillAmount = 0.0f;
            }
        }
        else
        {
            float totalAmount = playerHealth.Current / playerHealth.Maximum;
            InnerRadialImage.fillAmount = totalAmount;
            InnerRadialImage.color = Color.Lerp(LowColour, FullColour, totalAmount);
            OuterRadialImage.fillAmount = 0.0f;
        }

        UIText.text = playerHealth.Current.ToInt().ToString();
    }
}
