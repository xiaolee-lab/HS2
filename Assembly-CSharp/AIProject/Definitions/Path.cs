using System;

namespace AIProject.Definitions
{
	// Token: 0x02000944 RID: 2372
	public static class Path
	{
		// Token: 0x17000C92 RID: 3218
		// (get) Token: 0x06004281 RID: 17025 RVA: 0x001A28FD File Offset: 0x001A0CFD
		public static string SaveDataDirectory { get; } = UserData.Path + "save/";

		// Token: 0x17000C93 RID: 3219
		// (get) Token: 0x06004282 RID: 17026 RVA: 0x001A2904 File Offset: 0x001A0D04
		public static string CharaFileDirectory { get; } = UserData.Path + "chara/";

		// Token: 0x17000C94 RID: 3220
		// (get) Token: 0x06004283 RID: 17027 RVA: 0x001A290B File Offset: 0x001A0D0B
		public static string GlobalSaveDataFileName { get; } = "global.ila";

		// Token: 0x17000C95 RID: 3221
		// (get) Token: 0x06004284 RID: 17028 RVA: 0x001A2912 File Offset: 0x001A0D12
		public static string WorldSaveDataFileName { get; } = "save.ila";

		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x06004285 RID: 17029 RVA: 0x001A2919 File Offset: 0x001A0D19
		public static string GlobalSaveDataFile { get; } = Path.SaveDataDirectory + Path.GlobalSaveDataFileName;

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x06004286 RID: 17030 RVA: 0x001A2920 File Offset: 0x001A0D20
		public static string WorldSaveDataFile { get; } = Path.SaveDataDirectory + Path.WorldSaveDataFileName;

		// Token: 0x04003EF1 RID: 16113
		public const string ProductName = "";

		// Token: 0x04003EF2 RID: 16114
		public const string DefaultManifestName = "abdata";

		// Token: 0x04003EF3 RID: 16115
		public const string SceneCommonBundleDirectory = "scene/common/";

		// Token: 0x04003EF4 RID: 16116
		public const string SceneCommonBundleName = "scene/common/00.unity3d";

		// Token: 0x04003EF5 RID: 16117
		public const string SceneCommonAdd50BundleName = "scene/common/50.unity3d";
	}
}
