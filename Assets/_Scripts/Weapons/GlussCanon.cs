using UnityEngine;

public class GlussCanon : BaseWeapon
{
    public GameObject ropePrefab;
    private GameObject rope;
    public int nbOfAttach = 0;
    private bool anchorIsStatic = false; // Indique si le premier tir est sur un objet statique

    public Rigidbody rbA;
    public Rigidbody rbB;

    // Réinitialise la corde
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

    // Crée la corde à la position de l'impact
    private void CreateRope(Vector3 position)
    {
        rope = Instantiate(ropePrefab, position, Quaternion.identity);
        Debug.Log("Corde instanciée");
    }

    // Attache la corde à l'objet dynamique visé
    private void AttachRope(Transform parent)
    {
        rope.transform.SetParent(parent, false);
        // On active la physique sur la corde
        //rope.GetComponent<Rigidbody>().isKinematic = false;
        nbOfAttach++;
        anchorIsStatic = false;
        Debug.Log("Corde attachée au Rigidbody via SetParent");
    }

    // Connecte via ElasticJoint dans le cas d'un ancrage dynamique (les deux extrémités dynamiques)
    private void ConnectElastic(Rigidbody Rb1, Rigidbody Rb2)
    {
        ElasticJoint joint = rope.GetComponent<ElasticJoint>();
        joint.Initialize(Rb1, Rb2);
        nbOfAttach++;
        Debug.Log("ElasticJoint connecté (ancrage dynamique)");
    }

    // Connecte via ElasticJoint pour un ancrage statique (seul l'objet dynamique est mobile)
    private void ConnectElasticToStatic(Rigidbody newRb)
    {
        
        ElasticJoint joint = rope.GetComponent<ElasticJoint>();
        joint.Initialize(newRb, rope.transform.position);

        nbOfAttach++;
        Debug.Log("ElasticJoint connecté (ancrage statique)");
    }

    public override void Shoot()
    {
        if (isRecoiling)
            return;
        isRecoiling = true;

        // Réinitialise la corde si on a déjà 2 attachements
        if (nbOfAttach > 1)
        {
            ResetRope();
            return;
        }
            

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100f))
        {
            // Premier tir : création de la corde si elle n'existe pas déjà
            if (rope == null)
            {
                CreateRope(hit.point);

                // Si l'objet touché possède un Rigidbody, c'est un ancrage dynamique
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
                // Si le premier tir était sur un objet dynamique
                ConnectElastic(rbA, rbB);
            }
        }
    }
}
