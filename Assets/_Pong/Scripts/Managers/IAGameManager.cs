///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 19:03
///-----------------------------------------------------------------

using Fr.Raymondaud.Sebastien.Pong.Objects;
using Fr.Raymondaud.Sebastien.Pong.UI;
using System;
using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Managers {
    public class IAGameManager : GameManager {

		[SerializeField] protected PaddleIA paddleIAPrefab;

		protected PaddleIA _paddleIA;

		override protected void Start () {
			_paddleIA = Instantiate(paddleIAPrefab);
			base.Start();
		}

		protected override void VictoryScreen_OnRestart()
		{
			VictoryScreen.Instance.OnRestart -= _paddleIA.Instance_OnRestart;
			base.VictoryScreen_OnRestart();

		}
		protected override void CheckScore(int pScore)
		{
			base.CheckScore(pScore);
			if (pScore >= maxScore)
				VictoryScreen.Instance.OnRestart += _paddleIA.Instance_OnRestart;
		}

		override protected void VictoryScreen_OnHome()
		{
			base.VictoryScreen_OnHome();
			if (_paddleIA)
				Destroy(_paddleIA.gameObject);
		}
    }
}



