using BepInEx;
using HarmonyLib;

namespace PickAnything
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    [BepInProcess("Variables 2.exe")]
    public class PickAnything : BaseUnityPlugin
    {
        static Harmony instance;
		private void Awake()
        {
            // Plugin startup logic
            Logger.LogInfo($"{PluginInfo.PLUGIN_NAME} {PluginInfo.PLUGIN_VERSION} is loaded.");
			instance = Harmony.CreateAndPatchAll(typeof(PickAnything));
        }

		/*
			// Token: 0x060026E9 RID: 9961 RVA: 0x0007EA4C File Offset: 0x0007CC4C
			public bool Map_CanRemoveConstruct(Construct construct)
			{
				switch (construct.Setting.RemoveType)
				{
				case RemoveTypes.Always:
					return true;
				case RemoveTypes.NewAdded:
					return this.NewAddedSet.Contains(construct);
				case RemoveTypes.Cannot:
					return false;
				default:
					throw new NotImplementedException();
				}
			}
         */
		[HarmonyPatch(typeof(Variables2.Gameplay.Battle.C_ConstructLogic), "Map_CanRemoveConstruct")]
		[HarmonyPostfix]
		public static void Map_CanRemoveConstruct(ref bool __result, Variables2.ConstructSystem.Construct construct)
        {

            switch (construct.Setting.ShowName)
            {
				case "ground":
				case "terrain":
					__result = false;
					break;
				default:
					__result = true;
					break;

			}
        }
	}
}
