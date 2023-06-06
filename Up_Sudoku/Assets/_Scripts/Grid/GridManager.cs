using System.Collections.Generic;
using _Scripts.Grid;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using XomracUtilities.Patterns;
using Random = UnityEngine.Random;

public class GridManager : SerializedServiceLocator
{

	#region Fields

	private const int GRID_DIMENSION = 9;
	private const int SQUARES_DIMENSION = 3;
	
	[SerializeField] [HideInInspector]public CellController[,] cells;

	[SerializeField] [ReadOnly] private List<CellController> uncompletedCells;
	public List<CellController> UncompletedCells => uncompletedCells;

	#endregion

	#region EDITOR_CellsFetching

#if UNITY_EDITOR

	[Button]
	private void FetchCells()
	{
		var gridBuilder = GetComponent<GridBuilder>();
		var gridChecker = GetComponent<GridChecker>();
		var rows = new Dictionary<int, List<CellController>>();
		var columns = new Dictionary<int, List<CellController>>();
		var squares = new Dictionary<int, List<CellController>>();
		for (int i = 0; i < GRID_DIMENSION; i++)
		{
			rows.Add(i, new List<CellController>());
			columns.Add(i, new List<CellController>());
			squares.Add(i, new List<CellController>());
		}
		cells = new CellController[GRID_DIMENSION, GRID_DIMENSION];
		var controllers = gridBuilder.GridParent.GetComponentsInChildren<CellController>();
		for (int i = 0; i < controllers.Length; i++)
		{
			int row = Mathf.FloorToInt(i / GRID_DIMENSION);
			int col = i % GRID_DIMENSION;
			int square = Mathf.FloorToInt(row / SQUARES_DIMENSION) + Mathf.FloorToInt(col / SQUARES_DIMENSION) + (2 * Mathf.FloorToInt(row / SQUARES_DIMENSION));

			rows[row].Add(controllers[i]);
			columns[col].Add(controllers[i]);
			squares[square].Add(controllers[i]);

			controllers[i].Init(new CellNode(i, row, col, square));
			cells[row, col] = controllers[i];
		}
		Debug.Log(cells.Length);
		gridChecker.SetDictionaries(rows, columns, squares);
		EditorUtility.SetDirty(this);
		UnityEditor.SceneManagement.EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
	}
#endif

	#endregion

	#region LifeCycle

	private void Awake()
	{
		PopulateDictionary();
	}
	private void OnEnable()
	{
		GameManager.GameResetted += OnGameReset;
		ToolsManager.HintClicked += RandomlyFillCell;
	}

	private void OnDisable()
	{
		GameManager.GameResetted -= OnGameReset;
		ToolsManager.HintClicked -= RandomlyFillCell;
	}

	#endregion
	
	#region Methods
	
	private void OnGameReset()
	{
		GetService<GridBuilder>().CreateGrid();
	}

	private void RandomlyFillCell()
	{
		var randomIndex = Random.Range(0, uncompletedCells.Count);
		var cellToResolve = uncompletedCells[randomIndex];
		uncompletedCells.Remove(uncompletedCells[randomIndex]);
		cellToResolve.AutoResolve();
	}
	
	public (int gridDimension, int squaresDimension) GetGridDimensions => (GRID_DIMENSION, SQUARES_DIMENSION);

	#endregion

	#region Overrides

	public override void PopulateDictionary()
	{
		services.Add(typeof(GridBuilder), GetComponent<GridBuilder>());
		services.Add(typeof(GridChecker), GetComponent<GridChecker>());
	}

	#endregion
	
}