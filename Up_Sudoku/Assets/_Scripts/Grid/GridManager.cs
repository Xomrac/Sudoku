using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;
using XomracUtilities.Extensions;
using XomracUtilities.Patterns;
using Debug = UnityEngine.Debug;
using Random = UnityEngine.Random;

public class GridManager : SerializedMonoBehaviour
{

	#region Fields

	private const int GRID_DIMENSION = 9;
	private const int SQUARES_DIMENSION = 3;

	public int numbersToRemove = 30;

	[SerializeField] private RectTransform gridParent;

	[SerializeField] [HideInInspector] private CellController[,] cells;

	private Stopwatch creationStopWatch;

	#endregion

	#region Fetching

	[Button]
	private void FetchCells()
	{
		cells = new CellController[GRID_DIMENSION, GRID_DIMENSION];
		var controllers = gridParent.GetComponentsInChildren<CellController>();
		for (int i = 0; i < controllers.Length; i++)
		{
			int row = Mathf.FloorToInt(i / GRID_DIMENSION);
			int col = i % GRID_DIMENSION;
			int square = Mathf.FloorToInt(row / SQUARES_DIMENSION) + Mathf.FloorToInt(col / SQUARES_DIMENSION) + (2 * row / SQUARES_DIMENSION);

			CellController currentController = controllers[i];
			currentController.Init(new CellNode(i, row, col, square));
			cells[row, col] = currentController;
		}
	}

	#endregion

	#region Checks

	bool CheckIfSafe(int i, int j, int num)
	{
		return (UnUsedInRow(i, num) &&
		        UnUsedInCol(j, num) &&
		        UnUsedInBox(i - i % SQUARES_DIMENSION, j - j % SQUARES_DIMENSION, num));
	}

	bool UnUsedInBox(int rowStart, int colStart, int num)
	{
		for (int i = 0; i < SQUARES_DIMENSION; i++)
		{
			for (int j = 0; j < SQUARES_DIMENSION; j++)
			{
				if (cells[rowStart + i, colStart + j].CorrectValue == num)
				{
					return false;
				}
			}
		}
		return true;
	}

	bool UnUsedInRow(int i, int num)
	{
		for (int j = 0; j < GRID_DIMENSION; j++)
		{
			if (cells[i, j].CorrectValue == num)
			{
				return false;
			}
		}
		return true;
	}

	bool UnUsedInCol(int j, int num)
	{
		for (int i = 0; i < GRID_DIMENSION; i++)
		{
			if (cells[i, j].CorrectValue == num)
			{
				return false;
			}
		}
		return true;
	}

	#endregion

	#region Filling

	void FillBox(int row, int col)
	{
		int num = Random.Range(0, 10);
		for (int i = 0; i < SQUARES_DIMENSION; i++)
		{
			for (int j = 0; j < SQUARES_DIMENSION; j++)
			{
				while (!UnUsedInBox(row, col, num))
				{
					num = Random.Range(0, 10);
				}
				cells[row + i, col + j].SetInitialValue(num);
			}
		}
	}

	void FillDiagonal()
	{
		for (int i = 0; i < GRID_DIMENSION; i += SQUARES_DIMENSION)
		{
			FillBox(i, i);
		}
	}

	bool FillRemaining(int row, int col)
	{
		//  Move to the next row if we have reached the end of the current row
		if (col >= GRID_DIMENSION && row < GRID_DIMENSION - 1)
		{
			row += 1;
			col = 0;
		}

		// Check if we have reached the end of the matrix
		if (row == GRID_DIMENSION - 1 && col >= GRID_DIMENSION)
		{
			return true;
		}

		if (cells[row, col].CorrectValue != 0)
		{
			return FillRemaining(row, col + 1);
		}

		//Try filling the current cell with a valid value
		for (int num = 1; num <= GRID_DIMENSION; num++)
		{
			if (CheckIfSafe(row, col, num))
			{
				cells[row, col].SetInitialValue(num);
				if (FillRemaining(row, col + 1))
				{
					return true;
				}
				cells[row, col].SetInitialValue(0);
			}
		}
		// No valid value was found, so backtrack
		return false;
	}

	#endregion

	#region Methods

	private void Start()
	{
		CreateGrid();
	}

	[Button]
	public void CreateGrid()
	{
		ResetCells();
		creationStopWatch = new Stopwatch();
		creationStopWatch.Start();
		PopulateCells();
		EraseCells(numbersToRemove);
		Debug.Log($"Grid created in {creationStopWatch.ElapsedMilliseconds}ms");
		creationStopWatch.Stop();
	}

	private void PopulateCells()
	{
		FillDiagonal();
		FillRemaining(0, SQUARES_DIMENSION);
	}

	private void ResetCells()
	{
		foreach (CellController cell in cells)
		{
			cell.SetInitialValue(0);
			cell.EraseValue();
		}
	}

	private void EraseCells(int amount)
	{
		var availableCells = cells.Flatten().ToList();
		for (int i = 0; i < amount; i++)
		{
			var randomIndex = Random.Range(0, availableCells.Count);
			availableCells[randomIndex].CurrentValue = null;
			availableCells.RemoveAt(randomIndex);
		}
	}

	#endregion

}