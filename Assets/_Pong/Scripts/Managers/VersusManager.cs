///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 21:19
///-----------------------------------------------------------------
using Fr.Raymondaud.Sebastien.Pong.Objects;
using Fr.Raymondaud.Sebastien.Pong.UI;
using System;
using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Managers {
    public class VersusManager : GameManager {

		[SerializeField] protected Paddle paddleJ2Prefab;

		protected Paddle _paddleJ2;

		override protected void Start()
		{
			_paddleJ2 = Instantiate(paddleJ2Prefab);
			_paddleJ2.SetControls(Controls.Player2);
			base.Start();
		}

		protected override void CheckScore(int pScore)
		{
			base.CheckScore(pScore);
			if (pScore >= maxScore)
				VictoryScreen.Instance.OnRestart += _paddleJ2.Instance_OnRestart;
		}

		protected override void VictoryScreen_OnRestart()
		{
			VictoryScreen.Instance.OnRestart -= _paddleJ2.Instance_OnRestart;
			base.VictoryScreen_OnRestart();
		}

		override protected void VictoryScreen_OnHome()
		{
			base.VictoryScreen_OnHome();
			if (_paddleJ2)
				Destroy(_paddleJ2.gameObject);
		}
	}
}



