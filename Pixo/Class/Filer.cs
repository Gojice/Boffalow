using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pixo
{
    class Filer
    {
        private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        public async Task<string> ReadSession()
        {
            return await _loadSession();
        }

        private static async Task<string> _loadSession()
        {
            try
            {
                var us = localSettings.Values["LastUser"] as string;
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile File = await storageFolder.GetFileAsync(us + ".txt");
                Debug.WriteLine(storageFolder);
                var json = await Windows.Storage.FileIO.ReadTextAsync(File);
                Debug.WriteLine(json);
                return json;
            }
            catch { return ""; }
        }
    }
}
