using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace _Scripts
{

	public enum GameMode
	{
		Zen,
		Classic
	}
	
	[Serializable]
	public class GameSettings
	{
		[SerializeField]private GameMode gameMode;
		public GameMode GameMode => gameMode;

		[SerializeField][Min(1)] [MaxValue(81)] private int blankCells;
		public int BlankCells => blankCells;

		[SerializeField]private int maxErrors;
		public int MaxErrors => maxErrors;

		public GameSettings(GameMode mode, int cells, int errors)
		{
			gameMode = mode;
			blankCells = cells;
			maxErrors = errors;
		}
	}

}