using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]

public class PlayerView : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer _spriteRenderer;

    [SerializeField]
    private CircleCollider2D _collider;

    [SerializeField]
    private Rigidbody2D _rigidbody;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public CircleCollider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;
}