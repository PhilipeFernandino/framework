﻿using System.Diagnostics;
using UnityEditor;
using UnityEngine.Rendering;

namespace Coimbra.Editor
{
    /// <summary>
    /// Editor for <see cref="RenderImageListener"/>.
    /// </summary>
    [CustomEditor(typeof(RenderImageListener))]
    public sealed class RenderImageListenerEditor : UnityEditor.Editor
    {
        /// <inheritdoc/>
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GraphicsSettings.currentRenderPipeline.IsValid())
            {
                DisplayRenderPipelineWarning();
            }
        }

        [Conditional("WITH_HDRP")]
        [Conditional("WITH_URP")]
        private void DisplayRenderPipelineWarning()
        {
            CoimbraEditorGUIUtility.DrawComponentWarningForRenderPipeline(target.GetType());
        }
    }
}
