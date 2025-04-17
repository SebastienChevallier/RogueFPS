using UnityEngine;

public class GlussCanon : BaseWeapon
{
    public GameObject ropePrefab;
    private GameObject rope;
    public int nbOfAttach = 0;
    private bool anchorIsStatic = false; // Indique si le premier tir est sur un objet statique

    public Rigidbody rbA;
    public Rigidbody rbB;

    // R�initialise la corde
    private void ResetRope()
    {
        if (rope != null)
            Destroy(rope);

        rope = null;
        rbB = null;
        rbA = null;
        nbOfAttach = 0;
        anchorIsStatic = false;
    }

    // Cr�e la corde � la position de l'impact
    private void CreateRope(Vector3 position)
    {
        rope = Instantiate(ropePrefab, position, Quaternion.identity);
        Debug.Log("Corde instanci�e");
    }

    // Attache la corde � l'objet dynamique vis�
    private void AttachRope(Transform parent)
    {
        rope.transform.SetParent(parent, false);
        // On active la physique sur la corde
        //rope.GetComponent<Rigidbody>().isKinematic = false;
        nbOfAttach++;
        anchorIsStatic = false;
        Debug.Log("Corde attach�e au Rigidbody via SetParent");
    }

    // Connecte via ElasticJoint dans le cas d'un ancrage dynamique (les deux extr�mit�s dynamiques)
    private void ConnectElastic(Rigidbody Rb1, Rigidbody Rb2)
    {
        ElasticJoint joint = rope.GetComponent<ElasticJoint>();
        joint.Initialize(Rb1, Rb2);
        nbOfAttach++;
        Debug.Log("ElasticJoint connect� (ancrage dynamique)");
    }

    // Connecte via ElasticJoint pour un ancrage statique (seul l'objet dynamique est mobile)
    private void ConnectElasticToStatic(Rigidbody newRb)
    {
        
        ElasticJoint joint = rope.GetComponent<ElasticJoint>();
        joint.Initialize(newRb, rope.transform.position);

        nbOfAttach++;
        Debug.Log("ElasticJoint connect� (ancrage statique)");
    }

    public override void Shoot()
    {
        if (isRecoiling)
            return;
        isRecoiling = true;

        // R�initialise la corde si on a d�j� 2 attachements
        if (nbOfAttach > 1)
        {
            ResetRope();
            return;
        }
            

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f))
        {
            // Premier tir : cr�ation de la corde si elle n'existe pas d�j�
            if (rope == null)
            {
                CreateRope(hit.point);

                // Si l'objet touch� poss�de un Rigidbody, c'est un ancrage dynamique
                if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody hitRb))
                {
                    rbA = hitRb;
                }
                else
                {                    
                    anchorIsStatic = true;
                    Debug.Log("Ancrage statique (objet sans Rigidbody)");
                }

                return;
            }

            if (hit.collider.TryGetComponent<Rigidbody>(out Rigidbody hitRb2))
            {
                rbB = hitRb2;
            }

            if (anchorIsStatic)
            {
                if(rbA == null)
                    ConnectElasticToStatic(rbB);
                if(rbB == null)
                    ConnectElasticToStatic(rbA);
            }
            else
            {
                // Si le premier tir �tait sur un objet dynamique
                ConnectElastic(rbA, rbB);
            }
        }
    }
}
