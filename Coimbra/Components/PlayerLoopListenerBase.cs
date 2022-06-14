﻿using System.Runtime.CompilerServices;
using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Listen to player loop callbacks.
    /// </summary>
    /// <seealso cref="FixedUpdateListener"/>
    /// <seealso cref="LateUpdateListener"/>
    /// <seealso cref="UpdateListener"/>
    public abstract class PlayerLoopListenerBase : MonoBehaviour
    {
        public delegate void EventHandler(PlayerLoopListenerBase sender, float deltaTime);

        /// <summary>
        /// Invoked inside <see cref="Trigger"/>.
        /// </summary>
        public virtual event EventHandler OnTrigger
        {
            add => _eventHandler += value;
            remove => _eventHandler -= value;
        }

        private EventHandler _eventHandler;

        /// <summary>
        /// True if <see cref="OnTrigger"/> has any listener.
        /// </summary>
        protected bool HasListener => _eventHandler != null;

        /// <summary>
        /// Invokes the <see cref="OnTrigger"/> event.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected void Trigger(float deltaTime)
        {
            _eventHandler?.Invoke(this, deltaTime);
        }
    }
}
