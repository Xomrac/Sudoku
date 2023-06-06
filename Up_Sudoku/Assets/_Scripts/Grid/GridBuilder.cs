using System;
using System.Diagnostics;
using System.Linq;
using _Scripts.Grid;
using UnityEngine;
using XomracUtilities.Patterns;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GridBuilder : SerializedServiceComponent<GridManager>
{

	#region Fields

	[SerializeField] private RectTransform gridParent; 
	public RectTransform GridParent => gridParent;
	
	public static event Action GridReady;
	
	private Stopwatch creationStopWatch;
	
	#endregion

	#region LifeCycle

	private void OnEnable()
	{
		GameManager.GameStarted += CreateGrid;
	}

	private void OnDisable()
	{
		GameManager.GameStarted -= CreateGrid;
	}

	#endregion

	#region GridGeneration

	void FillBox(int row, int col)
	{
		int num = Random.Range(0, 10);
		for (int i = 0; i < ServiceLocator.GetGridDimensions.squaresDimension; i++)
		{
			for (int j = 0; j < ServiceLocator.GetGridDimensions.squaresDimension; j++)
			{
				while (!ServiceLocator.GetService<GridChecker>().UnUsedInBox(row, col, num))
				{
					num = Random.Range(0, 10);
				}
				ServiceLocator.cells[row + i, col + j].SetInitialValue(num);
			}
		}
	}

	void FillDiagonal()
	{
		for (int i = 0; i < ServiceLocator.GetGridDimensions.gridDimension; i += ServiceLocator.GetGridDimensions.squaresDimension)
		{
			FillBox(i, i);
		}
	}

	bool FillRemaining(int row, int col)
	{
		//  Move to the next row if we have reached the end of the current row
		if (col >= ServiceLocator.GetGridDimensions.gridDimension && row < ServiceLocator.GetGridDimensions.gridDimension - 1)
		{
			row += 1;
			col = 0;
		}

		// Check if we have reached the end of the matrix
		if (row == ServiceLocator.GetGridDimensions.gridDimension - 1 && col >= ServiceLocator.GetGridDimensions.gridDimension)
		{
			return true;
		}

		if (ServiceLocator.cells[row, col].CorrectValue != 0)
		{
			return FillRemaining(row, col + 1);
		}

		//Try filling the current cell with a valid value
		for (int num = 1; num <= ServiceLocator.GetGridDimensions.gridDimension; num++)
		{
			if (ServiceLocator.GetService<GridChecker>().CheckIfSafe(row, col, num))
			{
				ServiceLocator.cells[row, col].SetInitialValue(num);
				if (FillRemaining(row, col + 1))
				{
					return true;
				}
				ServiceLocator.cells[row, col].SetInitialValue(0);
			}
		}
		// No valid value was found, so backtrack
		return false;
	}

	#endregion

	#region PuzzleGeneration

	//Right now cells are randomly erased but maybe in the future will be another more complex way
	private void EraseRandomCells(int amount)
	{
		var availableCells = ServiceLocator.cells.Cast<CellController>().ToList();
		for (int i = 0; i < amount; i++)
		{
			var randomIndex = Random.Range(0, availableCells.Count);
			ServiceLocator.UncompletedCells.Add(availableCells[randomIndex]);
			availableCells[randomIndex].EraseValue();
			availableCells.RemoveAt(randomIndex);
		}
	}

	#endregion

	#region Methods

	public void CreateGrid()
	{
		creationStopWatch = new Stopwatch();
		creationStopWatch.Start();
		FillDiagonal();
		FillRemaining(0, ServiceLocator.GetGridDimensions.squaresDimension);
		EraseRandomCells(GameManager.Instance.gameSettings.BlankCells);
		Debug.Log($"Grid created in {creationStopWatch.ElapsedMilliseconds}ms");
		GridReady?.Invoke();
		creationStopWatch.Stop();
	}

	#endregion


	

}