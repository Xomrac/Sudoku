using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Audio
{
    [CustomEditor(typeof(ScriptableAudio))]
    public class ScriptableAudioEditor : Editor
    {
        [SerializeField] private AudioSource previewer;

        public void OnEnable()
        {
            previewer = EditorUtility.CreateGameObjectWithHideFlags("Audio preview", HideFlags.HideAndDontSave, typeof(AudioSource)).GetComponent<AudioSource>();
        }

        public void OnDisable()
        {
            DestroyImmediate(previewer.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            EditorGUI.BeginDisabledGroup(serializedObject.isEditingMultipleObjects);
            if (GUILayout.Button("Preview"))
            {
                ((ScriptableAudio)target).Play(previewer);
            }
            EditorGUI.EndDisabledGroup();
        }
    }

}
