using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EAttack
{
    SingleAttack,
    MultiAttack,
}

public class AttackComponent : MonoBehaviour // Ability
{
    public float Dmg;
    public float Cooldown;
    public float RangeAttack;
    public float SpeedProjectile;
    public EAttack AttackType;
    public GameObject Projectile;
    private Transform ShotPos;
    private Transform enemy;

    private float timeShoot = 0;

    public void Setup(float Dmg, float Cooldown, float RangeAttack, float SpeedProjectile, Transform ShotPos)
    {
        this.Dmg = Dmg;
        this.Cooldown = Cooldown;
        this.RangeAttack = RangeAttack;
        this.ShotPos = ShotPos;
        this.SpeedProjectile = SpeedProjectile;
    }

    public void SetTarget(Transform target)
    {
        this.enemy = target;
    }

    private void Update()
    {
        if (enemy == null) return;
        timeShoot += Time.deltaTime;
        if (timeShoot > Cooldown)
        {
            timeShoot = 0;
            SpawnProjectile();
        }
    }

    private void SpawnProjectile()
    {
        var projectile = Instantiate(Projectile);
        projectile.transform.position = ShotPos.position;

        Projectile pjt = projectile.GetComponent<Projectile>();
        pjt.SetDamage(this.Dmg);
        pjt.SetSpeed(this.SpeedProjectile);

        Vector3 direction = (enemy.position - ShotPos.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.eulerAngles = new Vector3(0, 0, angle);
    }
}
