///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 22:29
///-----------------------------------------------------------------

using Fr.Raymondaud.Sebastien.Pong.UI;
using Fr.Raymondaud.Sebastien.Pong.Managers;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Fr.Raymondaud.Sebastien.Pong.UI {
    public class HomeScreen : MonoBehaviour {
        private static HomeScreen _instance;
        public static HomeScreen Instance { get { return _instance; } }

		[SerializeField] protected IAGameManager iaGameManager;
		[SerializeField] protected VersusManager versusManager;
		[SerializeField] protected HardGameManagerIA hardGameManager;
		[SerializeField] protected VersusHardManager versusHardManager;
		[SerializeField] protected ControlsScreen controlsScreen;

		[SerializeField] protected Button buttonPlayIAGame;
		[SerializeField] protected Button buttonPlayVersus;
		[SerializeField] protected Button buttonPlayIAGameHard;
		[SerializeField] protected Button buttonPlayVersusHard;
		[SerializeField] protected Button buttonControls;
		[SerializeField] protected Button buttonQuit;

		protected IAGameManager _iaGameManager;
		protected VersusManager _versusManager;
		protected HardGameManagerIA _hardGameManager;
		protected VersusHardManager _versusHardManager;
		protected ControlsScreen _controlsScreen;

        private void Awake(){
            if (_instance){
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        private void Start () {
			buttonPlayIAGame.onClick.AddListener(PlayIAGame_OnClick);
			buttonPlayVersus.onClick.AddListener(PlayVersus_OnClick);
			buttonPlayIAGameHard.onClick.AddListener(PlayIAGameHard_OnClick);
			buttonPlayVersusHard.onClick.AddListener(PlayVersusHard_OnClick);
			buttonControls.onClick.AddListener(Controls_OnClick);
			buttonQuit.onClick.AddListener(Quit_OnClick);

		}

		public void VictoryScreen_OnHome()
		{
			if (_iaGameManager)
				Destroy(_iaGameManager.gameObject);
			if (_versusManager)
				Destroy(_versusManager.gameObject);
			if (_hardGameManager)
				Destroy(_hardGameManager.gameObject);
			if (_versusHardManager)
				Destroy(_versusHardManager.gameObject);
		}

		private void PlayIAGame_OnClick()
		{
			_iaGameManager = Instantiate(iaGameManager);
			gameObject.SetActive(false);
		}

		private void PlayVersus_OnClick()
		{
			_versusManager = Instantiate(versusManager);
			gameObject.SetActive(false);
		}

		private void PlayIAGameHard_OnClick()
		{
			_hardGameManager = Instantiate(hardGameManager);
			gameObject.SetActive(false);
		}

		private void PlayVersusHard_OnClick()
		{
			_versusHardManager = Instantiate(versusHardManager);
			gameObject.SetActive(false);
		}

		private void Controls_OnClick()
		{
			_controlsScreen = Instantiate(controlsScreen);
			gameObject.SetActive(false);
		}

		private void Quit_OnClick()
		{
			Application.Quit();
		}

		private void OnDestroy(){
            if (this == _instance)
                _instance = null;
        }
    }
}



