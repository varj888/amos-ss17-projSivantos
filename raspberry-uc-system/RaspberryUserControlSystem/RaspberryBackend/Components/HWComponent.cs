namespace RaspberryBackend
{
    /// <summary>
    /// This abstract Class represents the Super - Class vor all Hardware Components which shall be connected to the RasPi
    /// </summary>
    public abstract class HWComponent
    {
        protected bool _initialized = false;

        /// <summary>
        /// Can be used to ask if a Hardware Component is initialized
        /// </summary>
        /// <returns>True if initialised otherwise False</returns>
        public bool isInitialized()
        {
            return _initialized;
        }

        /// <summary>
        /// Initiates a Hardware Component, so it is ready to use for the RasPi
        /// </summary>
        public abstract void initiate();
    }
}