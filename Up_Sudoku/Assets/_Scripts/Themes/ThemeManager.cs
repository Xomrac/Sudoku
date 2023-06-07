using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XomracUtilities.Extensions;

namespace Themes
{

	public class ThemeManager : MonoBehaviour
	{

		#region Fields

		[SerializeField] private List<ThemePalette> themes;
		private ThemePalette currentTheme;
		public static event Action<ThemePalette> themeChanged;
		public static event Action ThemeChanging;

		#endregion

		#region LifeCycle

		private void Start()
		{
			currentTheme = themes.First();
			ChangeTheme(themes.First());
		}

		#endregion

		#region Editor

		[Button]
		private void ApplyCurrentTheme()
		{
			ThemeChanging?.Invoke();
			themeChanged?.Invoke(currentTheme);
		}

		#endregion

		#region Methods

		private void ChangeTheme(ThemePalette newTheme)
		{
			currentTheme = newTheme;
			ThemeChanging?.Invoke();
			themeChanged?.Invoke(newTheme);
		}

		#endregion

	}

}