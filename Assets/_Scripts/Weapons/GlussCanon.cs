using UnityEngine;

public class GlussCanon : BaseWeapon
{
    [Header("Prefab & paramètres")]
    public GameObject ropePrefab;

    private GameObject rope;
    private int nbOfAttach = 0;

    private Rigidbody anchorRb;
    private Rigidbody targetRb;
    private Vector3 anchorPoint;
    private Vector3 targetPoint;

    public override void Shoot()
    {
        if (isRecoiling) return;
        StartRecoil();

        ResetIfNeeded();

        if (!TryGetHit(out RaycastHit hit)) return;

        if (nbOfAttach == 0)
            HandleFirstShot(hit);
        else if (nbOfAttach == 1)
            HandleSecondShot(hit);
    }

    // ─── GESTION DU RECOIL ─────────────────────────────────────────────────────────
    private void StartRecoil()
    {
        isRecoiling = true;
    }

    // ─── RAYCAST ───────────────────────────────────────────────────────────────────
    private bool TryGetHit(out RaycastHit hit)
    {
        return Physics.Raycast(
            Camera.main.transform.position,
            Camera.main.transform.forward,
            out hit, 100f
        );
    }

    // ─── RÉINITIALISATION ─────────────────────────────────────────────────────────
    private void ResetIfNeeded()
    {
        if (nbOfAttach >= 2)
            ResetRope();
    }

    private void ResetRope()
    {
        if (rope != null) Destroy(rope);

        rope = null;
        nbOfAttach = 0;
        anchorRb = null;
        targetRb = null;
        // anchorPoint et targetPoint restent intacts
    }

    // ─── PREMIER TIR ───────────────────────────────────────────────────────────────
    private void HandleFirstShot(RaycastHit hit)
    {
        CreateRopeAt(hit.point);
        RecordAnchor(hit);
        nbOfAttach = 1;
    }

    private void CreateRopeAt(Vector3 position)
    {
        rope = Instantiate(ropePrefab, position, Quaternion.identity);
        Debug.Log("Corde instanciée");
    }

    private void RecordAnchor(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            anchorRb = rb;
            Debug.Log("Ancrage dynamique enregistré");
        }
        else
        {
            anchorPoint = hit.point;
            Debug.Log("Ancrage statique enregistré");
        }
    }

    // ─── SECOND TIR ────────────────────────────────────────────────────────────────
    private void HandleSecondShot(RaycastHit hit)
    {
        RecordTarget(hit);
        SetupElasticJoint();
        nbOfAttach = 2;
    }

    private void RecordTarget(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody rb))
        {
            targetRb = rb;
            Debug.Log("Cible dynamique enregistrée");
        }
        else
        {
            targetPoint = hit.point;
            Debug.Log("Cible statique enregistrée");
        }
    }

    // ─── INITIALISATION DU JOINT ÉLASTIQUE ─────────────────────────────────────────
    private void SetupElasticJoint()
    {
        var joint = GetOrAddElasticJoint();

        // 1) Dynamic ↔ Dynamic
        if (anchorRb != null && targetRb != null)
        {
            joint.Initialize(anchorRb, targetRb);
            Debug.Log("ElasticJoint : dynamique ↔ dynamique");
        }
        // 2) Dynamic ↔ Static
        else if (anchorRb != null && targetRb == null)
        {
            joint.Initialize(anchorRb, targetPoint);
            Debug.Log("ElasticJoint : dynamique ↔ statique");
        }
        // 3) Static ↔ Dynamic
        else if (anchorRb == null && targetRb != null)
        {
            joint.Initialize(targetRb, anchorPoint);
            Debug.Log("ElasticJoint : dynamique ↔ statique (inversé)");
        }
        // 4) Static ↔ Static (pas d’effet)
        else
        {
            Debug.Log("Deux ancrages statiques → aucun effet");
            ResetRope();
        }
    }

    private ElasticJoint GetOrAddElasticJoint()
    {
        var joint = rope.GetComponent<ElasticJoint>();
        if (joint == null)
            joint = rope.AddComponent<ElasticJoint>();
        return joint;
    }
}
