using System;

namespace CTS
{
	// Token: 0x02000684 RID: 1668
	public static class CTSConstants
	{
		// Token: 0x06002737 RID: 10039 RVA: 0x000E65F4 File Offset: 0x000E49F4
		public static int GetTextureSize(CTSConstants.TextureSize size)
		{
			switch (size)
			{
			case CTSConstants.TextureSize.Texture_64:
				return 64;
			case CTSConstants.TextureSize.Texture_128:
				return 128;
			case CTSConstants.TextureSize.Texture_256:
				return 256;
			case CTSConstants.TextureSize.Texture_512:
				return 512;
			case CTSConstants.TextureSize.Texture_1024:
				return 1024;
			case CTSConstants.TextureSize.Texture_2048:
				return 2048;
			case CTSConstants.TextureSize.Texture_4096:
				return 4096;
			case CTSConstants.TextureSize.Texture_8192:
				return 8192;
			default:
				return 0;
			}
		}

		// Token: 0x04002764 RID: 10084
		public static readonly int MajorVersion = 1;

		// Token: 0x04002765 RID: 10085
		public static readonly int MinorVersion = 8;

		// Token: 0x04002766 RID: 10086
		public static readonly string CTSPresentSymbol = "CTS_PRESENT";

		// Token: 0x04002767 RID: 10087
		public const string CTSShaderName = "CTS/CTS Terrain";

		// Token: 0x04002768 RID: 10088
		public const string CTSShaderMeshBlenderName = "CTS/CTS_Model_Blend";

		// Token: 0x04002769 RID: 10089
		public const string CTSShaderMeshBlenderAdvancedName = "CTS/CTS_Model_Blend_Advanced";

		// Token: 0x0400276A RID: 10090
		public const string CTSShaderLiteName = "CTS/CTS Terrain Shader Lite";

		// Token: 0x0400276B RID: 10091
		public const string CTSShaderBasicName = "CTS/CTS Terrain Shader Basic";

		// Token: 0x0400276C RID: 10092
		public const string CTSShaderBasicCutoutName = "CTS/CTS Terrain Shader Basic CutOut";

		// Token: 0x0400276D RID: 10093
		public const string CTSShaderAdvancedName = "CTS/CTS Terrain Shader Advanced";

		// Token: 0x0400276E RID: 10094
		public const string CTSShaderAdvancedCutoutName = "CTS/CTS Terrain Shader Advanced CutOut";

		// Token: 0x0400276F RID: 10095
		public const string CTSShaderTesselatedName = "CTS/CTS Terrain Shader Advanced Tess";

		// Token: 0x04002770 RID: 10096
		public const string CTSShaderTesselatedCutoutName = "CTS/CTS Terrain Shader Advanced Tess CutOut";

		// Token: 0x02000685 RID: 1669
		public enum ShaderType
		{
			// Token: 0x04002772 RID: 10098
			Unity,
			// Token: 0x04002773 RID: 10099
			Basic,
			// Token: 0x04002774 RID: 10100
			Advanced,
			// Token: 0x04002775 RID: 10101
			Tesselation,
			// Token: 0x04002776 RID: 10102
			Lite
		}

		// Token: 0x02000686 RID: 1670
		public enum ShaderMode
		{
			// Token: 0x04002778 RID: 10104
			DesignTime,
			// Token: 0x04002779 RID: 10105
			RunTime
		}

		// Token: 0x02000687 RID: 1671
		public enum AOType
		{
			// Token: 0x0400277B RID: 10107
			None,
			// Token: 0x0400277C RID: 10108
			NormalMapBased,
			// Token: 0x0400277D RID: 10109
			TextureBased
		}

		// Token: 0x02000688 RID: 1672
		public enum TextureSize
		{
			// Token: 0x0400277F RID: 10111
			Texture_64,
			// Token: 0x04002780 RID: 10112
			Texture_128,
			// Token: 0x04002781 RID: 10113
			Texture_256,
			// Token: 0x04002782 RID: 10114
			Texture_512,
			// Token: 0x04002783 RID: 10115
			Texture_1024,
			// Token: 0x04002784 RID: 10116
			Texture_2048,
			// Token: 0x04002785 RID: 10117
			Texture_4096,
			// Token: 0x04002786 RID: 10118
			Texture_8192
		}

		// Token: 0x02000689 RID: 1673
		public enum TextureType
		{
			// Token: 0x04002788 RID: 10120
			Albedo,
			// Token: 0x04002789 RID: 10121
			Normal,
			// Token: 0x0400278A RID: 10122
			AmbientOcclusion,
			// Token: 0x0400278B RID: 10123
			Height,
			// Token: 0x0400278C RID: 10124
			Splat,
			// Token: 0x0400278D RID: 10125
			Emission
		}

		// Token: 0x0200068A RID: 1674
		public enum TextureChannel
		{
			// Token: 0x0400278F RID: 10127
			R,
			// Token: 0x04002790 RID: 10128
			G,
			// Token: 0x04002791 RID: 10129
			B,
			// Token: 0x04002792 RID: 10130
			A
		}

		// Token: 0x0200068B RID: 1675
		[Flags]
		public enum TerrainChangedFlags
		{
			// Token: 0x04002794 RID: 10132
			NoChange = 0,
			// Token: 0x04002795 RID: 10133
			Heightmap = 1,
			// Token: 0x04002796 RID: 10134
			TreeInstances = 2,
			// Token: 0x04002797 RID: 10135
			DelayedHeightmapUpdate = 4,
			// Token: 0x04002798 RID: 10136
			FlushEverythingImmediately = 8,
			// Token: 0x04002799 RID: 10137
			RemoveDirtyDetailsImmediately = 16,
			// Token: 0x0400279A RID: 10138
			WillBeDestroyed = 256
		}
	}
}
