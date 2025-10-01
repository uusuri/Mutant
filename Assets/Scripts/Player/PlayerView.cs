using Player.Mutation;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(SpriteRenderer), typeof(CircleCollider2D), typeof(Rigidbody2D))]

    public class PlayerView : MonoBehaviour
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private new CircleCollider2D collider;

        [SerializeField]
        private new Rigidbody2D rigidbody;
    
        [Header("Mutation Sprites")]
        [SerializeField]
        private Sprite _slimeSprite;
    
        [SerializeField]
        private Sprite _snakeSprite;
    
        [SerializeField]
        private Sprite _batSprite;
    
        [SerializeField]
        private Sprite _spiderSprite;
        
        public SpriteRenderer SpriteRenderer => spriteRenderer;
        public CircleCollider2D Collider => collider;
        public Rigidbody2D Rigidbody => rigidbody;
    
        public void SetMutationSprite(MutationState mutationState)
        {
            spriteRenderer.sprite = mutationState switch
            {
                MutationState.Slime => _slimeSprite,
                MutationState.Snake => _snakeSprite,
                MutationState.Bat => _batSprite,
                MutationState.Spider => _spiderSprite,
                _ => spriteRenderer.sprite
            };
        }
    }
}