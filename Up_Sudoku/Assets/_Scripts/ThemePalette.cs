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


		[SerializeField] private Color initialValuesColor=Color.white;
		public Color InitialValuesColor => initialValuesColor;

		[SerializeField] private Color correctValuesColor=Color.white;
		public Color CorrectValuesColor => correctValuesColor;

		[SerializeField] private Color wrongValuesColor=Color.white;
		public Color WrongValuesColor => wrongValuesColor;

		[SerializeField] private Color neutralValuesColor=Color.white;
		public Color NeutralValuesColor => neutralValuesColor;

		[SerializeField] private Color notesColor = Color.white;
		public Color NotesColor => notesColor;

		

		

		

		

		
		
	}

}