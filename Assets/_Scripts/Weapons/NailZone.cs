using System.Collections.Generic;
using UnityEngine;

public class NailZone : MonoBehaviour
{
    public Rigidbody rb;
    private string layerName;

    private List<E_Entity> _entities = new();
    private List<float> _entitiesDelay = new();

    private NailGunData _nailData;
    private void Init(NailGunData nailData)
    {
        layerName = "Enemy";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            _entities.Add(other.gameObject.GetComponent<E_Entity>());
            _entitiesDelay.Add(_nailData.Delay);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < _entities.Count; i++)
        {
            _entitiesDelay[i] -= Time.deltaTime;
            if ( _entitiesDelay[i] < 0 )
            {
                Damage(_entities[i]);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer(layerName))
        {
            int index = _entities.FindIndex(x => x.Equals(other.gameObject.GetComponent<E_Entity>()));
            _entities.RemoveAt(index);
            _entitiesDelay.RemoveAt(index);
        }
    }

    private void Damage(E_Entity entity)
    {
        entity.OnDecreaseHealth(_nailData.Damage);
    }
}
