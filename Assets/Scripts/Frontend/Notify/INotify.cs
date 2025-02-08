using Core.Entity;

namespace Frontend.Notify
{
    public interface INotify
    {
        void OnNotify(NotifyMessage notifyMessage);
    }
}