using System;
using UniRx;

namespace Frontend.Notify
{
    public sealed class Notifier
    {
        private Notifier()
        {
        }

        public NotifyMessage previousMessage { get; set; }

        public NotifyMessage currentMessage { get; set; }


        private static Notifier instance { get; set; }

        private event Action<NotifyMessage> onNotify;

        public static Notifier GetInstance()
        {
            if (null == instance) instance = new Notifier();
            return instance;
        }

        public void Notify(NotifyMessage.Title title, object parameter = null)
        {
            var message = new NotifyMessage();
            message.title = title;
            message.parameter = parameter;
            Notify(message);
        }

        private void Notify(NotifyMessage message)
        {
            if (onNotify == null) return;
            onNotify(message);
            previousMessage = currentMessage;
            currentMessage = message;
        }

        public void Dispose()
        {
            onNotify = null;
        }

        public IObservable<NotifyMessage> OnNotify()
        {
            return Observable.FromEvent<NotifyMessage>(message => onNotify += message, message => onNotify -= message);
        }
    }
}