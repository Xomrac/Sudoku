using System;
using _Scripts.Grid.Themes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Themes
{

	public class UIElementThemer : MonoBehaviour,IThemeable
	{

		#region Fields

		private MaskableGraphic elementToRecolor;
		public string elementKey;

		#endregion

		#region LifeCycle

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

		#endregion

		#region Methods

		public void ApplyTheme(ThemePalette theme)
		{
			elementToRecolor.color = theme.GetElementColor(elementKey);
		}

		#endregion
		
	}

}