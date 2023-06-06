using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Grid
{

	public class UIElementThemer : MonoBehaviour,IThemeable
	{
		private MaskableGraphic elementToRecolor;
		public string elementKey;

		
		private TextMeshProUGUI elementText;

		private void Awake()
		{
			elementToRecolor = GetComponent<MaskableGraphic>();
		}

		private void OnEnable()
		{
			ThemeManager.themeChanged += ApplyTheme;
		}
		
		private void OnDisable()
		{
			ThemeManager.themeChanged -= ApplyTheme;
		}

		public void ApplyTheme(ThemePalette theme)
		{
			elementToRecolor.color = theme.GetElementColor(elementKey);
		}
	}

}