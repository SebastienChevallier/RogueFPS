using UnityEngine;

[CreateAssetMenu(fileName = "NailGunData", menuName = "Game/Gun/NailGunData", order = 1)]
public class NailGunData : ScriptableObject
{
    [SerializeField]
    private float boxRadius;
    [SerializeField]
    private int damage;
    [SerializeField]
    private float delay;
    [SerializeField]
    private float movementDecreaseMult;
    [SerializeField]
    private NailZone nailZonePrefab;
    [SerializeField]
    private float gunForce;
    [SerializeField]
    private float spreadAngle;

    public float BoxRadius => boxRadius;
    public int Damage => damage;
    public float Delay => delay;
    public float MovementDecreaseMult => movementDecreaseMult;
    public NailZone NailZonePrefab => nailZonePrefab;
    public float GunForce => gunForce;
    public float SpreadAngle => spreadAngle;
}
