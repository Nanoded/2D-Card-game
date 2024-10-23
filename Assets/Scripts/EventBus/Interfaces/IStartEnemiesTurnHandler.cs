namespace CardGame.BusEvents
{
    public interface IStartEnemiesTurnHandler : IGlobalSubscriber
    {
        void OnStartEnemiesTurnHandler(Characters.Character[] enemies);
    }
}
