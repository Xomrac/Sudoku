using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Scripts.Grid.Themes;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Themes
{

	[CreateAssetMenu(fileName = "Theme_", menuName = "Themes/Palette")]
	public class ThemePalette : SerializedScriptableObject
	{

		#region Fields

		[DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.OneLine,KeyLabel = "Element Name",ValueLabel = "Element Color")]
		[SerializeField] private Dictionary<string,Color> elementsColor= new();
		[SerializeField] private Color backupColor=Color.white;

		#endregion

		#region Editor

#if UNITY_EDITOR

		[OnInspectorInit]
		private void CheckDictionary()
		{
			List<string> stringValues = GetStrings();
			List<string> keys = new List<string>(elementsColor.Keys);
			foreach (string key in keys)
			{
				if (!stringValues.Contains(key))
				{
					elementsColor.Remove(key);
				}
			}
			foreach (string stringValue in stringValues)
			{
				elementsColor.TryAdd(stringValue, Color.white);
			}

		}

		private List<string> GetStrings()
		{
			Type type = typeof(ElementsNames);
			var props = type.GetFields(BindingFlags.Public | BindingFlags.Static);
			return props.Select(prop => (string)prop.GetValue(null)).ToList();
		}
#endif

		#endregion

		#region Methods

		public Color GetElementColor(string key)
		{
			return elementsColor.TryGetValue(key, out Color value) ? value : backupColor;
		}

		#endregion
		


		
		














	}

}