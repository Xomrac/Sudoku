using System;
using System.Collections.Generic;
using UnityEngine;
using XomracUtilities.Patterns;

public class GridChecker : SerializedServiceComponent<GridManager>
{

	#region Fields
	
	
	private Dictionary<int, List<CellController>> rows;
	public Dictionary<int, List<CellController>> Rows => rows;

	private Dictionary<int, List<CellController>> columns;
	public Dictionary<int, List<CellController>> Columns => columns;

	private Dictionary<int, List<CellController>> squares;
	public Dictionary<int, List<CellController>> Squares => squares;

	public static event Action GameWon;
	
	private bool canCheck = false;
	
	#endregion

	#region LifeCycle

	private void OnEnable()
	{
		CellController.CellUpdated += CheckForCompletition;
	}
	
	private void OnDisable()
	{
		CellController.CellUpdated -= CheckForCompletition;
	}

	#endregion

	#region ChecksMethods
	
	public bool UnUsedInBox(int rowStart, int colStart, int num)
	{
		for (int i = 0; i < ServiceLocator.GetGridDimensions.squaresDimension; i++)
		{
			for (int j = 0; j < ServiceLocator.GetGridDimensions.squaresDimension; j++)
			{
				if (ServiceLocator.cells[rowStart + i, colStart + j].CorrectValue == num)
				{
					return false;
				}
			}
		}
		return true;
	}

	bool UnUsedInRow(int i, int num)
	{
		for (int j = 0; j < ServiceLocator.GetGridDimensions.gridDimension; j++)
		{
			if (ServiceLocator.cells[i, j].CorrectValue == num)
			{
				return false;
			}
		}
		return true;
	}

	bool UnUsedInCol(int j, int num)
	{
		for (int i = 0; i < ServiceLocator.GetGridDimensions.gridDimension; i++)
		{
			if (ServiceLocator.cells[i, j].CorrectValue == num)
			{
				return false;
			}
		}
		return true;
	}
	
	public bool CheckIfSafe(int i, int j, int num)
	{
		return (UnUsedInRow(i, num) &&
		        UnUsedInCol(j, num) &&
		        UnUsedInBox(i - i % ServiceLocator.GetGridDimensions.squaresDimension, j - j % ServiceLocator.GetGridDimensions.squaresDimension, num));
	}

	
	private void CheckForCompletition(CellController cell)
	{
		if (!canCheck) return;
		
		if (!ServiceLocator.UncompletedCells.Contains(cell) && !cell.CurrentlyCompleted)
		{
			ServiceLocator.UncompletedCells.Add(cell);
		}
		if (ServiceLocator.UncompletedCells.Contains(cell) && cell.CurrentlyCompleted)
		{
			ServiceLocator.UncompletedCells.Remove(cell);
		}
		if (ServiceLocator.UncompletedCells.Count==0)
		{
			GameWon?.Invoke();
		}
	}

	#endregion

	#region Methods

	public void SetDictionaries(Dictionary<int, List<CellController>> r, Dictionary<int, List<CellController>> cols, Dictionary<int, List<CellController>> s)
	{
		rows = r;
		columns = cols;
		squares = s;
	}

	#endregion
}