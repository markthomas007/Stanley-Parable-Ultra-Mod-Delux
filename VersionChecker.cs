using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Net;
using MelonLoader;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

namespace StanleyParableUltraModDeluxe
{
    //Checks if the mod is on the latest version. If not output warning to console and ui.
    public class VersionChecker : MelonMod
    {
        /* Didnt work. Will attempt later
        public override void OnApplicationStart()
        {

            MelonLogger.Msg("Response:");
            string Response;
            var client = new WebClient();
            try
            {
                Response = client.DownloadString("https://stanley-mod-thing.netlify.app/version.txt");
            }
            catch (WebException e)
            {
                MelonLogger.Error($"Failed to contact website: {e.Message}");
                return;
            }
            
            MelonLogger.Msg("Response:" + Response);
        }*/
    }
}
