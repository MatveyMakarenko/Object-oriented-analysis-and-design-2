namespace SmartHomeWithPattern.Products
{
    public interface Lock
    {
        void Lock();
        void Unlock();
        bool IsLocked();
        string GetLog();
    }
}