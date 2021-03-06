﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityPull : MonoBehaviour
{
    public float gravity;
    public string gravLayer = "GravEffector";


    private List<IMovable> targets = new List<IMovable>();
    private List<Transform> targetTrans = new List<Transform>();


    private void Start()
    {
        gameObject.layer = LayerMask.NameToLayer(gravLayer);
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            if (targetTrans[i] == null || targets[i] == null)
            {
                targets.RemoveAt(i);
                targetTrans.RemoveAt(i);
                continue;
            }

            targets[i].ApplyGravForce((transform.position - targetTrans[i].position).normalized * gravity * Time.fixedDeltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IMovable newTarget = other.transform.root.GetComponent<IMovable>();

        if (newTarget != null && !targets.Contains(newTarget))
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

    private void OnDestroy()
    {
        for (int i = 0; i < targets.Count; i++)
        {
            targets[i].FlushGravForce();
        }
    }
}
