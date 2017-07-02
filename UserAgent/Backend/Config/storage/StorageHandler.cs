using System;
using System.IO;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Windows.Storage;

namespace RaspberryBackend
{
    /// <summary>
    /// Adaptedion of <see cref="https://stackoverflow.com/questions/34385625/saving-files-on-raspberry-pi-with-windows-iot"/>
    /// Provides functions to save and load single object as well as List of 'T' using serialization
    /// </summary>
    /// <typeparam name="T">Type parameter to be serialize</typeparam>
    public static class StorageHandler<T> where T : new()
    {
        private static bool folderExists;

        private static StorageFolder storageFolder;
        /// <summary>
        /// Can be used to save an arbitary DataType to an arbitary Filename. For now it is used to Save the current information of the HI.
        /// </summary>
        /// <param name="FileName">The name of the File which shall be saved. For generic names use <see cref="StorageCfgs"/> </param>
        /// <param name="_Data"> The serializable Object whish shall be the content of the Fole. <see cref="Hi"/></param>
        /// <returns>The Task which can be used for waiting to be finisched </returns>
        public static async Task Save(string FileName, T _Data)
        {
            MemoryStream _MemoryStream = new MemoryStream();
            DataContractSerializer Serializer = new DataContractSerializer(typeof(T));
            Serializer.WriteObject(_MemoryStream, _Data);

            Task.WaitAll();

            if (!folderExists)
            {
                System.Diagnostics.Debug.WriteLine("\n Storage Folder is going to be initialized... \n");
                //Windows user documents folder
                StorageFolder docfolder = await KnownFolders.GetFolderForUserAsync(null, KnownFolderId.DocumentsLibrary);
                storageFolder = await docfolder.CreateFolderAsync(StorageCfgs.StorageFolder_Cfgs, CreationCollisionOption.OpenIfExists);
                folderExists = storageFolder != null;
                System.Diagnostics.Debug.WriteLine(folderExists ? "\n ...Storage Folder initialized \n" : "\n ...Storage Folder initialization FAILED \n");
            }

            System.Diagnostics.Debug.WriteLine("\n Saving File {0} , in {1} : ", FileName, folderExists ? storageFolder.Path : " NOWHERE! ");
            System.Diagnostics.Debug.WriteLine("\n Data Content to be saved: \n " + _Data.ToString());


            StorageFile _File = await storageFolder.CreateFileAsync(FileName, CreationCollisionOption.ReplaceExisting);

            using (Stream fileStream = await _File.OpenStreamForWriteAsync())
            {
                _MemoryStream.Seek(0, SeekOrigin.Begin);
                await _MemoryStream.CopyToAsync(fileStream);
                await fileStream.FlushAsync();
                fileStream.Dispose();

                System.Diagnostics.Debug.WriteLine("\n ...Data Content saved! \n ");
            }
        }

        /// <summary>
        /// Can be used to load an arbitary DataType to an arbitary Filename. For now it is used to load the current information of the HI.
        /// </summary>
        /// <param name="FileName">The name of the File which shall be loaded. For generic names use <see cref="StorageCfgs"/> </param>
        /// <returns>
        /// Returns the Type which was defined as the Generig Parameter T. If it exists, it content will be taken from file.
        /// If the file could not be found it will return a new empty Type.
        /// </returns>
        public static async Task<T> Load(string FileName)
        {
            if (!folderExists)
            {
                System.Diagnostics.Debug.WriteLine("\n Storage Folder is going to be initialized... \n");
                StorageFolder docfolder = await KnownFolders.GetFolderForUserAsync(null, KnownFolderId.DocumentsLibrary);
                storageFolder = await docfolder.CreateFolderAsync(StorageCfgs.StorageFolder_Cfgs, CreationCollisionOption.OpenIfExists);
                folderExists = storageFolder != null;
                System.Diagnostics.Debug.WriteLine(folderExists ? "\n ...Storage Folder initialized \n" : "\n ...Storage Folder initialization FAILED! \n");

            }

            StorageFile _File;
            T Result;

            try
            {
                Task.WaitAll();
                System.Diagnostics.Debug.WriteLine("\n Loading File {0} , in {1}: \n ", FileName, folderExists ? storageFolder.Path : " NOWHERE! ");

                _File = await storageFolder.GetFileAsync(FileName);


                using (Stream stream = await _File.OpenStreamForReadAsync())
                {
                    DataContractSerializer Serializer = new DataContractSerializer(typeof(T));
                    Result = (T)Serializer.ReadObject(stream);

                    System.Diagnostics.Debug.WriteLine("\n Data Content to be loaded: \n " + Result.ToString());
                }
                return Result;
            }
            catch (Exception ex)
            {
                return new T();
            }
        }
    }
}
