using UnityEngine;

namespace Player
{
    public class PlayerJumpController : BaseController
    {
        private readonly PlayerView _view;
        private readonly PlayerModel _model;
        private readonly ContactsPoller _contactsPoller;
    
        private bool _shouldJump;
        private bool _jumpInputHeld;
        private float _jumpTimer;
        private float _coyoteTimer;
        private int _airJumpsRemaining;

        public PlayerJumpController(PlayerView view, GameModel model, ContactsPoller contactsPoller)
        {
            _view = view;
            _model = model.CurrentPlayer;
            _contactsPoller = contactsPoller;
        
            _airJumpsRemaining = _model.MaxAirJumps;
        }

        public void Update()
        {
            var jumpPressed = Input.GetKeyDown(KeyCode.Space);
            _jumpInputHeld = Input.GetKey(KeyCode.Space);
        
            if (jumpPressed)
            {
                _shouldJump = true;
                _jumpTimer = _model.JumpBufferTime;
            }
        
            if (_jumpTimer > 0)
            {
                _jumpTimer -= Time.deltaTime;
            }
        
            if (_contactsPoller.IsGrounded)
            {
                _coyoteTimer = _model.CoyoteTime;
                _airJumpsRemaining = _model.MaxAirJumps;
            }
            else
            {
                _coyoteTimer -= Time.deltaTime;
            }
        }

        public void FixedUpdate()
        {
            var canJump = (_contactsPoller.IsGrounded || _coyoteTimer > 0 || _airJumpsRemaining > 0) && _jumpTimer > 0;
        
            if (canJump && _shouldJump)
                PerformJump();
        
            if (_view.Rigidbody.linearVelocity.y > 0 && _jumpInputHeld)
                _view.Rigidbody.gravityScale = _model.JumpGravityScale;
            else
                _view.Rigidbody.gravityScale = _model.FallGravityScale;
            
            if (Input.GetKey(KeyCode.S) && _view.Rigidbody.linearVelocity.y < 0)
            {
                _view.Rigidbody.gravityScale = _model.FastFallGravityScale;
            }
        
            if (_view.Rigidbody.linearVelocity.y < -_model.MaxFallSpeed)
            {
                _view.Rigidbody.linearVelocity = new Vector2(
                    _view.Rigidbody.linearVelocity.x, 
                    -_model.MaxFallSpeed
                );
            }
            _shouldJump = false;
        }

        private void PerformJump()
        {
            _view.Rigidbody.linearVelocity = new Vector2(_view.Rigidbody.linearVelocity.x, 0);
            _view.Rigidbody.AddForce(Vector2.up * _model.JumpForce, ForceMode2D.Impulse);

            if (_contactsPoller.IsGrounded || _coyoteTimer > 0)
                _coyoteTimer = 0;
            else
                _airJumpsRemaining = Mathf.Max(0, _airJumpsRemaining - 1);

            _jumpTimer = 0;
            _shouldJump = false;
        }
    }   
}