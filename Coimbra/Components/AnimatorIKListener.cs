﻿using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Listen to <see cref="Animator"/>'s <see cref="OnAnimatorIK"/> callback.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Animator))]
    [AddComponentMenu(CoimbraUtility.GeneralMenuPath + "Animator IK Listener")]
    [HelpURL("https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnAnimatorIK.html")]
    public sealed class AnimatorIKListener : MonoBehaviour
    {
        public delegate void EventHandler(AnimatorIKListener sender, int layerIndex);

        /// <summary>
        /// Invoked inside <see cref="OnAnimatorIK"/>.
        /// </summary>
        public event EventHandler OnTrigger;

        private Animator _animator;

        /// <summary>
        /// The animator this component depends on.
        /// </summary>
        public Animator Animator => _animator != null ? _animator : _animator = GetComponent<Animator>();

        private void OnAnimatorIK(int layerIndex)
        {
            OnTrigger?.Invoke(this, layerIndex);
        }
    }
}
