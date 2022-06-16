﻿using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Listen to <see cref="OnBeforeTransformParentChanged"/> callback.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu(CoimbraUtility.GeneralMenuPath + "Before Transform Parent Changed Listener")]
    [HelpURL("https://docs.unity3d.com/Packages/com.unity.ugui@1.0/api/UnityEngine.EventSystems.UIBehaviour.html#UnityEngine_EventSystems_UIBehaviour_OnBeforeTransformParentChanged")]
    public sealed class BeforeTransformParentChangedListener : MonoBehaviour
    {
        public delegate void EventHandler(BeforeTransformParentChangedListener sender);

        /// <summary>
        /// Invoked inside <see cref="OnBeforeTransformParentChanged"/>.
        /// </summary>
        public event EventHandler OnTrigger;

        private void OnBeforeTransformParentChanged()
        {
            OnTrigger?.Invoke(this);
        }
    }
}
