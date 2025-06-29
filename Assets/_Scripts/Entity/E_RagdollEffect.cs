using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class E_RagdollEffect : MonoBehaviour
{
    [Header("references")]
    public Rigidbody body;
    public NavMeshAgent agent;
    public Collider mainCollider;
    public Rigidbody spineBody;

    Rigidbody[] rigidbodies;
    Collider[] ragdollColliders;
    public float minRagdollMagnitude;
    public float delayOnDisableRagdoll;
    public float maxRagdollToDisable;
    public bool isEnableRagdoll;

    public List<BoneInfo> boneInfos = new List<BoneInfo>();

    private void Start()
    {
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        var ragdollBones = GetComponentsInChildren<Rigidbody>();
        foreach (var rb in ragdollBones)
        {
            BoneInfo info = new BoneInfo();
            info.bone = rb.transform;
            info.position = rb.transform.localPosition;
            info.rotation = rb.transform.localRotation;
            boneInfos.Add(info);
        }


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
            if (spineBody.linearVelocity.magnitude <= maxRagdollToDisable)
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
        spineBody.linearVelocity = body.linearVelocity;
    }

    public void DisableRagdoll()
    {
        isEnableRagdoll = false;
        //Activer timer
        body.constraints = RigidbodyConstraints.FreezeRotation;
        agent.enabled = true;
        ToggleRagdoll(true);

        foreach (var info in boneInfos)
        {
            info.bone.localPosition = info.position;
            info.bone.localRotation = info.rotation;
        }
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
        
        mainCollider.enabled = activate;
    }
}


public class BoneInfo
{
    public Transform bone;
    public Vector3 position;
    public Quaternion rotation;
}
