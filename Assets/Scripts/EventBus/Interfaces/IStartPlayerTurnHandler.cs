namespace CardGame.BusEvents
{
    public interface IStartPlayerTurnHandler : IGlobalSubscriber
    {
        void OnStartPlayerTurnHandler();
    }
}
