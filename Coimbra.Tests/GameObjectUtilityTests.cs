﻿using NUnit.Framework;
using UnityEngine;

namespace Coimbra.Tests
{
    [TestFixture]
    [TestOf(typeof(GameObjectUtility))]
    public class GameObjectUtilityTests
    {
        [Test]
        public void GivenGameObjectWithoutBehaviour_WhenGetOrCreateBehaviour_ThenBehaviourIsValid()
        {
            GameObject gameObject = new GameObject();
            GameObjectBehaviour behaviour = gameObject.GetOrCreateBehaviour();
            Assert.That(behaviour, Is.Not.Null);
            Assert.That(behaviour.gameObject, Is.EqualTo(gameObject));
        }

        [Test]
        public void GivenGameObjectWithBehaviour_WhenGetOrCreateBehaviour_ThenBehaviourIsStillUnique()
        {
            GameObject gameObject = new GameObject("Test", typeof(GameObjectBehaviour));
            GameObjectBehaviour behaviour = gameObject.GetOrCreateBehaviour();
            Assert.That(behaviour.gameObject, Is.EqualTo(gameObject));
            Assert.That(gameObject.GetComponents<GameObjectBehaviour>().Length, Is.EqualTo(1));
        }

        [Test]
        public void GivenGameObjectWithBehaviour_AndItsTransform_WhenGetOrCreateBehaviourWithBoth_ThenBothResultsTheSame()
        {
            GameObject gameObject = new GameObject("Test", typeof(GameObjectBehaviour));
            Transform transform = gameObject.transform;
            Assert.That(gameObject.GetOrCreateBehaviour(), Is.EqualTo(transform.GetOrCreateBehaviour()));
        }
    }
}
