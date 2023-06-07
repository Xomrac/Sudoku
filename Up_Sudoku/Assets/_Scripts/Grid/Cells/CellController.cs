using System;
using _Scripts.Grid;
using Audio;
using Command;
using DG.Tweening;
using GameSystem;
using GameTools;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using XomracUtilities.Patterns;

namespace Grid.Cells
{

	public class CellController : ServiceLocator, IPointerDownHandler
	{

		#region Fields

		[SerializeField] private float growEndValue = 1.1f;
		[SerializeField] private float growSpeed = .2f;
		[SerializeField] private Ease ease;
		public static event Action<CellController> CellClicked;
		public static event Action<CellController> CellUpdated;

		[SerializeField][ReadOnly]private CellNode node;
		public CellNode Node => node;

		[SerializeField][ReadOnly]private bool editable;
		public bool Editable => editable;

		private int correctValue;
		public int CorrectValue => correctValue;

		[ReadOnly]public bool currentlyCompleted;
		public bool CurrentlyCompleted => currentlyCompleted;

		#endregion

		#region Editor

#if UNITY_EDITOR
		public void Init(CellNode newNode)
		{
			node = newNode;
			gameObject.name = $"[{node.CellRow},{node.CellColumn},{node.CellSquare}]";
			correctValue = 0;
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

		#endregion

		#region Callbacks

		public void OnPointerDown(PointerEventData eventData)
		{
			if (!editable) return;

			if (TryGetService(out CellInput input))
			{
				NumberSelector.Selected = input.OnValuePressed;
				ToolsManager.EraserClicked = input.OnEraserPressed;
			}
			else
			{
				Debug.LogWarning($"{gameObject.name} has no CellInput Component");
			}

			Animate();
			CellClicked?.Invoke(this);
		}

		#endregion

		#region Methods

		private void Animate()
		{
			var sequence = DOTween.Sequence();
			sequence.Append(transform.DOScale(growEndValue, growSpeed).SetEase(ease));
			sequence.Append(transform.DOScale(1f, growSpeed).SetEase(ease));
			sequence.Play();
		}

		public void SetCellValue(int value)
		{
			correctValue = value;
			if (TryGetService(out CellInput input))
			{
				input.SetInitialValue(value);
			}
			else
			{
				Debug.LogWarning($"{gameObject.name} has no CellInput Component");
			}
			currentlyCompleted = true;
		}

		public void InputValue(int value)
		{
			if (TryGetService(out CellInput input))
			{
				input.InputValue(value);
			}
			else
			{
				Debug.LogWarning($"{gameObject.name} has no CellInput Component");
			}
		}
		
		public void RestoreInitialCellValue(int? value)
		{
			if (TryGetService(out CellInput input))
			{
				input.RestoreInitialValue(value);
			}
			else
			{
				Debug.LogWarning($"{gameObject.name} has no CellInput Component");
			}
			currentlyCompleted = true;
		}

		public void AutoResolve()
		{
			if (TryGetService(out CellInput input))
			{
				input.SetCorrectvalue();
			}
			else
			{
				Debug.LogWarning($"{gameObject.name} has no CellInput Component");
			}
		}

		public void RestorePreviousValue(int? value)
		{
			if (!GameManager.Instance.IsZenMode && currentlyCompleted) return;
			if (TryGetService(out CellInput input))
			{
				NumberSelector.Selected = input.OnValuePressed;
				ToolsManager.EraserClicked = input.OnEraserPressed;
				input.RestorePreviousValue(value);
			}
			else
			{
				Debug.LogWarning($"{gameObject.name} has no CellInput Component");
			}
			Animate();
			CellClicked?.Invoke(this);
			CellUpdated?.Invoke(this);
		}

		public void SetCompletition(bool completed, bool UpdateCell=true)
		{
			currentlyCompleted = completed;
			editable = true;
			if (!GameManager.Instance.IsZenMode)
			{
				editable = !completed;
			}
			if (UpdateCell)
			{
				CellUpdated?.Invoke(this);
			}
		}

		#endregion

		#region Overrides

		public override void PopulateDictionary()
		{
			services.Add(typeof(CellHighlighter), GetComponent<CellHighlighter>());
			services.Add(typeof(CellNotesController),GetComponent<CellNotesController>());
			services.Add(typeof(CellThemer), GetComponent<CellThemer>());
			services.Add(typeof(CellInput), GetComponent<CellInput>());
			services.Add(typeof(CellDisplayer), GetComponent<CellDisplayer>());
		}

		#endregion

	}

}