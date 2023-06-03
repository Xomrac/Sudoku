using UnityEngine;
using UnityEngine.Serialization;

namespace _Scripts.Grid
{

	[CreateAssetMenu(fileName = "Theme_", menuName = "Themes/Palette")]
	public class ThemePalette : ScriptableObject
	{
		[SerializeField] private Color normalCellsColor=Color.white;
		public Color NormalCellsColor => normalCellsColor;

		[SerializeField] private Color selectedCellColor=Color.white;
		public Color SelectedCellColor => selectedCellColor;

		[SerializeField] private Color colRowSquaresCellsColor=Color.white;
		public Color ColRowSquaresCellsColor => colRowSquaresCellsColor;

		[SerializeField] private Color sameNumberColor=Color.white;
		public Color SameNumberColor => sameNumberColor;

		
		
	}

}