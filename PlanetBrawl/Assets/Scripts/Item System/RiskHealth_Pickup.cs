using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiskHealth_Pickup : MonoBehaviour
{
    public float switchTime = 0.5f;
    public float healthBonus;
    public float damage;
    public float dmgStunTime = 0.25f;
    public Sprite healthSprite;
    public Sprite dmgSprite;

    private Player_HealthController target;
    private SpriteRenderer SpriteRenderer;
    private bool heal = false;
    private float timer;


    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        timer = switchTime;
    }

    private void FixedUpdate()
    {
        timer -= switchTime * Time.fixedDeltaTime;

        if (timer <= 0)
        {
            timer = switchTime;
            heal = !heal;

            if (heal)
                SpriteRenderer.sprite = healthSprite;
            else
                SpriteRenderer.sprite = dmgSprite;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        target = other.transform.root.GetComponent<Player_HealthController>();

        //Hit the target if it is damageable
        if (target != null)
        {
            if (heal)
            {
                target.Heal(healthBonus);
            }
            else
            {
                target.Hit(damage, DamageType.physical, Vector2.zero, dmgStunTime);
            }
            
            Destroy(gameObject);
        }
    }
}
