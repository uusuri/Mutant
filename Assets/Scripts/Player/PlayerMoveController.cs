using UnityEngine;
using Player.Mutation;

namespace Player
{
    public class PlayerMoveController : BaseController
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;
        private readonly ContactsPoller _contactsPoller;

        public PlayerMoveController(PlayerView view, GameModel model, 
            ContactsPoller contactsPoller)
        {
            _view = view;
            _model = model.CurrentPlayer;
            _contactsPoller = contactsPoller;
        }

        public void FixedUpdate()
        {
            if (_model.CurrentMutationState.Value == MutationState.Bat)
                return;
            var xAxisInput = Input.GetAxis("Horizontal");
            var isGoSideWay = Mathf.Abs(xAxisInput) > _model.MoveThreshold;

            if (isGoSideWay)
                _view.SpriteRenderer.flipX = xAxisInput > 0;

            var newVelocity = 0f;

            if (isGoSideWay &&
                (xAxisInput > 0 || !_contactsPoller.HasLeftContacts) &&
                (xAxisInput < 0 || !_contactsPoller.HasRightContacts))
            {
                newVelocity = _model.WalkSpeed * (xAxisInput < 0 ? -1 : 1);
            }

            _view.Rigidbody.linearVelocity = new Vector2(newVelocity, _view.Rigidbody.linearVelocity.y);
        }
    }
}