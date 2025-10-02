using UnityEngine;

namespace Player.Mutation
{
    [CreateAssetMenu(fileName = "SpiderPreset",
        menuName = "ScriptableObjects/SpiderPreset")]
    public class SpiderPreset : ScriptableObject
    {
        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float webCooldown;
        [SerializeField]
        private float webSplitDistance;
        [SerializeField]
        private float moveThreshold;
        
        public float WalkSpeed => walkSpeed;
        public float WebCooldown => webCooldown;
        public float WebSplitCooldown => webSplitDistance;
        public float MoveThreshold => moveThreshold;
    }
}