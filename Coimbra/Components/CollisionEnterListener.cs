﻿using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Listen to <see cref="Collider"/>'s <see cref="OnCollisionEnter"/> callback.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu(CoimbraUtility.GeneralMenuPath + "Collision Enter Listener")]
    [HelpURL("https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnCollisionEnter.html")]
    public sealed class CollisionEnterListener : CollisionListenerBase
    {
        private void OnCollisionEnter(Collision collision)
        {
            Trigger(collision);
        }
    }
}
