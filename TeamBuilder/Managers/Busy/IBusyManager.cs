namespace TeamBuilder.Managers.Busy
{
    public interface IBusyManager
    {
        bool IsBusy { get; }
        Task SetBusy();
        Task SetUnBusy();
        event EventHandler BusyChangedEvent;
    }
}