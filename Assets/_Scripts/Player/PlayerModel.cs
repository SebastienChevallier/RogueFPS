using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    public static PlayerModel instance;

    [SerializeField]
    private P_Movement p_Movement;

    public Vector3 playerPos => p_Movement.transform.position;

    private void Awake()
    {
        instance = this;
    }
} 
