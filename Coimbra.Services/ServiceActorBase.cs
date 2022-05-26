﻿#nullable enable

using System.ComponentModel;
using System.Diagnostics;
using UnityEngine;

namespace Coimbra.Services
{
    /// <summary>
    /// Base class to easily create a <see cref="IService"/> that is also an <see cref="Actor"/>.
    /// </summary>
    [DisallowMultipleComponent]
    public abstract class ServiceActorBase<TServiceActor, TService> : Actor, IService
        where TServiceActor : ServiceActorBase<TServiceActor, TService>, TService
        where TService : class, IService
    {
        [SerializeReference]
        [Disable]
        private ServiceLocator? _owningLocator;

        /// <inheritdoc/>
        public ServiceLocator? OwningLocator
        {
            [DebuggerStepThrough]
            get => _owningLocator;
            [EditorBrowsable(EditorBrowsableState.Never)]
            set
            {
                if (_owningLocator == value)
                {
                    return;
                }

                ServiceLocator? previous = _owningLocator;
                _owningLocator = value;
                OnOwningLocatorChanged(previous, value);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            Destroy();
        }

        /// <inheritdoc/>
        protected override void OnDestroyed()
        {
            base.OnDestroyed();

            if (OwningLocator == null)
            {
                return;
            }

            if (OwningLocator.IsCreated(out TService? value) && value == this as TService)
            {
                OwningLocator.Set<TService>(null, false);
            }
            else
            {
                ServiceLocator? previous = _owningLocator;
                _owningLocator = null;
                OnOwningLocatorChanged(previous, null);
            }
        }

        /// <summary>
        /// Will be called after the <see cref="OwningLocator"/> has been set and only if the value actually changed.
        /// </summary>
        /// <param name="previous">The value before.</param>
        /// <param name="current">The value after. Is the same as the current <see cref="OwningLocator"/>.</param>
        protected virtual void OnOwningLocatorChanged(ServiceLocator? previous, ServiceLocator? current) { }
    }
}
