﻿using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Listen to <see cref="Renderer"/>'s <see cref="OnBecameVisible"/> callback.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu(CoimbraUtility.GeneralMenuPath + "Became Visible Listener")]
    [HelpURL("https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnBecameVisible.html")]
    public sealed class BecameVisibleListener : MonoBehaviour
    {
        public delegate void EventHandler(BecameVisibleListener sender);

        /// <summary>
        /// Invoked inside <see cref="OnBecameVisible"/>.
        /// </summary>
        public event EventHandler OnTrigger;

        private Renderer _renderer;

        /// <summary>
        /// The renderer this component depends on.
        /// </summary>
        public Renderer Renderer => _renderer != null ? _renderer : _renderer = GetComponent<Renderer>();

        private void OnBecameVisible()
        {
            OnTrigger?.Invoke(this);
        }
    }
}