using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Color = System.Drawing.Color;
using Random = UnityEngine.Random;

namespace WrestlingEmpireTemplateMod
{
    [BepInPlugin(PluginGuid, PluginName, PluginVer)]
    [HarmonyPatch]
    public class Plugin : BaseUnityPlugin
    {
        public const string PluginGuid = "IngoH.WrestlingEmpire.WrestlingEmpireKeybindCheats";
        public const string PluginName = "WrestlingEmpireKeybindCheats";
        public const string PluginVer = "1.0.0";

        internal static ManualLogSource Log;
        internal readonly static Harmony Harmony = new(PluginGuid);

        internal static string PluginPath;
        

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
        
        private static float _explosiondelay = Time.time;
        
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
                INNAMALBFPL.PHMKNKGAFGL(3, 1,  new UnityEngine.Color(1f, Random.Range(0.3f,0.7f), 0f), size, null, Random.Range(-40,40f), Random.Range(-10,10f), Random.Range(-40,40f), 0f, 0f, 0.1f);
                if (!Input.GetKey(KeyCode.LeftShift) && !Input.GetKey(KeyCode.RightShift))
                {
                    _explosiondelay = Time.time + 0.1f;
                }
            }
            for (int i = (int)KeyCode.Alpha0; i <= (int)KeyCode.Alpha9; i++)
            {
                if (Input.GetKeyDown((KeyCode)i) && SceneManager.GetActiveScene().name == "Calendar")
                {
                    HJBJPCDFLGL.COCJBGIIEKO();
                    HJBJPCDFLGL.CPPCEJDKCFM = 1501;
                    HJBJPCDFLGL.HEFPEGMGIEP = Characters.fedData[((i - (int)KeyCode.Alpha1 + 10) % 10) + 1].booker;
                    KPGIEHHDIDA.BANOJFCLKIM(70);
                }
            }

            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.End) && SceneManager.GetActiveScene().name == "Game")
            {
                foreach (var cd in NODLGBMEKCI.NMOJJPKABCC)
                {
                    if (cd == null) continue;
                    cd.DOJIAJKMOEI /= 2;
                }
            }
            
            if (Input.GetKey(KeyCode.LeftAlt) && Input.GetKeyDown(KeyCode.I) && SceneManager.GetActiveScene().name == "Game")
            {
                var cd = NODLGBMEKCI.NMOJJPKABCC.Where(cd => cd != null).OrderBy(c => Guid.NewGuid()).First();
                cd.BBOMLOGMHDO(Random.Range(0, 4), int.MaxValue);
            }
        }
        
        [HarmonyPatch(typeof(FDPCGNBMNLJ), "GHPGPDGEJMK")]
        [HarmonyPostfix]
        private static void FDPCGNBMNLJ_GHPGPDGEJMK(ref int __result)
        {
            if (Input.GetKey(KeyCode.N)) {
                __result = 2;
            }
        }
    }
}