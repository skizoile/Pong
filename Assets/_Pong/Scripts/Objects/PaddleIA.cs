///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 14:34
///-----------------------------------------------------------------

using Fr.Raymondaud.Sebastien.Pong.UI;
using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Objects {
    public class PaddleIA : Paddles {

		protected Ball target;

		[SerializeField] protected Vector2 startPosIA = new Vector2(8, 0);
		[SerializeField, Range(0.05f, 0.3f)] protected float _moveTime = 0.2f;

		protected float elaspedTime = 0;

		public float MoveTime
		{
			get { return _moveTime; }
			set { _moveTime = value; }
		}

		protected void Awake()
		{
			Ball.OnBallGenerate += Ball_OnBallGenerate;
		}

		public void Instance_OnRestart()
		{
			transform.localPosition = startPosIA;
		}

		private void Ball_OnBallGenerate(Ball pBall)
		{
			target = pBall;
		}

		private void Start()
		{
			transform.localPosition = startPosIA;
		}

		private void Update()
		{
			elaspedTime += Time.deltaTime;
			if (elaspedTime > _moveTime)
			{
				Move();
				elaspedTime %= _moveTime;
			}
			
		}

		protected void Move()
		{
			if (!target || target.CurrentDirection_X == CurrentDirectionX.Right)
				return;


			Vector2 pos = transform.localPosition;
			Vector2 ballPos = target.transform.localPosition;

			if (pos.y <= ballPos.y)
			{
				transform.Translate(Vector3.up * _speed * Time.deltaTime);
			}
			else
			{
				transform.Translate(Vector3.down * _speed * Time.deltaTime);
			}

			if (pos.y > margeY)
				transform.localPosition = new Vector3(pos.x, margeY, 0);
			else if (pos.y < -margeY)
				transform.localPosition = new Vector3(pos.x, -margeY, 0);
		}

		protected void OnDestroy()
		{
			Ball.OnBallGenerate -= Ball_OnBallGenerate;
		}

	}
}



