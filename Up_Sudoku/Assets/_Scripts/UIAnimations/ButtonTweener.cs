using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UIAnimations
{

	public class ButtonTweener : MonoBehaviour
	{

		#region Fields

		[SerializeField] private float growEndValue = 1.1f;
		[SerializeField] private float growSpeed = .2f;
		[SerializeField] private Ease ease = Ease.Linear;

		#endregion

		#region LifeCycle
		
		private void Start()
		{
			GetComponent<Button>().onClick.AddListener(Animate);
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

		#endregion

	}

}