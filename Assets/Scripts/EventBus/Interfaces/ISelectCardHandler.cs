namespace CardGame.BusEvents
{
    public interface ISelectCardHandler : IGlobalSubscriber
    {
        void OnSelectCardHandler(Cards.CardHolder cardHolder);
    }
}
