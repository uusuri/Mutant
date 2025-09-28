using UnityEngine;

namespace Player.Mutation
{
    [CreateAssetMenu(fileName = "BatPreset",
        menuName = "ScriptableObjects/BatPreset")]
    public class BatPreset : ScriptableObject
    {
        [SerializeField]
        private float flySpeed;
        [SerializeField]
        private float dashSpeed;
        [SerializeField]
        private float dashTime;
        [SerializeField]
        private float moveThreshold;


        public float FlySpeed => flySpeed;
        public float MoveThreshold => moveThreshold;
        public float DashSpeed => dashSpeed;
        public float DashTime => dashTime;
    }
}