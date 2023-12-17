using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using WrestlingEmpireCameraControls.Internal;
using Color = System.Drawing.Color;
using Random = UnityEngine.Random;

namespace WrestlingEmpireKeybindCheats
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "IngoH.WrestlingEmpire.WrestlingEmpireKeybindCheats";
        public const string PluginName = "WrestlingEmpireKeybindCheats";
        public const string PluginVer = "1.5.0";

        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginGuid);

        internal static string PluginPath;

        enum TargetMode
        {
            TARGET_ALL,
            TARGET_PARTICIPANTS,
            TARGET_NON_PARTICIPANTS,
            TARGET_PLAYERS,
            TARGET_AI,
            TARGET_RANDOM,
            TARGET_RANDOM_PARTICIPANT,
            TARGET_RANDOM_NON_PARTICIPANT,
            TARGET_RANDOM_PLAYER,
            TARGET_RANDOM_AI
        }

        private static TargetMode targetMode = TargetMode.TARGET_ALL;
        

        private void Awake()
        {
            Plugin.Log = base.Logger;

            PluginPath = Path.GetDirectoryName(Info.Location);
        }

        private void OnEnable()
        {
            Harmony.PatchAll();
            Logger.LogInfo($"Loaded {PluginName}!");
        }

        private void OnDisable()
        {
            Harmony.UnpatchSelf();
            Logger.LogInfo($"Unloaded {PluginName}!");
        }
        
        private static float _explosiondelay;
        
        private void Update()
        {
            if (Input.GetKey(KeyCode.E) && Input.GetKey(KeyCode.X) && SceneManager.GetActiveScene().name == "Game" && Time.time > _explosiondelay)
            {
                var size = Random.Range(7f, 10f);
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    size *= 2;
                }
                if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
                {
                    size *= 4;
                }
                ALIGLHEIAGO.MDFJMAEDJMG(3, 1,  new UnityEngine.Color(1f, Random.Range(0.3f,0.7f), 0f), size, null, Random.Range(-40,40f), Random.Range(-10,10f), Random.Range(-40,40f), 0f, 0f, 0.1f);
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                {
                    _explosiondelay = Time.time + 0.1f;
                }
            }
            for (int i = (int)KeyCode.Alpha0; i <= (int)KeyCode.Alpha9; i++)
            {
                if (Input.GetKeyDown((KeyCode)i) && SceneManager.GetActiveScene().name == "Calendar")
                {
                    MappedPromo.FindMeetings();
                    MappedPromo.meeting = 1501;
                    MappedPromo.meetChar = Characters.fedData[((i - (int)KeyCode.Alpha1 + 10) % 10) + 1].booker;
                    MappedMenus.ChangeScreen(70);
                }
            }

            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.End) && SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedPlayer cd in GetTargets())
                {
                    if (cd == null) continue;
                    cd.health /= 2;
                    cd.hp = 0;
                    if (MappedGlobals.Rnd(1, 2) == 1)
                    {
                        cd.SellBackFall();
                    }
                    else
                    {
                        cd.SellFrontFall();
                    }
                }
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.I) && SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedPlayer cd in GetTargets())
                {
                    cd.RiskInjury(Random.Range(0, 4), int.MaxValue);
                    if (MappedGlobals.Rnd(1, 2) == 1)
                    {
                        cd.SellBackFall();
                    }
                    else
                    {
                        cd.SellFrontFall();
                    }
                }
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Alpha1) && SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedPlayer cd in GetTargets())
                {
                    if (cd == null) continue;
                    cd.health = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ? 0 : 5000 * MappedGlobals.optLength;
                    cd.healthLimit = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ? 0 : 5000 * MappedGlobals.optLength;
                }
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Alpha2) && SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedPlayer cd in GetTargets())
                {
                    if (cd == null) continue;
                    cd.spirit = Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl) ? 0 : 1000;
                }
            }
                    
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Alpha3) && SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedPlayer cd in GetTargets())
                {
                    if (cd == null) continue;
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        cd.toilet = -19;
                        cd.EmptyBowels();
                    }
                    else
                    {
                        cd.toilet = 19;
                        cd.EmptyBowels();
                    }
                }
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Alpha4) && SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedPlayer cd in GetTargets())
                {
                    if (cd == null) continue;
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        cd.toilet = 0;
                    }
                    else
                    {
                        MappedSound.Emit(cd.voice, MappedSound.drown[MappedGlobals.Rnd(1, 3)], cd.charData.voice);
                        MappedSound.Emit(cd.audio, MappedSound.splash, -0.1f, 0.5f);
                        cd.Spurt(3, 101, new UnityEngine.Color(0.9f, Random.Range(0.6f, 0.9f), 0.3f),
                            Random.Range(5f, 8f), 3, 0f, 0f, 0f, cd.a, 0.25f * cd.scale, 0.1f);
                        cd.health /= 2f;
                        cd.spirit /= 2f;
                        cd.blind = -MappedGlobals.Rnd(100, 300);
                        cd.dt = Mathf.Abs(cd.blind);
                        if (cd.anim == 0 || cd.anim == 10)
                        {
                            cd.ChangeAnim(805);
                        }

                        if (cd.id == MappedPlayers.star)
                        {
                            MappedPromo.RiskFeedback(55, 0, Progress.fed);
                        }
                    }
                }
            }

            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Alpha5) &&
                SceneManager.GetActiveScene().name == "Game")
            {
                if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                {
                    MappedItems.Add(0);
                }
                else
                {
                    MappedWeapons.Add(0);
                }
                
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Alpha6) &&
                SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedWeapon w in MappedWeapons.weap)
                {
                    if (w == null) continue;
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        w.fireTim = 0;
                        if (w.fire != null)
                        {
                            w.fire.SetActive(false);
                        }
                    }
                    else
                    {
                        w.Ignite();
                    }
                }

                foreach (MappedItem i in MappedItems.item)
                {
                    if (i == null) continue;
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        i.fireTim = 0;
                        if (i.fire != null)
                        {
                            i.fire.SetActive(false);
                        }
                    }
                    else
                    {
                        i.Break();
                    }
                }
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Alpha7) &&
                SceneManager.GetActiveScene().name == "Game")
            {
                foreach (MappedItem i in MappedItems.item)
                {
                    if (i == null) continue;
                    if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
                    {
                        if (i.state < 0)
                        {
                            if (i.anim == 22)
                            {
                                i.anim = 21;
                            }
                            else
                            {
                                i.anim = 20;
                            }

                            i.animTim = 0f;
                            i.state = 1;
                        }
                    }
                    else {
                        if (i.state >= 0)
                        {
                            i.Break();
                        }
                    }
                }
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Comma) &&
                SceneManager.GetActiveScene().name == "Game")
            {
                targetMode = (TargetMode) (((int) targetMode + Enum.GetNames(typeof(TargetMode)).Length - 1) % Enum.GetNames(typeof(TargetMode)).Length);
                MappedMatch.PostComment($"Target mode: {targetMode.ToString().Replace("TARGET_", "").Replace("NON_", "NON-").Replace("_", " ")}");
            }
            
            if ((Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt)) && Input.GetKeyDown(KeyCode.Period) &&
                SceneManager.GetActiveScene().name == "Game")
            {
                targetMode = (TargetMode) (((int) targetMode + 1) % Enum.GetNames(typeof(TargetMode)).Length);
                MappedMatch.PostComment($"Target mode: {targetMode.ToString().Replace("TARGET_", "").Replace("NON_", "NON-").Replace("_", " ")}");
            }
        }

        private List<MappedPlayer> GetTargets()
        {
            switch (targetMode)
            {
                case TargetMode.TARGET_ALL:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer) p).ToList();
                case TargetMode.TARGET_PARTICIPANTS:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.Competing() > 0).ToList();
                case TargetMode.TARGET_NON_PARTICIPANTS:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.Competing() == 0).ToList();
                case TargetMode.TARGET_PLAYERS:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.control >= 0).ToList();
                case TargetMode.TARGET_AI:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.control < 0).ToList();
                case TargetMode.TARGET_RANDOM:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).OrderBy(c => Guid.NewGuid()).Take(1).ToList();
                case TargetMode.TARGET_RANDOM_PARTICIPANT:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.Competing() > 0).OrderBy(c => Guid.NewGuid()).Take(1).ToList();
                case TargetMode.TARGET_RANDOM_NON_PARTICIPANT:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.Competing() == 0).OrderBy(c => Guid.NewGuid()).Take(1).ToList();
                case TargetMode.TARGET_RANDOM_PLAYER:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.control >= 0).OrderBy(c => Guid.NewGuid()).Take(1).ToList();
                case TargetMode.TARGET_RANDOM_AI:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).Where(p => p != null && p.control < 0).OrderBy(c => Guid.NewGuid()).Take(1).ToList();
                default:
                    return MappedPlayers.p.Skip(1).Select(p => (MappedPlayer)p).ToList();
            }
        }
        
        // Career verdict
        [HarmonyPatch(typeof(HJMNFNHBABO), nameof(HJMNFNHBABO.FAHFIJABANL))]
        [HarmonyPostfix]
        private static void HJMNFNHBABO_FAHFIJABANL(ref int __result)
        {
            if (Input.GetKey(KeyCode.N)) {
                __result = 2;
            }
        }
        
        // Booking verdict
        [HarmonyPatch(typeof(HJMNFNHBABO), nameof(HJMNFNHBABO.CBMFPPKBNHH))]
        [HarmonyPostfix]
        private static void HJMNFNHBABO_CBMFPPKBNHH(ref int __result)
        {
            if (Input.GetKey(KeyCode.N)) {
                __result = 2;
            }
        }
        
        // Match verdict
        [HarmonyPatch(typeof(NEGAFEHECNL), nameof(NEGAFEHECNL.LBPJGAGBCLP))]
        [HarmonyPostfix]
        private static void NEGAFEHECNL_LBPJGAGBCLP(ref int __result)
        {
            if (Input.GetKey(KeyCode.N)) {
                __result = 1;
            }
        }
        
        // Partner verdict
        [HarmonyPatch(typeof(NEGAFEHECNL), nameof(NEGAFEHECNL.PMKDGGFHOEO))]
        [HarmonyPostfix]
        private static void NEGAFEHECNL_PMKDGGFHOEO(ref int __result)
        {
            if (Input.GetKey(KeyCode.N)) {
                __result = 1;
            }
        }
        
        // Manager verdict
        [HarmonyPatch(typeof(NEGAFEHECNL), nameof(NEGAFEHECNL.GFNGNOGAMBL))]
        [HarmonyPostfix]
        private static void NEGAFEHECNL_GFNGNOGAMBL(ref int __result)
        {
            if (Input.GetKey(KeyCode.N)) {
                __result = 1;
            }
        }
    }
}