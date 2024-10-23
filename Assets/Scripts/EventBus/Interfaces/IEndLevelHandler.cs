namespace CardGame.BusEvents
{
    public interface IEndLevelHandler : IGlobalSubscriber
    {
        void OnEndLevelHandler(bool isWin);
    }
}
