using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IonNebula : MonoBehaviour
{
    public float dps = 20;

    private List<IDamageable> targets = new List<IDamageable>();


    void OnTriggerEnter2D(Collider2D other)
    {
        IDamageable newTarget = other.GetComponent<IDamageable>();

        if (newTarget != null)
        {
            targets.Add(newTarget);
            
            if (targets.Count == 1)
            {
                StartCoroutine(DamageTargets());
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        IDamageable oldTarget = other.GetComponent<IDamageable>();

        if (targets.Contains(oldTarget))
        {
            targets.Remove(oldTarget);
        }
    }

    IEnumerator DamageTargets()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                targets[i].Hit(dps);
            }
            else
            {
                targets.RemoveAt(i);
            }
        }

        if (targets.Count > 0)
            StartCoroutine(DamageTargets());
    }
}
