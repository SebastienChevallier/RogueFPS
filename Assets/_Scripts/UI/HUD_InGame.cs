using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine.UI;
using System.Collections;

public class HUD_InGame : MonoBehaviour
{
    public Image cursor;
    public Image grip1, grip2;
    public Material edgesMaterial;
    private EdgeDetection edgesDetectFeature;
    public float duration;

    public Color actualColor = Color.white;

    public void Start()
    {
        var urpAsset = GraphicsSettings.currentRenderPipeline as UniversalRenderPipelineAsset;
        var rendererData = urpAsset.rendererDataList;

       foreach (var renderer in rendererData)
        {            
            if (renderer.rendererFeatures[0] is EdgeDetection edgeDetection)
            {
                edgesDetectFeature = edgeDetection;
                edgesDetectFeature.settings.outlineColor = actualColor;
            }
        }
    }

    public void UpdateColorCursor(Color valueColor)
    {
        LerpEdgeColorCoroutine(actualColor, valueColor, duration);

        cursor.color = valueColor;
        actualColor = valueColor;

        if (edgesMaterial != null)
        {
            edgesMaterial.SetColor("_Color", valueColor);
        }
    }

    public void UpdateEdgesColor(Color valueColor)
    {
        if (edgesDetectFeature != null)
        {
            edgesDetectFeature.settings.outlineColor = valueColor;
        }
    }

    public async void LerpEdgeColorCoroutine(Color baseColor, Color targetColor, float duration)
    {
        //Color startColor = actualColor;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            await System.Threading.Tasks.Task.Yield();
            elapsed += Time.deltaTime;
            Color currentColor = Color.Lerp(baseColor, targetColor, elapsed / duration);

            // Mettre à jour visuellement et dans les features
            UpdateEdgesColor(currentColor);
        }

        // S'assurer que la couleur finale est bien appliquée
        UpdateEdgesColor(targetColor);
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
