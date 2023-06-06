using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using _Scripts.Grid;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(UIElementThemer))][CanEditMultipleObjects]
public class UIElementThemerEditor : Editor
{
    public VisualTreeAsset UXML_File;
    private VisualElement root;
    private DropdownField keysDropdown;
    
    
    public override VisualElement CreateInspectorGUI()
    {
        root = new VisualElement();
        UXML_File.CloneTree(root);
        var dropdownChoices = new List<string>();
        keysDropdown = root.Q<DropdownField>(name: "keys-dropdown");
        
        Type type = typeof(ElementsNames);
        var props = type.GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo prop in props)
        {
            dropdownChoices.Add((string)prop.GetValue(null));
        }
        
        
        keysDropdown.choices = dropdownChoices;
        return root;
    }
}
