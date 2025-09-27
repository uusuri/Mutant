using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "PlayerPreset",
        menuName = "ScriptableObjects/PlayerPreset")]
    public class PlayerPreset : ScriptableObject
    {
        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float jumpForce;
        [SerializeField]
        private float moveThreshold;
        [SerializeField]
        private float flyThreshold;
        [SerializeField]
        private int maxAirJumps;
        [SerializeField]
        private float coyoteTime;
        [SerializeField]
        private float jumpBufferTime;
        [SerializeField]
        private float jumpGravityScale;
        [SerializeField]
        private float fallGravityScale;
        [SerializeField]
        private float fastFallGravityScale;
        [SerializeField]
        private float maxFallSpeed;
    
        public float WalkSpeed => walkSpeed;
        public float JumpForce => jumpForce;
        public float MoveThreshold => moveThreshold;
        public float FlyThreshold => flyThreshold;
    
        public int MaxAirJumps => maxAirJumps;
        public float CoyoteTime => coyoteTime;
        public float JumpBufferTime => jumpBufferTime;
        public float JumpGravityScale => jumpGravityScale;
        public float FallGravityScale => fallGravityScale;
        public float FastFallGravityScale => fastFallGravityScale;
        public float MaxFallSpeed => maxFallSpeed;
    }
}