///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 12:51
///-----------------------------------------------------------------

using Fr.Raymondaud.Sebastien.Pong.UI;
using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Objects {
	public enum Controls
	{
		Player1,
		Player2
	}

	[RequireComponent(typeof(BoxCollider2D))]
	public class Paddle : Paddles {

		[SerializeField] protected Vector2 startPosJ1= new Vector2(-8, 0);
		[SerializeField] protected Vector2 startPosJ2 = new Vector2(8, 0);
		protected BoxCollider2D _boxCollid;
		protected string controlsAxis;
		[SerializeField] protected string axisPlayer1 = "Vertical_1";
		[SerializeField] protected string axisPlayer2 = "Vertical_2";
		protected Controls control;
		protected int idTouch;

		public void Instance_OnRestart()
		{
			if (control == Controls.Player1)
				transform.localPosition = startPosJ1;
			else if (control == Controls.Player2)
				transform.localPosition = startPosJ2;
		}

		private void Start () {
			_boxCollid = GetComponent<BoxCollider2D>();
		}

		public void SetControls(Controls pControls)
		{
			control = pControls;
			if (control == Controls.Player1)
			{
#if UNITY_ANDROID || UNITY_IOS
				idTouch = 0;
#endif
				controlsAxis = axisPlayer1;
				transform.localPosition = startPosJ1;
			}
			else if (control == Controls.Player2)
			{
#if UNITY_ANDROID || UNITY_IOS
				idTouch = 1;
#endif
				controlsAxis = axisPlayer2;
				transform.localPosition = startPosJ2;
			}
		}

		private void Update () {
			Vector2 pos = transform.position;
#if UNITY_ANDROID || UNITY_IOS
			if (Input.GetTouch(idTouch).phase == TouchPhase.Moved)
			{
				Vector2 touchDeltaPosition = Input.GetTouch(idTouch).deltaPosition;
				pos.y += ((_speed / 8) * touchDeltaPosition.y * Time.deltaTime);
			}
#elif UNITY_EDITOR || UNITY_STANDALONE
			pos.y += (_speed * Input.GetAxis(controlsAxis) * Time.deltaTime);
#endif
			pos.y = Mathf.Clamp(pos.y, -margeY, margeY);
			transform.localPosition = pos;
		}
	}
}



