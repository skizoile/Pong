///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 07/07/2018 00:49
///-----------------------------------------------------------------

using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Objects {
    public class Paddles : MonoBehaviour {

		[SerializeField] protected float _speed = 15f;
		[SerializeField] protected float margeY = 3.5f;
		protected Camera _camera;

		public float Speed
		{
			get { return _speed; }
			set { _speed = value; }
		}

		private void Start () {
			_camera = Camera.main;
		}

        private void Update () {
            
        }
    }
}



