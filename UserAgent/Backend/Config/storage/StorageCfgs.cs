namespace RaspberryBackend
{
    /// <summary>
    /// This class can be used to define central all the Data which shal be stored on a persistant hard drive.
    /// </summary>
    public class StorageCfgs
    {
        /// <summary>
        /// The Storage Folder for Configuration Files of the Backend
        /// </summary>
        public static string StorageFolder_Cfgs = "BackendConfig";

        /// <summary>
        /// The Filename of the Hi Config
        /// </summary>
        public static string FileName_HiCfg = "HiCfg.xml";

        /// <summary>
        /// Container to save Information on the connected Hi
        /// </summary>
        public static Hi Hi { get; } = new Hi();

    }
}
