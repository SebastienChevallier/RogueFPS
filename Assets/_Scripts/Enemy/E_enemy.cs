using UnityEngine;
using UnityEngine.AI;

public class E_enemy : E_Entity
{
    [SerializeField]
    private NavMeshAgent agent;
    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        agent.SetDestination(PlayerModel.instance.playerPos);
    }

    public override void OnDie()
    {
        E_Wave.instance.OnEnemyDie();
        base.OnDie();
    }
}
