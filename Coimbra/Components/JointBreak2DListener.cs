﻿using UnityEngine;

namespace Coimbra
{
    /// <summary>
    /// Listen to <see cref="Joint2D"/>'s <see cref="OnJointBreak2D"/> callback.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Joint2D))]
    [AddComponentMenu(CoimbraUtility.GeneralMenuPath + "Joint Break 2D Listener")]
    [HelpURL("https://docs.unity3d.com/ScriptReference/MonoBehaviour.OnJointBreak2D.html")]
    public sealed class JointBreak2DListener : MonoBehaviour
    {
        public delegate void EventHandler(JointBreak2DListener sender, Joint2D brokenJoint);

        /// <summary>
        /// Invoked inside <see cref="OnJointBreak"/>.
        /// </summary>
        public event EventHandler OnTrigger;

        private Joint2D _joint;

        /// <summary>
        /// The joint this component depends on.
        /// </summary>
        public Joint2D Joint => _joint != null ? _joint : _joint = GetComponent<Joint2D>();

        private void OnJointBreak2D(Joint2D brokenJoint)
        {
            OnTrigger?.Invoke(this, brokenJoint);
        }
    }
}
