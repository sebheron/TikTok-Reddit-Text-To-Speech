using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedditTextToSpeech.UI.Services
{
    /// <summary>
    /// Custom implementation of an event aggregator (similar to Prisms).
    /// </summary>
    public class EventManager : IEventManager
    {
        private static IDictionary<string, List<Action<object>>> actionTEvents = new Dictionary<string, List<Action<object>>>();
        private static IDictionary<string, List<Action>> actionEvents = new Dictionary<string, List<Action>>();

        /// <summary>
        /// Emit an event.
        /// </summary>
        /// <typeparam name="T">Event args type.</typeparam>
        /// <param name="name">Name of the event.</param>
        /// <param name="args">Event args.</param>
        public void Emit<T>(string name, T args) where T : class
        {
            if (actionTEvents.TryGetValue(name, out List<Action<object>> actions))
            {
                foreach (var action in actions)
                {
                    action.Invoke(args);
                }
            }
        }

        /// <summary>
        /// Emit an event.
        /// </summary>
        /// <param name="name">Name of the event.</param>
        public void Emit(string name)
        {
            if (actionEvents.TryGetValue(name, out List<Action> actions))
            {
                foreach (var action in actions)
                {
                    action.Invoke();
                }
            }
        }

        /// <summary>
        /// Subscribe to an event.
        /// </summary>
        /// <typeparam name="T">Event args type.</typeparam>
        /// <param name="name">Name of the event.</param>
        /// <param name="action">The action to be invoked.</param>
        public void Subscribe<T>(string name, Action<T> action) where T : class
        {
            if (!actionTEvents.ContainsKey(name))
            {
                actionTEvents.Add(name, new List<Action<object>>());
            }
            actionTEvents[name].Add(o => action((T)o));
        }

        /// <summary>
        /// Subscribe to an event.
        /// </summary>
        /// <param name="name">Name of the event.</param>
        /// <param name="action">The action to be invoked.</param>
        public void Subscribe(string name, Action action)
        {
            if (!actionEvents.ContainsKey(name))
            {
                actionEvents.Add(name, new List<Action>());
            }
            actionEvents[name].Add(action);
        }
    }
}
