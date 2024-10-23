namespace CardGame.BusEvents
{
    public interface IClickHandler : IGlobalSubscriber
    {
        void OnClickHandler(UnityEngine.Collider2D hittedCollider);
    }
}
