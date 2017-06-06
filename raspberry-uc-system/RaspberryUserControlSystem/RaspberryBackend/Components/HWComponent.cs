namespace RaspberryBackend
{
    public abstract class HWComponent
    {
        protected bool _initialized = false;

        public bool isInitialized()
        {
            return _initialized;
        }

        public abstract void initiate();
    }
}