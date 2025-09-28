using Player.Mutation;
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
    
    [Header("Mutation Sprites")]
    [SerializeField]
    private Sprite _slimeSprite;
    
    [SerializeField]
    private Sprite _snakeSprite;
    
    [SerializeField]
    private Sprite _batSprite;
    
    [SerializeField]
    private Sprite _spiderSprite;

    public SpriteRenderer SpriteRenderer => _spriteRenderer;
    public CircleCollider2D Collider => _collider;
    public Rigidbody2D Rigidbody => _rigidbody;
    
    public void SetMutationSprite(MutationState mutationState)
    {
        _spriteRenderer.sprite = mutationState switch
        {
            MutationState.Slime => _slimeSprite,
            MutationState.Snake => _snakeSprite,
            MutationState.Bat => _batSprite,
            MutationState.Spider => _spiderSprite,
            _ => _spriteRenderer.sprite
        };
    }
}