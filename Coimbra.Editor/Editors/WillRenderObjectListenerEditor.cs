﻿using System.Diagnostics;
using UnityEditor;
using UnityEngine.Rendering;

namespace Coimbra.Editor
{
    /// <summary>
    /// Editor for <see cref="WillRenderObjectListener"/>.
    /// </summary>
    [CustomEditor(typeof(WillRenderObjectListener))]
    public sealed class WillRenderObjectListenerEditor : UnityEditor.Editor
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
        private void DisplayRenderPipelineWarning()
        {
            CoimbraEditorGUIUtility.DrawComponentWarningForRenderPipeline(target.GetType());
        }
    }
}
