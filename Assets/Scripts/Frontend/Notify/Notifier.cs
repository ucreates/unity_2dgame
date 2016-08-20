//======================================================================
// Project Name    : hetappy bird
//
// Copyright © 2016 U-CREATES. All rights reserved.
//
// This source code is the property of U-CREATES.
// If such findings are accepted at any time.
// We hope the tips and helpful in developing.
//======================================================================
using UnityEngine;
using System.Collections.Generic;
using Core.Entity;
using Frontend.Component.Property;
namespace Frontend.Notify {
public sealed class Notifier {
    public NotifyMessage previousMessage {
        get;
        set;
    }
    public NotifyMessage currentMessage {
        get;
        set;
    }
    public Dictionary<int, INotify> notifierDictionary {
        get;
        set;
    }
    private static Notifier instance {
        get;
        set;
    }
    private Notifier() {
        this.notifierDictionary = new Dictionary<int, INotify>();
    }
    public static Notifier GetInstance() {
        if (null == Notifier.instance) {
            Notifier.instance = new Notifier();
        }
        return Notifier.instance;
    }
    public void Notify(NotifyMessage message) {
        foreach (int id in this.notifierDictionary.Keys) {
            INotify notify = this.notifierDictionary[id];
            notify.OnNotify(message, null);
        }
        this.previousMessage = this.currentMessage;
        this.currentMessage = message;
    }
    public void Notify(int id, NotifyMessage message) {
        this.notifierDictionary[id].OnNotify(message, null);
        this.previousMessage = this.currentMessage;
        this.currentMessage = message;
    }
    public void Notify(NotifyMessage message, Parameter parameter) {
        foreach (int id in this.notifierDictionary.Keys) {
            INotify notify = notifierDictionary[id];
            notify.OnNotify(message, parameter);
        }
        this.previousMessage = this.currentMessage;
        this.currentMessage = message;
    }
    public bool Add(INotify notify, BaseProperty property) {
        if (false != this.notifierDictionary.ContainsKey(property.id)) {
            return false;
        }
        this.notifierDictionary.Add(property.id, notify);
        return true;
    }
    public bool Remove(BaseProperty property) {
        return this.Remove(property.id);
    }
    public bool Remove(int id) {
        if (false == this.notifierDictionary.ContainsKey(id)) {
            return false;
        }
        this.notifierDictionary.Remove(id);
        return true;
    }
    public void Clear() {
        this.notifierDictionary.Clear();
        return;
    }
}
}
