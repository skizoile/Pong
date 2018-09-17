///-----------------------------------------------------------------
///   Author : Sebastien Raymondaud                    
///   Date   : 06/07/2018 18:35
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fr.Raymondaud.Sebastien.Pong.Objects {
	public enum PositionGoal
	{
		Left,
		Right
	}
	[RequireComponent(typeof(AudioSource))]
	public class Goal : MonoBehaviour {



		[SerializeField] protected PositionGoal positionGoal;
		protected AudioSource _audioSource;
		public static event Action<PositionGoal> OnGoal;

		protected void Start()
		{
			_audioSource = GetComponent<AudioSource>();

		}
		protected void OnTriggerEnter2D(Collider2D collision)
		{
			Ball target = collision.GetComponent<Ball>();
			if (!target)
				return;

			if (target.ParticleExplodePrefab)
			{
				ParticleSystem lPart = Instantiate(target.ParticleExplodePrefab, target.transform.localPosition, Quaternion.identity);
				lPart.transform.SetParent(target.transform.parent, false);
				Destroy(lPart.gameObject, lPart.main.duration);
			}
			_audioSource.PlayOneShot(target.AudioExplode);
			Destroy(target.gameObject);

			if (OnGoal != null)
				OnGoal(positionGoal);
		}
	}
}



