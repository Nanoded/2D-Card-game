using System;
using System.Collections.Generic;
using System.Linq;

namespace CardGame.BusEvents
{
    public static class EventBus
    {
    	 private static Dictionary<Type, List<IGlobalSubscriber>> s_subscribers = new();

        private static List<Type> GetAllSubscriberTypes(Type subscriberType)
        {
            List<Type> allTypes = subscriberType.GetInterfaces()
                .Where(i => typeof(IGlobalSubscriber).IsAssignableFrom(i) && i != typeof(IGlobalSubscriber)).ToList();
            return allTypes;
        }

        public static void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetAllSubscriberTypes(subscriber.GetType());
            foreach (Type subscriberType in subscriberTypes)
            {
                if(!s_subscribers.ContainsKey(subscriberType))
                {
                    s_subscribers[subscriberType] = new List<IGlobalSubscriber>();
                }
                s_subscribers[subscriberType].Add(subscriber);
            }
        }

        public static void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetAllSubscriberTypes(subscriber.GetType());
            foreach(Type subscriberType in subscriberTypes)
            {
                if(s_subscribers.ContainsKey(subscriberType))
                {
                    s_subscribers[subscriberType].Remove(subscriber);
                }
            }
        }

        public static void Invoke<TSubscriber>(Action<TSubscriber> action) where TSubscriber : class, IGlobalSubscriber
        {
            if (!s_subscribers.ContainsKey(typeof(TSubscriber)))
            {
                return;
            }
            foreach (IGlobalSubscriber subscriber in s_subscribers[typeof(TSubscriber)])
            {
                action.Invoke(subscriber as TSubscriber);
            }
        }
    }
}
