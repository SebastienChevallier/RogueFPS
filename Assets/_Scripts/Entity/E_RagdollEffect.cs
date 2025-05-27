using UnityEngine;
using UnityEngine.AI;

public class E_RagdollEffect : MonoBehaviour
{
    [Header("references")]
    public Rigidbody body;
    public NavMeshAgent agent;

    Rigidbody[] rigidbodies;
    Collider[] ragdollColliders;
    public float minRagdollMagnitude;
    public float delayOnDisableRagdoll;
    public float maxRagdollToDisable;
    public bool isEnableRagdoll;

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();
        ToggleRagdoll(true);
    }


    private void Update()
    {
        if (!isEnableRagdoll)
        {
            if (body.linearVelocity.magnitude > minRagdollMagnitude)
            {
                ActivateRagdoll();
            }
        } else
        {
            if (body.linearVelocity.magnitude <= maxRagdollToDisable)
            {
                DisableRagdoll();
            }
        }
    }

    public void ActivateRagdoll()
    {
        isEnableRagdoll = true;
        body.constraints = RigidbodyConstraints.None;
        agent.enabled = false;
        ToggleRagdoll(false);
    }

    public void DisableRagdoll()
    {
        isEnableRagdoll = false;
        //Activer timer
        body.constraints = RigidbodyConstraints.FreezeRotation;
        agent.enabled = true;
        ToggleRagdoll(true);
    }

    void ToggleRagdoll(bool activate)
    {
        foreach (Rigidbody rb in rigidbodies) {
            rb.isKinematic = activate;
        }

        foreach (var col in ragdollColliders)
        {
            col.enabled = !activate;
        }
    }
}
