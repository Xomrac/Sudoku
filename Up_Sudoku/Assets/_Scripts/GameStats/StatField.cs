using DG.Tweening;
using TMPro;
using UnityEngine;

namespace GameStats
{

	public class StatField : MonoBehaviour
	{
		#region Fields

		[SerializeField] private TextMeshProUGUI statText;
        
		[SerializeField] private float growEndValue = 1.1f;
		[SerializeField] private float growSpeed = .2f;
		[SerializeField] private Ease ease = Ease.Linear;       
        
		#endregion

		#region Methods
		public void SetValue(string newValue,bool animate)
		{
			statText.text = newValue;
            
			if (!animate) return;
			Animate();
		}

		private void Animate()
		{
			Sequence sequence = DOTween.Sequence();
			sequence.Append(statText.transform.DOScale(growEndValue, growSpeed).SetEase(ease));
			sequence.Append(statText.transform.DOScale(1, growSpeed).SetEase(ease));
			sequence.Play();
		}
        
		#endregion
	}

}