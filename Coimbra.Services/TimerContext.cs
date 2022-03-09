﻿using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace Coimbra.Services
{
    [AddComponentMenu("")]
    [DisallowMultipleComponent]
    [Preserve]
    internal sealed class TimerContext : MonoBehaviour
    {
        internal int CompletedLoops;

        internal int TargetLoops;

        internal int Version;

        internal Action Callback;

        internal TimerService Service;

        internal void Run()
        {
            Callback?.Invoke();

            if (TargetLoops <= 0)
            {
                return;
            }

            CompletedLoops++;

            if (CompletedLoops < TargetLoops)
            {
                return;
            }

            Service.StopTimer(this);
        }

        private void OnDisable()
        {
            CancelInvoke();

            Callback = null;
        }
    }
}