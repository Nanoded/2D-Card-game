namespace CardGame.BusEvents
{
    public interface IEndPlayerTurnHandler : IGlobalSubscriber
    {
        void OnEndPlayerTurnHandler();
    }
}
