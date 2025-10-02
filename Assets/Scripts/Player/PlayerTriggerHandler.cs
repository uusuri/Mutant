using UnityEngine;

namespace Player
{
    public class PlayerTriggerHandler : MonoBehaviour
    {
        public event System.Action<Collider2D> OnTriggerEnter;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTriggerEnter?.Invoke(other);
        }
    }
}
