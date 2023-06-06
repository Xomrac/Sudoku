using System;
using _Scripts.Grid;
using UnityEngine;
using XomracUtilities.Patterns;

public class CellThemer : ServiceComponent<CellController>,IThemeable
{
	[SerializeField] private ThemePalette theme;
	public ThemePalette Theme => theme;

	private void OnEnable()
	{
		ThemeManager.ThemeChanging += ResetTheme;
		ThemeManager.themeChanged += ApplyTheme;
	}
	
	private void OnDisable()
	{
		ThemeManager.ThemeChanging -= ResetTheme;
		ThemeManager.themeChanged -= ApplyTheme;
	}

	public void ResetTheme()
	{
		ServiceLocator.GetService<CellHighlighter>().ResetColors();
	}

	public void ApplyTheme(ThemePalette newTheme)
	{
		theme = newTheme;
		ServiceLocator.GetService<CellHighlighter>().ResetColors();
	}
}