///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 21:06
///-----------------------------------------------------------------

using Fr.Raymondaud.Sebastien.Pong.Managers;
using UnityEngine;
using UnityEngine.UI;

namespace Fr.Raymondaud.Sebastien.Pong.UI {
    public class HUD : MonoBehaviour {
        private static HUD _instance;
        public static HUD Instance { get { return _instance; } }

		[SerializeField] protected Text scorePlayerLeft;
		[SerializeField] protected Text scorePlayerRight;

        private void Awake(){
            if (_instance){
                Destroy(gameObject);
                return;
            }

            _instance = this;
			GameManager.Instance.OnIncreaseScorePlayerLeft += Instance_OnIncreaseScorePlayerLeft;
			GameManager.Instance.OnIncreaseScorePlayerRight += Instance_OnIncreaseScorePlayerRight;
        }

		public void Instance_OnIncreaseScorePlayerRight(int pScore)
		{
			scorePlayerRight.text = pScore.ToString();
		}

		public void Instance_OnIncreaseScorePlayerLeft(int pScore)
		{
			scorePlayerLeft.text = pScore.ToString();
		}

		private void OnDestroy(){
            if (this == _instance)
                _instance = null;

        }
    }
}



