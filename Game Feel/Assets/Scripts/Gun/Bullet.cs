using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 10f;
    [SerializeField] private int _damageAmount = 1;

    private Vector2 _fireDirection;
    private Rigidbody2D _rigidBody;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    public void SetUp(Vector2 _bullateSpownPosition , Vector2 _mousePos)
    {
        _fireDirection = (_mousePos-_bullateSpownPosition).normalized  ;
    }
    private void FixedUpdate()
    {
        _rigidBody.velocity = _fireDirection * _moveSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        Entity myEntity = other.gameObject.GetComponent<Entity>();
        if (myEntity != null)
        {
            myEntity.myHealth.TakeDamage(_damageAmount);
            myEntity.DamageEffect(Mathf.RoundToInt(_fireDirection.x));
        }

        this.gameObject.SetActive(false);
    }
}