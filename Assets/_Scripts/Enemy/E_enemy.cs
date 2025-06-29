using UnityEngine;
using UnityEngine.AI;

public class E_enemy : E_Entity
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private int scoreOnDie = 100;
    
    public override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        if (agent.isActiveAndEnabled)
        {
            agent.SetDestination(PlayerModel.instance.playerPos);
        }

        if (transform.position.y < -30 && !isDie)
        {
            OnDie();
        }
    }

    public override void OnDie()
    {
        E_Wave.instance.OnEnemyDie();
        InGamePage.Instance.AddScore(scoreOnDie);
        base.OnDie();
    }
}
