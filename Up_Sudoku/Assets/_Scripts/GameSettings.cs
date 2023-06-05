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
		public GameMode GameMode
		{
			get => gameMode;
			set => gameMode = value;
		}

		[SerializeField][Min(1)] [MaxValue(81)] private int blankCells;
		public int BlankCells
		{
			get => blankCells;
			set => blankCells = value;
		}

		[SerializeField]private int maxErrors;
		public int MaxErrors
		{
			get => maxErrors;
			set => maxErrors = value;
		}

		[SerializeField][Min(0)] [MaxValue(81)] private int maxHints;
		public int MaxHints
		{
			get => maxHints;
			set => maxHints = value;
		}

		public GameSettings(GameMode mode, int cells, int errors, int hints)
		{
			gameMode = mode;
			blankCells = cells;
			maxErrors = errors;
			maxHints = hints;
		}
	}

}