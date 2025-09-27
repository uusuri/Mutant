namespace Player
{
    public class PlayerModel
    {
        public float WalkSpeed { get;}
        public float JumpForce { get; }
        public float MoveThreshold { get; }
        public float FlyThreshold { get; }
    
        public int MaxAirJumps { get; }
        public float CoyoteTime { get; }
        public float JumpBufferTime { get; }
        public float JumpGravityScale { get; }
        public float FallGravityScale { get; }
        public float FastFallGravityScale { get; }
        public float MaxFallSpeed { get; }

        public PlayerModel(PlayerPreset playerPreset)
        {
            WalkSpeed = playerPreset.WalkSpeed;
            JumpForce = playerPreset.JumpForce;
            MoveThreshold = playerPreset.MoveThreshold;
            FlyThreshold = playerPreset.FlyThreshold;
        
            MaxAirJumps = playerPreset.MaxAirJumps;
            CoyoteTime = playerPreset.CoyoteTime;
            JumpBufferTime = playerPreset.JumpBufferTime;
            JumpGravityScale = playerPreset.JumpGravityScale;
            FallGravityScale = playerPreset.FallGravityScale;
            FastFallGravityScale = playerPreset.FastFallGravityScale;
            MaxFallSpeed = playerPreset.MaxFallSpeed;
        }
    }
}