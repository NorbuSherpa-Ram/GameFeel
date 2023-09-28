using System.Collections;
using UnityEngine;

public class Enemy : Entity ,IDamageable
{ 
    [SerializeField] private float _jumpInterval = 4f;
    [SerializeField] private float _changeDirectionInterval = 3f;

    protected override void Awake()
    {
        base.Awake();
    }


    protected override void Start()
    {
        base.Start();
        StartCoroutine(ChangeDirection());
        StartCoroutine(RandomJump());
    }

    protected override void FixedUpdate()
    {
        if (!isKnockback)
            Move();
    }
    public void SetColot(Color _color)
    {
        myFx.mySR.color = _color;
    }


    private void Move()
    {
        SetVelocity(facingDirection * moveSpeed, myRb.velocity.y);
        FlipController(myRb.velocity.x);
    }

    private IEnumerator ChangeDirection()
    {
        while (true)
        {
            if ((UnityEngine.Random.Range(0, 2) * 2 - 1) < 0)
            {
                Flip();// 1 or -1
            }
            yield return new WaitForSeconds(_changeDirectionInterval);
        }
    }

    private IEnumerator RandomJump() 
    {
        while (true)
        {
            yield return new WaitForSeconds(_jumpInterval);
            float randomDirection = Random.Range(-1, 1);
            Vector2 jumpDirection = new Vector2(randomDirection, 1f).normalized;
            myRb.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
        }
    }

    public void TakeDamage(int _damageAmount, int _knockDirection)
    {
       myHealth.TakeDamage(_damageAmount);
      DamageEffect(_knockDirection);
    }

    public void OnHit()
    {
        myFx.Flash();
    }
}
