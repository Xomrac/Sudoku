using System;
using UnityEngine;
using XomracUtilities.Patterns;

namespace GameStats
{

	public class GameStatsManager : ServiceLocator
	{
		#region LifeCycle
 
		
		private void Awake()
		{
			PopulateDictionary();
		}

		#endregion
		
		
		#region Overrides

		public override void PopulateDictionary()
		{
			services.Add(typeof(ErrorsTracker),GetComponent<ErrorsTracker>());
			services.Add(typeof(ScoreTracker),GetComponent<ScoreTracker>());
		}

		#endregion
	}

}