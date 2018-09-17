///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 12:46
///-----------------------------------------------------------------

using System;
using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Objects {
	public enum CurrentDirectionX
	{
		Left,
		Right
	}
	[RequireComponent(typeof(BoxCollider2D), typeof(AudioSource))]
	public class Ball : MonoBehaviour {

		protected enum StartDirection
		{
			Top,
			Left,
			Right,
			Bottom
		}

		protected enum CurrentDirectionY
		{
			Top,
			Bottom
		}

		protected BoxCollider2D _boxCollid;
		[SerializeField] protected float speed = 15;
		[SerializeField] protected ParticleSystem particleHurtPrefab;
		[SerializeField] protected ParticleSystem particleExplodePrefab;
		[SerializeField] protected AudioClip audioHurt;
		[SerializeField] protected AudioClip audioExplode;
		protected Vector2 _speedVelocity = new Vector2();
		protected Camera _camera;
		protected float _halfSize;
		protected Vector2 _velocity = new Vector2();
		protected float _verticalSizeCamera;
		protected float _horizontalSizeCamera;
		protected Vector3 _startPosition = new Vector3();
		private CurrentDirectionX _currentDirectionX;
		protected CurrentDirectionY _currentDirectionY;

		protected AudioSource _audioSource;

		public ParticleSystem ParticleExplodePrefab
		{
			get{ return particleExplodePrefab;}
		}

		public CurrentDirectionX CurrentDirection_X
		{
			get{ return _currentDirectionX; }
			set{ _currentDirectionX = value; }
		}

		public float Speed
		{
			get { return speed; }
			set { speed = value; }
		}

		public AudioClip AudioExplode
		{
			get { return audioExplode; }
			set { audioExplode = value; }
		}

		public static event Action<Ball> OnBallGenerate;

		protected void Start () {
			_boxCollid = GetComponent<BoxCollider2D>();
			_audioSource = GetComponent<AudioSource>();
			_camera = Camera.main;

			_verticalSizeCamera = _camera.orthographicSize * 2;
			_horizontalSizeCamera = _verticalSizeCamera * Screen.width / Screen.height;
			_halfSize = _boxCollid.bounds.size.x / 2;
			transform.position = _startPosition;

			if (OnBallGenerate != null)
				OnBallGenerate(this);
		}

		public void SetMoveDirection(CurrentDirectionX pCurrentDirection)
		{
			_currentDirectionX = pCurrentDirection;
			_speedVelocity = new Vector2((pCurrentDirection == CurrentDirectionX.Right) ? speed : -speed, 0);
		}

		private void Update () {
			_velocity = transform.localPosition;
			if (LimitBottom() || LimitTop())
			{
				_speedVelocity.y *= -1;
				_currentDirectionY = (_speedVelocity.y < 0) ? CurrentDirectionY.Bottom : CurrentDirectionY.Top;
				CreateParticle();
			}

			_velocity.y += _speedVelocity.y * Time.deltaTime;

			if (LimitLeft() || LimitRight())
			{
				_speedVelocity.x *= -1;
				_currentDirectionX = (_speedVelocity.x < 0) ? CurrentDirectionX.Right : CurrentDirectionX.Left;
				CreateParticle();
			}

			_velocity.x += _speedVelocity.x * Time.deltaTime;
			transform.localPosition = _velocity;
		}

		protected void OnTriggerEnter2D(Collider2D collision)
		{
			if (!collision.CompareTag("Player") && !collision.CompareTag("Obstacles"))
				return;

			_speedVelocity.x *= -1;
			_currentDirectionX = (_speedVelocity.x < 0) ? CurrentDirectionX.Right : CurrentDirectionX.Left;

			float lPosY = HitPaddlePos(transform.localPosition, collision.transform.localPosition, collision.bounds.size.y);
			if (lPosY > 0.1f)
			{
				_speedVelocity.y = UnityEngine.Random.Range(1, 5);
				_currentDirectionY = CurrentDirectionY.Top;
			}
			else if (lPosY < 0.1f)
			{
				_speedVelocity.y = UnityEngine.Random.Range(-5, -1);
				_currentDirectionY = CurrentDirectionY.Bottom;
			}
			CreateParticle();
		}

		protected float HitPaddlePos(Vector2 pBallPos, Vector2 pPaddlePos, float pPaddleHeight)
		{
			return (pBallPos.y - pPaddlePos.y) / pPaddleHeight;
		}

		protected bool LimitTop()
		{
			if (_currentDirectionY == CurrentDirectionY.Top && (transform.localPosition.y + _halfSize) > _camera.transform.localPosition.y + (_verticalSizeCamera / 2))
			{
				_currentDirectionY = CurrentDirectionY.Bottom;
				return true;
			}
			else
			return false;
		}

		protected bool LimitBottom()
		{
			if (_currentDirectionY == CurrentDirectionY.Bottom && (transform.localPosition.y - _halfSize) < _camera.transform.localPosition.y - (_verticalSizeCamera / 2))
			{
				_currentDirectionY = CurrentDirectionY.Top;
				return true;
			}
			else
				return false;
		}

		protected bool LimitLeft()
		{
			if (_currentDirectionX == CurrentDirectionX.Left && (transform.localPosition.x - _halfSize) > _camera.transform.localPosition.x + (_horizontalSizeCamera / 2))
			{
				_currentDirectionX = CurrentDirectionX.Right;
				return true;
			}
			else
				return false;
		}

		protected bool LimitRight()
		{
			if (_currentDirectionX == CurrentDirectionX.Right && (transform.localPosition.x + _halfSize) < _camera.transform.localPosition.x - (_horizontalSizeCamera / 2))
			{
				_currentDirectionX = CurrentDirectionX.Left;
				return true;
			}
			else
				return false;
		}

		protected void CreateParticle()
		{
			if (audioHurt)
				_audioSource.PlayOneShot(audioHurt);
			if (!particleHurtPrefab)
				return;

			ParticleSystem lPart = Instantiate(particleHurtPrefab, transform.localPosition, Quaternion.identity);
			lPart.transform.SetParent(transform.parent, false);
			Destroy(lPart.gameObject, lPart.main.duration);
		}

		public void PlayExplodeSound()
		{
			if (audioExplode)
				_audioSource.PlayOneShot(audioExplode);
		}

	}
}



