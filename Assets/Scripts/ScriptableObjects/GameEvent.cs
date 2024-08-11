using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SnakeGame
{
    [CreateAssetMenu(menuName = "Snake/GameEvent")]
    public class GameEvent : ScriptableObject
    {

        public delegate void EventRaisedDelegate();
        EventRaisedDelegate eventRaisedDelegate = null;

        public void RegisterDelegate(EventRaisedDelegate inputDelegate)
        {
            eventRaisedDelegate += inputDelegate;
        }

        public void UnregisterDelegate(EventRaisedDelegate inputDelegate)
        {
            eventRaisedDelegate -= inputDelegate;
        }

        public void Rise()
        {
            if(eventRaisedDelegate != null)
            {
                eventRaisedDelegate();
            }
        }

    }
}