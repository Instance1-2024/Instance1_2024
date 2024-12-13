#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Script.Runtime.InputSystem.InputTooltip {
    [CustomEditor(typeof(SCInputTooltipText))]
    public class SCInputTooltipEditor : Editor {
        SerializedProperty _actionProperty;
        SerializedProperty _bindingIDProperty;
        SerializedProperty _bindingTextProperty;

        readonly GUIContent _bindingLabel = new("Binding");
        GUIContent[] _bindingOptions;
        int _selectedBindingOption;
        string[] _bindingOptionsValues;

        protected void OnEnable() {
            _actionProperty = serializedObject.FindProperty("_action");
            _bindingIDProperty = serializedObject.FindProperty("_bindingID");
            _bindingTextProperty = serializedObject.FindProperty("_bindingText");
            
            RefreshBindingOptions();
        }
        
        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();

            // Display the label for the binding options
            EditorGUILayout.LabelField(_bindingLabel, EditorStyles.boldLabel);
            using (new EditorGUI.IndentLevelScope()) {
                // Display the action property field
                EditorGUILayout.PropertyField(_actionProperty);

                // Display the popup for binding options
                int newSelectedBinding = EditorGUILayout.Popup(_bindingLabel, _selectedBindingOption, _bindingOptions);
                if (newSelectedBinding != _selectedBindingOption) {
                    string bindingID = _bindingOptionsValues[newSelectedBinding];
                    _bindingIDProperty.stringValue = bindingID;
                    _selectedBindingOption = newSelectedBinding;
                }

                // Display the binding text property field
                EditorGUILayout.PropertyField(_bindingTextProperty);
            }

            // Apply any changes made in the inspector
            if (EditorGUI.EndChangeCheck()) {
                serializedObject.ApplyModifiedProperties();
                RefreshBindingOptions();
            }
        }

        void RefreshBindingOptions() {
            InputActionReference actionReference = (InputActionReference)_actionProperty.objectReferenceValue;
            InputAction action = actionReference?.action;

            if (action == null) {
                // Clear binding options if no action is set
                _bindingOptions = Array.Empty<GUIContent>();
                _bindingOptionsValues = Array.Empty<string>();
                _selectedBindingOption = -1;
                return;
            }

            ReadOnlyArray<InputBinding> bindings = action.bindings;
            int bindingCount = bindings.Count;

            _bindingOptions = new GUIContent[bindingCount];
            _bindingOptionsValues = new string[bindingCount];
            _selectedBindingOption = -1;

            string currentBindingId = _bindingIDProperty.stringValue;
            for (int i = 0; i < bindingCount; i++) {
                InputBinding binding = bindings[i];
                string bindingID = binding.id.ToString();
                bool haveBindingsGroups = !string.IsNullOrEmpty(binding.groups);

                // Determine the display options for the binding
                InputBinding.DisplayStringOptions displayOptions = InputBinding.DisplayStringOptions.DontUseShortDisplayNames |
                                                                   InputBinding.DisplayStringOptions.IgnoreBindingOverrides;
                if (!haveBindingsGroups) {
                    displayOptions |= InputBinding.DisplayStringOptions.DontOmitDevice;
                }

                // Create display string for the binding
                string displayString = action.GetBindingDisplayString(i, displayOptions);

                // Include part name if the binding is part of a composite
                if (binding.isPartOfComposite) {
                    displayString = $"{ObjectNames.NicifyVariableName(binding.name)} : {displayString}";
                }

                // Prevent '/' from creating submenus in the popup
                displayString = displayString.Replace('/', '\\');

                // Mention control schemes if the binding is part of them
                if (haveBindingsGroups) {
                    InputActionAsset asset = action.actionMap?.asset;
                    if (asset != null) {
                        string controlSchemes = string.Join(", ",
                            binding.groups.Split(InputBinding.Separator).Select(g =>
                                asset.controlSchemes.FirstOrDefault(s => s.bindingGroup == g).name));
                        displayString = $"{displayString} ({controlSchemes})";
                    }
                }

                _bindingOptions[i] = new GUIContent(displayString);
                _bindingOptionsValues[i] = bindingID;

                if (currentBindingId == bindingID) {
                    _selectedBindingOption = i;
                }
            }
        }
    }
}

#endif