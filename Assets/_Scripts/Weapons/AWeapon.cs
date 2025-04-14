using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    protected bool isRecoiling;
    protected bool isReleading;
    protected float recoilTime;
    protected float reloadTime;

    [Range(10, 50)]
    public int ammo;
    [Range(0.1f, 1f)]
    public float recoilSpeed;
    [Range(0.1f, 2f)]
    public float reloadSpeed;
    [Range(1, 20)]
    public int bulletsPerShoot;    

    public abstract void Shoot();

    public abstract void Recoil();

    public abstract void Reload();
}
