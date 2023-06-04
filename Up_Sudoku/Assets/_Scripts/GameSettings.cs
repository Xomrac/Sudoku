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

		[SerializeField][Min(0)] [MaxValue(81)] private int maxHints;
		public int MaxHints => maxHints;

		public GameSettings(GameMode mode, int cells, int errors, int hints)
		{
			gameMode = mode;
			blankCells = cells;
			maxErrors = errors;
			maxHints = hints;
		}
	}

}