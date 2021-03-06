///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 21:49
///-----------------------------------------------------------------

using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Managers {
    public class VersusHardManager : VersusManager {
		[SerializeField, Range(1.1f, 2f)] protected float multiplyDifficulty = 1.5f;
		[SerializeField] protected GameObject[] levelDesigns;

		protected GameObject _levelDesign;

		protected override void Start()
		{
			_levelDesign = Instantiate(levelDesigns[UnityEngine.Random.Range(0, levelDesigns.Length - 1)]);
			base.Start();
			_ball.Speed *= multiplyDifficulty;
		}

		protected override void ResetGame()
		{
			base.ResetGame();
			_ball.Speed *= multiplyDifficulty;
		}

		override protected void VictoryScreen_OnHome()
		{
			base.VictoryScreen_OnHome();
			if (_levelDesign)
				Destroy(_levelDesign);
		}
	}
}



