using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : MonoBehaviour
{
    public float gravity;
    public string gravLayer = "GravEffector";

    private List<IMovable> targets = new List<IMovable>();
    private List<Transform> targetTrans = new List<Transform>();


    private void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer(gravLayer);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
                targets[i].ApplyGravForce((transform.position - targetTrans[i].position).normalized * gravity * Time.fixedDeltaTime);
            else
                targets.RemoveAt(i);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IMovable newTarget = other.transform.root.GetComponent<IMovable>();

        if (newTarget != null)
        {
            targets.Add(newTarget);
            targetTrans.Add(other.transform.root);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IMovable oldTarget = other.transform.root.GetComponent<IMovable>();

        if (oldTarget != null)
        {
            oldTarget.FlushGravForce();
            targets.Remove(oldTarget);
            targetTrans.Remove(other.transform.root);
        }
    }
}
