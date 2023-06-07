using _Scripts.Grid;
using DG.Tweening;
using TMPro;
using UnityEngine;
using XomracUtilities.Patterns;

namespace Grid.Cells
{

	public class CellDisplayer : ServiceComponent<CellController>
	{

		#region Fields
		
		[SerializeField] private float growEndValue = 1.1f;
		[SerializeField] private float growSpeed = .2f;
		[SerializeField] private Ease ease;
		
		[SerializeField] private TextMeshProUGUI valueText;
		
		#endregion

		#region Methods

		private void Animate(bool correct)
		{
			Sequence sequence = DOTween.Sequence();
			if (correct)
			{
				sequence.Append(valueText.transform.DOPunchPosition(new Vector3(0, 10, 0), growSpeed));
			}
			else
			{
				sequence.Append(valueText.transform.DOPunchRotation(new Vector3(0, 0, 30), growSpeed));
				sequence.Append(valueText.transform.DOPunchRotation(new Vector3(0, 0, -30), growSpeed));
			}
			sequence.Append(valueText.transform.DOScale(growEndValue, growSpeed));
			sequence.Append(valueText.transform.DOScale(1, growSpeed));
			sequence.Play();
		}

		public void DisplayValue(int? value, Color color, bool correct)
		{
			valueText.text = value == null ? "" : $"{value}";
			valueText.color = color;
			Animate(correct);
		}

		public void DisplayValue(int? value, Color color)
		{
			valueText.text =value.HasValue ? $"{value}" : "";
			valueText.color = color;
		}

		public void HideValue()
		{
			var sequence = DOTween.Sequence();
			sequence.Append(valueText.transform.DOScale(0, growSpeed));
			sequence.onComplete += () => valueText.text = "";
			sequence.Play();
		}

		public void RemoveText()
		{
			valueText.text = "";
		}

		public void SetText(int? value,Color color)
		{
			valueText.text = value == null ? "" : $"{value}";
			valueText.color = color;
		}

		#endregion

	}

}