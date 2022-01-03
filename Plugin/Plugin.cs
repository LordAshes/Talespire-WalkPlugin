using BepInEx;
using HarmonyLib;
using System;
using UnityEngine;

namespace LordAshes
{
    [BepInPlugin(Guid, Name, Version)]
    [BepInDependency(LordAshes.FileAccessPlugin.Guid)]
    public partial class WalkPlugin : BaseUnityPlugin
    {
        // Plugin info
        public const string Name = "Walk Plug-In";            
        public const string Guid = "org.lordashes.plugins.walk";
        public const string Version = "1.0.0.0";                

        // Configuration

        /// <summary>
        /// Function for initializing plugin
        /// This function is called once by TaleSpire
        /// </summary>
        void Awake()
        {
            // Not required but good idea to log this state for troubleshooting purpose
            UnityEngine.Debug.Log("Walk Plugin: Active.");

            var harmony = new Harmony(Guid);
            harmony.PatchAll();

            Utility.PostOnMainPage(this.GetType());
        }

        /// <summary>
        /// Function for determining if view mode has been toggled and, if so, activating or deactivating Character View mode.
        /// This function is called periodically by TaleSpire.
        /// </summary>
        void Update()
        {
            // Can be used to determine if a board is loaded
            // Beware: Board loaded does not necessarily mean all the minis have properly loaded and are accessible
            if (Utility.isBoardLoaded())
            {
            }

            if(currentState==WalkState.walking)
            {
                if(DateTime.UtcNow.Subtract(lastWalk).TotalSeconds>1)
                {
                    currentState = WalkState.stopped;
                    //
                    // Deactivate Animation
                    //
                    try
                    {
                        CreatureBoardAsset asset;
                        CreaturePresenter.TryGetAsset(LocalClient.SelectedCreatureId, out asset);
                        if (asset != null)
                        {
                            Animation animation = asset.GetComponentInChildren<Animation>();
                            if (animation != null)
                            {
                                Debug.Log("Walk Plugin: Stopping Walking Animation. Starting Idle Animation.");
                                animation.Stop();
                                animation.Play("Idle");
                            }
                        }
                    }
                    catch(Exception x)
                    {
                        Debug.Log("Walk Plugin: Exception");
                        Debug.LogException(x);
                    }
                }
            }
        }
    }
}
