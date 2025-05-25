using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricEffect : MonoBehaviour
{
    [Header("Paramètres de l'effet")]
    public float radius = 5f;               // Rayon de propagation
    public float duration = 2f;             // Durée totale de l'effet actif
    public GameObject electricFX;           // Prefab VFX (LineRenderer, particules…)
    public LayerMask layerMask;             // Couches éligibles
    public float reactivationDelay = 1f;    // Délai avant réactivation
    public int dmg;

    // État interne
    public bool isActive = false;
    private float timer = 0f;
    private float lastActivationTime = -Mathf.Infinity;
    private HashSet<Transform> seen = new HashSet<Transform>();

    /// <summary>
    /// À appeler pour démarrer l'effet sur cet objet
    /// </summary>
    public void ActivateElectricEffect()
    {
        isActive = true;
        timer = 0f;
        lastActivationTime = Time.time;

        // Reset de la liste pour une nouvelle propagation
        seen.Clear();
        seen.Add(transform); // on s'ignore soi-même

        // activation du VFX local
        if (electricFX != null)
            electricFX.SetActive(true);
    }

    private void Update()
    {
        if (!isActive) return;

        timer += Time.deltaTime;

        // à mi-durée, on déclenche une vague de propagation
        if (timer >= duration / 2f)
        {
            Electrify();
            // pour ne pas relancer plusieurs fois sur la même activation
            timer = duration;
        }

        // fin de l'effet
        if (timer >= duration)
        {
            isActive = false;
            if (electricFX != null)
                electricFX.SetActive(false);
        }
    }

    private void Electrify()
    {
        // Récupère toutes les cibles potentielles
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius, layerMask);
        List<Transform> candidates = new List<Transform>();

        foreach (var col in hitColliders)
        {
            Transform t = col.transform;
            if (t == transform) continue;          // ignorer soi-même
            if (seen.Contains(t)) continue;        // déjà propagé dans cette vague
            candidates.Add(t);
        }

        // Tri par distance croissante
        candidates.Sort((a, b) =>
            (a.position - transform.position).sqrMagnitude
            .CompareTo((b.position - transform.position).sqrMagnitude)
        );

        // Parcourt les cibles, cherche la première sortie de latence
        foreach (var t in candidates)
        {
            if (t.TryGetComponent<ElectricEffect>(out var other))
            {
                if (Time.time >= other.lastActivationTime + reactivationDelay)
                {
                    // on l'ajoute aux vus, et on l'active
                    seen.Add(t);
                    other.ActivateElectricEffect();
                    Debug.Log($"{other.name} electrified by {name} at {Time.time}");
                    break;
                }
                // sinon on passe au suivant
            }
        }
    }
}
