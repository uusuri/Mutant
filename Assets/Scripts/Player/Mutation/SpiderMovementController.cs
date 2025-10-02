using UnityEngine;

namespace Player.Mutation
{
    public class SpiderMovementController : BaseController
    {
        private readonly PlayerView _view;
        private readonly GameModel _gameModel;
        private readonly ContactsPoller _contacts;

        public SpiderMovementController(PlayerView view, GameModel gameModel, ContactsPoller contacts)
        {
            _view = view;
            _gameModel = gameModel;
            _contacts = contacts;
        }

        private enum SurfaceType { None, Ground, LeftWall, RightWall, Ceiling }

        private SurfaceType GetSurfaceType()
        {
            if (_contacts.IsGrounded) return SurfaceType.Ground;
            if (_contacts.HasCeiling) return SurfaceType.Ceiling;
            if (_contacts.HasLeftContacts && !_contacts.HasRightContacts) return SurfaceType.LeftWall;
            if (_contacts.HasRightContacts && !_contacts.HasLeftContacts) return SurfaceType.RightWall;
            return SurfaceType.None;
        }

        public void FixedUpdate()
        {
            var surfaceType = GetSurfaceType();
            var attached = surfaceType != SurfaceType.None;

            var surfaceNormal = surfaceType switch
            {
                SurfaceType.Ground => _contacts.GroundNormal == Vector2.zero ? Vector2.up : _contacts.GroundNormal,
                SurfaceType.Ceiling => _contacts.CeilingNormal == Vector2.zero ? Vector2.down : _contacts.CeilingNormal,
                SurfaceType.LeftWall => Vector2.right,
                SurfaceType.RightWall => Vector2.left,
                _ => Vector2.up
            };

            Vector2 tangent = new(surfaceNormal.y, -surfaceNormal.x);

            var input = Input.GetAxis("Horizontal");
            var threshold = _gameModel.CurrentPlayer.MoveThreshold;
            var dir = Mathf.Abs(input) > threshold ? Mathf.Sign(input) : 0f;

            if (attached)
            {
                _view.Rigidbody.gravityScale = 0f;

                const float rotationSpeed = 12f;
                var velocity = tangent * (dir * _gameModel.CurrentPlayer.WalkSpeed);
                _view.Rigidbody.linearVelocity = velocity;

                var angle = Mathf.Atan2(surfaceNormal.y, surfaceNormal.x) * Mathf.Rad2Deg - 90f;
                var targetRotation = Quaternion.Euler(0f, 0f, angle);
                _view.transform.rotation = Quaternion.Lerp(
                    _view.transform.rotation,
                    targetRotation,
                    rotationSpeed * Time.fixedDeltaTime
                );

                if (dir == 0) return;
                var moveWorld = tangent * dir;
                _view.SpriteRenderer.flipX = moveWorld.x > 0f;
            }
            else
            {
                _view.Rigidbody.gravityScale = _gameModel.CurrentPlayer.FallGravityScale;
            }
        }
    }
}
