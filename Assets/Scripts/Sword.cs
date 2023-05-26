using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    public int swordDamage = 30;

    private void OnTriggerEnter(Collider other)
    {
        EnemyMovement enemy = other.GetComponent<EnemyMovement>();
        
        if (enemy != null)
        {
            enemy.TakeSwordHit(swordDamage);
        }
    }
}
