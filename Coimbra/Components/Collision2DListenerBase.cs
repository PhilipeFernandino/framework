﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Base class to listen to <see cref="Collider2D"/>'s <see cref="Collision2D"/> callbacks.
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public abstract class Collision2DListenerBase : MonoBehaviour
    {
        public delegate void EventHandler(Collision2DListenerBase sender, Collision2D collision);

        /// <summary>
        /// Invoked inside the <see cref="Collision2D"/> callback.
        /// </summary>
        public event EventHandler OnTrigger;

        private Collider2D _collider;

        /// <summary>
        /// The collider this component depends on.
        /// </summary>
        public Collider2D Collider => _collider != null ? _collider : _collider = GetComponent<Collider2D>();

        /// <summary>
        /// Invokes the <see cref="OnTrigger"/> event.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Trigger(Collision2D collision)
        {
            OnTrigger?.Invoke(this, collision);
        }
    }
}
