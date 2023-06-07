using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using _Scripts.Grid.Themes;
using UnityEditor;
using UnityEngine.UIElements;

namespace Themes
{
	[CustomEditor(typeof(UIElementThemer))]
	[CanEditMultipleObjects]
	public class UIElementThemerEditor : Editor
	{

		#region Fields

		public VisualTreeAsset UXML_File;
		private VisualElement root;
		private DropdownField keysDropdown;
		
		#endregion


		public override VisualElement CreateInspectorGUI()
		{
			root = new VisualElement();
			UXML_File.CloneTree(root);
			keysDropdown = root.Q<DropdownField>(name: "keys-dropdown");

			Type type = typeof(ElementsNames);
			FieldInfo[] props = type.GetFields(BindingFlags.Public | BindingFlags.Static);
			List<string> dropdownChoices = props.Select(prop => (string)prop.GetValue(null)).ToList();
			
			keysDropdown.choices = dropdownChoices;
			return root;
		}
	}
}