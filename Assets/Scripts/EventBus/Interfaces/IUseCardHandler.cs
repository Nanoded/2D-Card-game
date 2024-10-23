namespace CardGame.BusEvents
{
    public interface IUseCardHandler : IGlobalSubscriber
    {
    	void OnUseCardHandler(Characters.Character character);
    }
}
