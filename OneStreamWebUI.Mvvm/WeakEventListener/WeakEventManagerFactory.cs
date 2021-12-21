namespace OneStreamWebUI.Mvvm.WeakEventListener
{
    public interface IWeakEventManagerFactory
    {
        IWeakEventManager Create();
    }

    public class WeakEventManagerFactory : IWeakEventManagerFactory
    {
        public IWeakEventManager Create()
        {
            return new WeakEventManager();
        }
    }
}