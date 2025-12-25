using BepInEx;
using CustomNeedolin.Settings;
using HarmonyLib;
using UnityEngine;

namespace CustomNeedolin;

[BepInAutoPlugin(id: "io.github.danielstegink.customneedolin")]
[BepInDependency("org.silksong-modding.i18n")]
public partial class CustomNeedolin : BaseUnityPlugin
{
    internal static CustomNeedolin Instance { get; private set; }

    /// <summary>
    /// Stores the AudioClip currently set as the Needolin's default
    /// </summary>
    public static AudioClip? currentDefault = null;

    private void Awake()
    {
        // Put your initialization logic here
        Instance = this;

        Harmony harmony = new Harmony(Id);
        harmony.PatchAll();

        Logger.LogInfo($"Plugin {Name} ({Id}) has loaded!");
    }

    private void Start()
    {
        ConfigSettings.Initialize(Config);
    }

    /// <summary>
    /// Shared logger for the mod
    /// </summary>
    /// <param name="message"></param>
    internal void Log(string message)
    {
        Logger.LogInfo(message);
    }
}