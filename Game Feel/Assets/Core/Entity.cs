using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public Rigidbody2D myRb { get; private set; }
    public Health myHealth { get; private set; }
   // public EntityFX myFx;
 public EntityFX myFx { get; private set; }

    public Vector2 knockbackForce;
    public int facingDirection = 1;

    public float moveSpeed;
    public float jumpForce;

    protected bool isKnockback;
    private bool facingRight = true;

    protected virtual void Awake()
    {
        myFx = GetComponent<EntityFX>();
        myHealth = GetComponent<Health>();
        myRb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start() { }
    protected virtual void Update() { }
    protected virtual void FixedUpdate() { }

    public virtual void DeathEffect()
    {
        myFx.SplatterEffect();
        myFx.SplatParticle();
    }


    public virtual void DamageEffect( int _direction)
    {
        StartCoroutine(KnockBack(_direction ));
        myFx.Flash();
    }
    IEnumerator KnockBack(int _knockbackDirection)
    {
        SetVelocityZero();
        isKnockback = true;
        myRb.velocity = new Vector2(_knockbackDirection *  knockbackForce.x,knockbackForce.y);
        yield return new WaitForSeconds(.25f);
        isKnockback = false;
        SetVelocityZero();
    }


    protected virtual void SetVelocity(float _x, float _y)
    {
        myRb.velocity = new Vector2(_x, _y);
    }
    protected virtual void SetVelocityZero() => myRb.velocity = Vector2.zero;

    protected virtual void FlipController(float _x )
    {
        if (_x < 0 && facingRight)
            Flip();
        if (_x > 0 && !facingRight)
            Flip();
    }
    protected  void Flip()
    {
        facingRight = !facingRight;
        facingDirection *= -1;
        transform.Rotate(0, 180, 0);
    }
}
