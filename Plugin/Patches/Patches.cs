using BepInEx;
using HarmonyLib;

using System;
using System.Reflection;
using Unity.Mathematics;
using UnityEngine;

namespace LordAshes
{
    public partial class WalkPlugin : BaseUnityPlugin
    {
        private enum WalkState
        {
            walking,
            stopped
        }

        private static DateTime lastWalk;
        private static WalkState currentState;

        [HarmonyPatch(typeof(MovableBoardAsset), "MoveTo")]
        public static class MoveToPatch
        {
            public static bool Prefix(Vector3 pos, bool pickupLift = true)
            {
                lastWalk = DateTime.UtcNow;
                if (currentState == WalkState.stopped)
                {
                    currentState = WalkState.walking;
                    //
                    // Activate Animation
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
                                Debug.Log("Walk Plugin: Stopping Animation. Starting Walking Animation.");
                                animation.Stop();
                                animation.Play("Walk");
                            }
                        }
                    }
                    catch(Exception x)
                    {
                        Debug.Log("Walk Plugin: Exception");
                        Debug.LogException(x);
                    }
                }
                return true;
            }
        }
    }
}