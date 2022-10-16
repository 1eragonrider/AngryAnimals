using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDamage : MonoBehaviour
{
    public int hitPoints = 2;
    public Sprite damageSprite;
    public float damageImpactSpeed;

    private int currentHitPoints;
    private float damageImpactSpeedSqr;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb2d;
    private Collider2D collider2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        collider2D = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHitPoints = hitPoints;
        damageImpactSpeedSqr = damageImpactSpeed * damageImpactSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if  (collision.collider.tag != "Damager") // have to be hit by something that does damage
        {
            return;
        }

        if (collision.relativeVelocity.sqrMagnitude < damageImpactSpeedSqr) // the damager has to be fast enough
        {
            return;
        }

        spriteRenderer.sprite = damageSprite; // change to the damaged sprite
        currentHitPoints--;

        if (currentHitPoints <= 0)
        {
            Kill();
        }
    }

    private void Kill() // makes the target disappear and no longer affect other game objects
    {
        spriteRenderer.enabled = false;
        collider2D.enabled = false;
        rb2d.isKinematic = true;
    }
}

