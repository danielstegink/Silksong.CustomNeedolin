using BepInEx.Configuration;
using CustomNeedolin.Settings;
using DanielSteginkUtils.ExternalFiles;
using DanielSteginkUtils.Helpers;
using HarmonyLib;
using HutongGames.PlayMaker;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace CustomNeedolin.Helpers
{
    public static class NeedolinHelper
    {
        /// <summary>
        /// List of stored audio clips
        /// </summary>
        internal static Dictionary<string, AudioClip> loadedClips = new Dictionary<string, AudioClip>();

        /// <summary>
        /// Replaces the Needolin's audio clip with the given one
        /// </summary>
        /// <param name="fileName"></param>
        public static void ChangeNeedolin(string fileName)
        {
            // First, find the Needolin Fsm
            Fsm? fsm = Needolin.NeedolinFsm;
            if (fsm == null)
            {
                CustomNeedolin.Instance.Log($"Needolin FSM not found");
                return;
            }

            // Get the new audio clip
            AudioClip? newClip = GetAudioClipFromExternalFile(fileName);
            if (newClip == null)
            {
                // If we don't find one, use the default
                newClip = Needolin.DefaultClip;
            }

            // Set audio clip as new default
            //CustomNeedolin.Instance.Log($"New audio selected: {newClip.name}");
            if (newClip != null)
            {
                Needolin.SetNewDefaultClip(newClip);
            }
        }

        /// <summary>
        /// Gets the given .WAV file from the mod's audio folder and converts it to an audio clip
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static AudioClip? GetAudioClipFromExternalFile(string fileName)
        {
            //CustomNeedolin.Instance.Log($"Attempting to load audio: {fileName}");

            // Check if we already loaded the clip previously
            if (loadedClips.TryGetValue(fileName, out AudioClip? value))
            {
                return value;
            }

            // Check if the file name is blank
            if (string.IsNullOrEmpty(fileName))
            {
                return null;
            }

            // Check if the file exists
            string modFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string filePath = Path.Combine(modFolder, "audio", $"{fileName}.wav");
            FileInfo file = new FileInfo(filePath);
            if (!file.Exists)
            {
                CustomNeedolin.Instance.Log($"Error getting file '{fileName}.wav'");
                return null;
            }

            using (Stream stream = file.OpenRead())
            {
                // Confirm we were able to read the file as a stream
                if (stream == null)
                {
                    CustomNeedolin.Instance.Log($"Error reading file '{fileName}.wav'");
                    return null;
                }

                return GetAudioClip.GetAudioClipFromStream(stream, fileName);
            }
        }
    }
}