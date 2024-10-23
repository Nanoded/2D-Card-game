namespace CardGame.BusEvents
{
    public interface IDeselectCardHandler : IGlobalSubscriber
    {
        void OnDeselectCardHandler();
    }
}
