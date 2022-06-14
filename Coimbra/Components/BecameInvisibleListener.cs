﻿using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Listen to <see cref="Renderer"/>'s <see cref="OnBecameInvisible"/> callback.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Renderer))]
    [AddComponentMenu(CoimbraUtility.GeneralMenuPath + "Became Invisible Listener")]
    [HelpURL("https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnBecameInvisible.html")]
    public sealed class BecameInvisibleListener : MonoBehaviour
    {
        public delegate void EventHandler(BecameInvisibleListener sender);

        /// <summary>
        /// Invoked inside <see cref="OnBecameInvisible"/>.
        /// </summary>
        public event EventHandler OnTrigger;

        private Renderer _renderer;

        /// <summary>
        /// The renderer this component depends on.
        /// </summary>
        public Renderer Renderer => _renderer != null ? _renderer : _renderer = GetComponent<Renderer>();

        private void OnBecameInvisible()
        {
            OnTrigger?.Invoke(this);
        }
    }
}
