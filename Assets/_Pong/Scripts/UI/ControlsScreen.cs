///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 07/07/2018 00:26
///-----------------------------------------------------------------

using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fr.Raymondaud.Sebastien.Pong.UI {
    public class ControlsScreen : MonoBehaviour {
        private static ControlsScreen _instance;
        public static ControlsScreen Instance { get { return _instance; } }

		[SerializeField] protected Button backButton;

        private void Awake(){
            if (_instance){
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Start () {
			backButton.onClick.AddListener(BackButton_OnClick);
        }

		private void BackButton_OnClick()
		{
			HomeScreen.Instance.gameObject.SetActive(true);
			Destroy(gameObject);
		}

		private void OnDestroy(){
            if (this == _instance)
                _instance = null;
        }
    }
}



