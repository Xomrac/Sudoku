using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using XomracUtilities.Extensions;

namespace _Scripts.Grid
{

	public class ThemeManager : MonoBehaviour
	{
		public List<ThemePalette> themes;
		private ThemePalette currentTheme;

		public static event Action<ThemePalette> themeChanged;
		public static event Action ThemeChanging; 

		private void Start()
		{
			currentTheme = themes.First();
			ChangeTheme(themes.First());
		}

		[Button]
		private void ApplyCurrentTheme()
		{
			ThemeChanging?.Invoke();
			themeChanged?.Invoke(currentTheme);
		}

		public void ChangeTheme(ThemePalette newTheme)
		{
			currentTheme = newTheme;
			ThemeChanging?.Invoke();
			themeChanged?.Invoke(newTheme);
		}
	}

}