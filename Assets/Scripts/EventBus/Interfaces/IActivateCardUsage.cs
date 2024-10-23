namespace CardGame.BusEvents
{
    public interface IActivateCardUsage : IGlobalSubscriber
    {
        void OnActivateCardUsageHandler(Cards.CardType cardType);
    }
}
