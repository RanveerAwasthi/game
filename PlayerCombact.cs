using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombact : MonoBehaviour
{
    public Animator animator;
    public Transform attackPoint;
    public float attackRange = 0.7f;
    public LayerMask enemyLayers;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {

             Attack();
        }
    }
    void Attack()
    {
        animator.SetTrigger("Attack");
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("we hit" + enemy.name);
        }
    }
    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

       Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
