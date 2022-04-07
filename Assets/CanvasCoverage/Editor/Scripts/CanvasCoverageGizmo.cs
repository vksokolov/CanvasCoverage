using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CanvasCoverage
{
    public class CanvasCoverageGizmo : ScriptableObject
    {
        private static Texture2D _texture;

        [DrawGizmo(GizmoType.Active | GizmoType.Selected)]
        static void DrawGizmoForMyScript(RectTransform rectTransform, GizmoType gizmoType)
        {
            if (!CanvasCoverageTool.IsOn) return;
            
            if (_texture == null)
            {
                _texture = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                _texture.SetPixel(0, 0, Color.white);
                _texture.Apply();
            }

            GetChildren(rectTransform)
                .ToList()
                .ForEach(graphic =>
                {
                    var rect = GetWorldRect(graphic.rectTransform, graphic.rectTransform.lossyScale);
                    Gizmos.DrawGUITexture(rect, _texture, CanvasCoverageTool.Preset.MainMaterial);
                });
        }

        private static IEnumerable<Graphic> GetChildren(Transform transform)
        {
            return transform
                .GetComponentsInChildren<Graphic>()
                .Where(x => x.raycastTarget);
        }

        private static Rect GetWorldRect (RectTransform rt, Vector2 scale) 
        {
            Vector3[] corners = new Vector3[4];
            rt.GetWorldCorners(corners);
            Vector3 topLeft = corners[0];
            var rect = rt.rect;
            Vector2 scaledSize = new Vector2(scale.x * rect.size.x, scale.y * rect.size.y);
 
            return new Rect(topLeft, scaledSize);
        }
    }
}
