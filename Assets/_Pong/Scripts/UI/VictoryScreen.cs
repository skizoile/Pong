///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 20:20
///-----------------------------------------------------------------

using Fr.Raymondaud.Sebastien.Pong.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fr.Raymondaud.Sebastien.Pong.UI {
    public class VictoryScreen : MonoBehaviour {
        private static VictoryScreen _instance;
        public static VictoryScreen Instance { get { return _instance; } }

		[SerializeField] protected Button restartButton;
		[SerializeField] protected Button homeButton;
		[SerializeField] protected Button quitButton;
		[SerializeField] protected RectTransform victoryContainer;

		public event Action OnRestart;
		public event Action OnHome;

        private void Awake(){
            if (_instance){
                Destroy(gameObject);
                return;
            }

            _instance = this;
			OnHome += HomeScreen.Instance.VictoryScreen_OnHome;
        }

        private void Start () {
			restartButton.onClick.AddListener(RestartButton_OnClick);
			homeButton.onClick.AddListener(HomeButton_OnClick);
			quitButton.onClick.AddListener(QuitButton_OnClick); 
        }

		public void SetWinner(float pSide)
		{
			if (pSide < 0)
			{
				victoryContainer.localPosition = new Vector3(-400, 0, 0);
			}
			else if (pSide > 0)
			{
				victoryContainer.localPosition = new Vector3(0, 0, 0);
			}
		}

		private void RestartButton_OnClick()
		{
			if (OnRestart != null)
				OnRestart();
			OnHome -= HomeScreen.Instance.VictoryScreen_OnHome;
			Destroy(gameObject);
		}

		private void HomeButton_OnClick()
		{
			if (OnHome != null)
				OnHome();
			HomeScreen.Instance.gameObject.SetActive(true);
			GameManager.Instance.OnIncreaseScorePlayerLeft -= HUD.Instance.Instance_OnIncreaseScorePlayerLeft;
			GameManager.Instance.OnIncreaseScorePlayerRight -= HUD.Instance.Instance_OnIncreaseScorePlayerRight;
			Destroy(HUD.Instance.gameObject);
			OnHome -= HomeScreen.Instance.VictoryScreen_OnHome;
			Destroy(gameObject);
		}

		private void QuitButton_OnClick()
		{
			Application.Quit();
		}

		private void OnDestroy(){
            if (this == _instance)
                _instance = null;
		}
    }
}



