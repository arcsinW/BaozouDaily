using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.VoiceCommands;
using Windows.Storage;

namespace BaoZouRiBao.Helper
{
    public class VoiceCommandHelper
    {
        public static async void InstallVCDFile()
        {
            try
            {
                StorageFile vcdFile = await Package.Current.InstalledLocation.GetFileAsync("BaozouVoiceCommand.xml");

                await VoiceCommandDefinitionManager.InstallCommandDefinitionsFromStorageFileAsync(vcdFile);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Installing Voice Commands Failed: " + ex.ToString());
            }
        }
    }
}
