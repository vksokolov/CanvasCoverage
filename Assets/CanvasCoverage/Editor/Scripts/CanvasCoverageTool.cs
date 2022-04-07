using UnityEditor.EditorTools;
using UnityEngine;

namespace CanvasCoverage
{
    [EditorTool("CanvasCoverage", typeof(RectTransform))]
    class CanvasCoverageTool : EditorTool
    {
        public static bool IsOn;
        public static CanvasCoveragePreset Preset;
        
        [SerializeField] private Texture2D _toolIcon;
        [SerializeField] private CanvasCoveragePreset _preset;
        
        GUIContent _iconContent;

        void OnEnable()
        {
            EditorTools.activeToolChanged += OnToolChanged;
            _iconContent = new GUIContent()
            {
                image = _toolIcon,
                text = "CanvasCoverage",
                tooltip = "CanvasCoverage"
            };

            Preset = _preset;
        }

        public override GUIContent toolbarIcon => _iconContent;

        private void OnDisable()
        {
            EditorTools.activeToolChanged -= OnToolChanged;
        }

        private void OnToolChanged()
        {
            IsOn = EditorTools.IsActiveTool(this);
        }
    }
}
