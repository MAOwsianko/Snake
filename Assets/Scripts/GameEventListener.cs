using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SnakeGame
{
    public class GameEventListener : MonoBehaviour
    {
        [SerializeField] public GameEvent linkedEvent;
        [SerializeField] public UnityEvent actions;
        private void OnEnable()
        {
            if(linkedEvent!=null) 
            {
                linkedEvent.RegisterDelegate(OninvokedEvnet);
            }
        }

        private void OnDisable()
        {
            if (linkedEvent != null)
            {
                linkedEvent.UnregisterDelegate(OninvokedEvnet);
            }
        }

        private void OninvokedEvnet()
        {
            if(actions!=null)
            {
                actions.Invoke();
            }
        }
    }
}