using UnityEngine;

namespace Player.Mutation
{
	public class BatMovementController : BaseController
	{
		private readonly PlayerView _view;
		private readonly GameModel _gameModel;

		public BatMovementController(PlayerView view, GameModel gameModel)
		{
			_view = view;
			_gameModel = gameModel;
		}

		public void FixedUpdate()
		{
			var batPreset = _gameModel.BatPreset;
			if (!batPreset)
				return;

			var xAxisInput = Input.GetAxis("Horizontal");
			var yAxisInput = Input.GetAxis("Vertical");

			var isGoSideWay = Mathf.Abs(xAxisInput) > batPreset.MoveThreshold;
			var isGoUpDown = Mathf.Abs(yAxisInput) > batPreset.MoveThreshold;

			if (isGoSideWay)
				_view.SpriteRenderer.flipX = xAxisInput > 0;

			var targetVelocityX = 0f;
			var targetVelocityY = 0f;


			if (isGoSideWay)
				targetVelocityX = batPreset.FlySpeed * (xAxisInput < 0 ? -1 : 1);

			if (isGoUpDown)
				targetVelocityY = batPreset.FlySpeed * (yAxisInput < 0 ? -1 : 1);

			_view.Rigidbody.gravityScale = 0f;

			const float smoothingStrength = 6f;
			var current = _view.Rigidbody.linearVelocity;
			var alpha = 1f - Mathf.Exp(-smoothingStrength * Time.fixedDeltaTime);
			var smoothedX = Mathf.Lerp(current.x, targetVelocityX, alpha);
			var smoothedY = Mathf.Lerp(current.y, targetVelocityY, alpha);
			_view.Rigidbody.linearVelocity = new Vector2(smoothedX, smoothedY);
		}
	}
}


