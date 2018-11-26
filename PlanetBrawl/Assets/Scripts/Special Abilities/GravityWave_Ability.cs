using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityWave_Ability : MonoBehaviour, ISpecialAbility
{
    public float radius;
    public float force;
    public float forceTime;
    public float invincibilityTime;
    public LayerMask gravMask;
    public float timeout;

    private Player_HealthController healthController;
    private bool canAttack = true;
    private bool pressed = false;


    void Start()
    {
        healthController = GetComponent<Player_HealthController>();
    }

    public void Use()
    {
        if (canAttack && !pressed)
        {
            canAttack = false;
            pressed = true;

            healthController.Stun(invincibilityTime, false);

            Collider2D[] targetColliders = Physics2D.OverlapCircleAll(transform.position, radius, gravMask);

            for (int i = 0; i < targetColliders.Length; i++)
            {
                IMovable newTarget = targetColliders[i].GetComponent<IMovable>();

                if (newTarget == null)
                {
                    newTarget = targetColliders[i].attachedRigidbody?.GetComponent<IMovable>();
                }

                if (newTarget != null)
                {
                    newTarget.ApplyTempExForce((transform.position - targetColliders[i].transform.position).normalized * force, forceTime);
                }
            }

            StartCoroutine(ResetAttack());
        }
    }

    public void StopUse()
    {
        pressed = false;
    }

    IEnumerator ResetAttack()
    {
        yield return new WaitForSeconds(timeout);
        canAttack = true;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
