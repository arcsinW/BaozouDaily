using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace BaoZouRiBao.Helper
{
    /// <summary>
    /// 
    /// </summary>
    public class PersistenceFolders
    {
        static PersistenceFolders()
        {
            //LocalFolder.CreateFolderAsync("image", CreationCollisionOption.OpenIfExists);
        }

        public static StorageFolder LocalFolder = ApplicationData.Current.LocalFolder;
        public static StorageFolder Cache
        {
            get
            {
                return ApplicationData.Current.LocalCacheFolder;
            }
        }

        //public static StorageFolder Image
        //{
        //    get
        //    {
        //        return await LocalFolder.CreateFolderAsync("image", CreationCollisionOption.OpenIfExists);
        //    }
        //}
    }
}
