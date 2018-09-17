///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 21:19
///-----------------------------------------------------------------

using Fr.Raymondaud.Sebastien.Pong.Objects;
using Fr.Raymondaud.Sebastien.Pong.UI;
using System;
using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Managers {
    public class GameManager : MonoBehaviour {
        private static GameManager _instance;
        public static GameManager Instance { get { return _instance; } }

		[SerializeField] protected Paddle paddlePlayerPrefab;
		[SerializeField] protected Ball ballPrefab;
		[SerializeField] protected VictoryScreen victoryScreen;
		[SerializeField] protected HUD hud;
		[SerializeField] protected int maxScore = 10;

		protected Paddle _paddleJ1;
		protected Ball _ball;

		protected int _scorePlayerLeft = 0;
		protected int _scorePlayerRight = 0;

		protected CurrentDirectionX _nextDirectionBall = CurrentDirectionX.Right;

		public event Action<int> OnIncreaseScorePlayerLeft;
		public event Action<int> OnIncreaseScorePlayerRight;

		virtual protected void Awake()
		{
			if (_instance)
			{
				Destroy(gameObject);
				return;
			}

			_instance = this;
			Goal.OnGoal += Goal_OnGoal;
			Instantiate(hud);

		}

		virtual protected void Start()
		{
			_paddleJ1 = Instantiate(paddlePlayerPrefab);
			_paddleJ1.SetControls(Controls.Player1);
			ResetGame();
		}

		virtual protected void VictoryScreen_OnRestart()
		{
			VictoryScreen.Instance.OnRestart -= VictoryScreen_OnRestart;
			VictoryScreen.Instance.OnRestart -= _paddleJ1.Instance_OnRestart;
			_scorePlayerLeft = 0;
			if (OnIncreaseScorePlayerLeft != null)
				OnIncreaseScorePlayerLeft(_scorePlayerLeft);
			_scorePlayerRight = 0;
			if (OnIncreaseScorePlayerRight != null)
				OnIncreaseScorePlayerRight(_scorePlayerRight);
			ResetGame();
		}

		virtual protected void Goal_OnGoal(PositionGoal pPositionGoal)
		{
			if (pPositionGoal == PositionGoal.Right)
			{
				_nextDirectionBall = CurrentDirectionX.Left;
				_scorePlayerLeft++;
				CheckScore(_scorePlayerLeft);
				if (OnIncreaseScorePlayerLeft != null)
					OnIncreaseScorePlayerLeft(_scorePlayerLeft);
			}
			else if (pPositionGoal == PositionGoal.Left)
			{
				_nextDirectionBall = CurrentDirectionX.Right;
				_scorePlayerRight++;
				CheckScore(_scorePlayerRight);
				if (OnIncreaseScorePlayerRight != null)
					OnIncreaseScorePlayerRight(_scorePlayerRight);
			}
		}

		virtual protected void ResetGame()
		{
			if (_ball)
				Destroy(_ball.gameObject);

			_ball = Instantiate(ballPrefab);
			_ball.SetMoveDirection(_nextDirectionBall);
		}

		virtual protected void CheckScore(int pScore)
		{
			if (pScore >= maxScore)
			{
				Instantiate(victoryScreen);
				VictoryScreen.Instance.SetWinner((_scorePlayerLeft > _scorePlayerRight) ? -1 : 1);
				VictoryScreen.Instance.OnRestart += VictoryScreen_OnRestart;
				VictoryScreen.Instance.OnHome += VictoryScreen_OnHome;
				VictoryScreen.Instance.OnRestart += _paddleJ1.Instance_OnRestart;
			}
			else
				ResetGame();
		}

		virtual protected void VictoryScreen_OnHome()
		{
			if (_paddleJ1)
				Destroy(_paddleJ1.gameObject);
		}

		virtual protected void OnDestroy()
		{
			if (this == _instance)
			{
				_instance = null;
				Goal.OnGoal -= Goal_OnGoal;
			}
		}
	}
}



