//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================

using System.Collections.Generic;
using System.Linq;
using Core.Entity;
using Core.Extensions;
using Frontend.Component.Property;

namespace Frontend.Notify
{
    public sealed class Notifier
    {
        private Notifier()
        {
            notifierDictionary = new Dictionary<int, INotify>();
        }

        public NotifyMessage previousMessage { get; set; }

        public NotifyMessage currentMessage { get; set; }

        public Dictionary<int, INotify> notifierDictionary { get; set; }

        private static Notifier instance { get; set; }

        public static Notifier GetInstance()
        {
            if (null == instance) instance = new Notifier();
            return instance;
        }

        public void Notify(NotifyMessage message)
        {
            notifierDictionary.ForEach(pair => { pair.Value.OnNotify(message); });
            previousMessage = currentMessage;
            currentMessage = message;
        }

        public void Notify(int id, NotifyMessage message)
        {
            notifierDictionary[id].OnNotify(message);
            previousMessage = currentMessage;
            currentMessage = message;
        }

        public void Notify(NotifyMessage message, Parameter parameter)
        {
            notifierDictionary.ForEach(pair => { pair.Value.OnNotify(message, parameter); });
            previousMessage = currentMessage;
            currentMessage = message;
        }

        public bool Add(INotify notify, BaseProperty property)
        {
            if (notifierDictionary.ContainsKey(property.id)) return false;
            notifierDictionary.Add(property.id, notify);
            return true;
        }

        public void Remove(BaseProperty property)
        {
            Remove(property.id);
        }

        public void Remove(int id)
        {
            notifierDictionary.Where(pair => pair.Key == id).ForEach(pair => { notifierDictionary.Remove(pair.Key); });
        }

        public void Clear()
        {
            notifierDictionary.Clear();
        }
    }
}