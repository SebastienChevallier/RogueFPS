using UnityEngine;
using UnityEngine.UI;

public class HUD_InGame : MonoBehaviour
{
    public Image cursor;
    public Image grip1, grip2;
    public Material edgesMaterial;

    public Color actualColor = Color.white;

    public void UpdateColorCursor(Color valueColor)
    {
        cursor.color = valueColor;
        actualColor = valueColor;
        if (edgesMaterial != null)
        {
            edgesMaterial.SetColor("_Color", valueColor);
        }
    }

    public void CanTouch(bool canTouch)
    {
        if (canTouch)
        {
            cursor.color = Color.red;
        }
        else
        {
            cursor.color = actualColor;
        }
    }

    public void WeaponIsActive(bool isActive)
    {
        grip1.gameObject.SetActive(isActive);
        grip2.gameObject.SetActive(isActive);
    }

    public void UpdateWeaponStatus(int nb)
    {
        if(nb == 1)
        {
            grip1.gameObject.SetActive(true);
        }

        if(nb == 2)
        {
            grip2.gameObject.SetActive(true);
        }
    }
}
