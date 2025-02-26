using System;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001262 RID: 4706
	[DefaultExecutionOrder(-5)]
	public static class ItemShader
	{
		// Token: 0x06009B87 RID: 39815 RVA: 0x003FB594 File Offset: 0x003F9994
		static ItemShader()
		{
			ItemShader._PatternMask1 = Shader.PropertyToID("_PatternMask1");
			ItemShader._PatternMask2 = Shader.PropertyToID("_PatternMask2");
			ItemShader._PatternMask3 = Shader.PropertyToID("_PatternMask3");
			ItemShader._patternuv1 = Shader.PropertyToID("_patternuv1");
			ItemShader._patternuv2 = Shader.PropertyToID("_patternuv2");
			ItemShader._patternuv3 = Shader.PropertyToID("_patternuv3");
			ItemShader._patternuv1Rotator = Shader.PropertyToID("_patternuv1Rotator");
			ItemShader._patternuv2Rotator = Shader.PropertyToID("_patternuv2Rotator");
			ItemShader._patternuv3Rotator = Shader.PropertyToID("_patternuv3Rotator");
			ItemShader._patternclamp1 = Shader.PropertyToID("_patternclamp1");
			ItemShader._patternclamp2 = Shader.PropertyToID("_patternclamp2");
			ItemShader._patternclamp3 = Shader.PropertyToID("_patternclamp3");
			ItemShader._alpha = Shader.PropertyToID("_alpha");
			ItemShader._EmissionColor = Shader.PropertyToID("_EmissionColor");
			ItemShader._EmissionStrength = Shader.PropertyToID("_EmissionStrength");
			ItemShader._LightCancel = Shader.PropertyToID("_LightCancel");
			ItemShader._MainTex = Shader.PropertyToID("_MainTex");
			ItemShader._Metallic = Shader.PropertyToID("_Metallic");
			ItemShader._Metallic2 = Shader.PropertyToID("_Metallic2");
			ItemShader._Metallic3 = Shader.PropertyToID("_Metallic3");
			ItemShader._Metallic4 = Shader.PropertyToID("_Metallic4");
			ItemShader._Glossiness = Shader.PropertyToID("_Glossiness");
			ItemShader._Glossiness2 = Shader.PropertyToID("_Glossiness2");
			ItemShader._Glossiness3 = Shader.PropertyToID("_Glossiness3");
			ItemShader._Glossiness4 = Shader.PropertyToID("_Glossiness4");
			ItemShader._UsesWaterVolume = Shader.PropertyToID("_UsesWaterVolume");
		}

		// Token: 0x17002138 RID: 8504
		// (get) Token: 0x06009B88 RID: 39816 RVA: 0x003FB790 File Offset: 0x003F9B90
		// (set) Token: 0x06009B89 RID: 39817 RVA: 0x003FB797 File Offset: 0x003F9B97
		public static int _Color { get; private set; } = Shader.PropertyToID("_Color");

		// Token: 0x17002139 RID: 8505
		// (get) Token: 0x06009B8A RID: 39818 RVA: 0x003FB79F File Offset: 0x003F9B9F
		// (set) Token: 0x06009B8B RID: 39819 RVA: 0x003FB7A6 File Offset: 0x003F9BA6
		public static int _Color2 { get; private set; } = Shader.PropertyToID("_Color2");

		// Token: 0x1700213A RID: 8506
		// (get) Token: 0x06009B8C RID: 39820 RVA: 0x003FB7AE File Offset: 0x003F9BAE
		// (set) Token: 0x06009B8D RID: 39821 RVA: 0x003FB7B5 File Offset: 0x003F9BB5
		public static int _Color3 { get; private set; } = Shader.PropertyToID("_Color3");

		// Token: 0x1700213B RID: 8507
		// (get) Token: 0x06009B8E RID: 39822 RVA: 0x003FB7BD File Offset: 0x003F9BBD
		// (set) Token: 0x06009B8F RID: 39823 RVA: 0x003FB7C4 File Offset: 0x003F9BC4
		public static int _Color4 { get; private set; } = Shader.PropertyToID("_Color4");

		// Token: 0x1700213C RID: 8508
		// (get) Token: 0x06009B90 RID: 39824 RVA: 0x003FB7CC File Offset: 0x003F9BCC
		// (set) Token: 0x06009B91 RID: 39825 RVA: 0x003FB7D3 File Offset: 0x003F9BD3
		public static int _PatternMask1 { get; private set; }

		// Token: 0x1700213D RID: 8509
		// (get) Token: 0x06009B92 RID: 39826 RVA: 0x003FB7DB File Offset: 0x003F9BDB
		// (set) Token: 0x06009B93 RID: 39827 RVA: 0x003FB7E2 File Offset: 0x003F9BE2
		public static int _PatternMask2 { get; private set; }

		// Token: 0x1700213E RID: 8510
		// (get) Token: 0x06009B94 RID: 39828 RVA: 0x003FB7EA File Offset: 0x003F9BEA
		// (set) Token: 0x06009B95 RID: 39829 RVA: 0x003FB7F1 File Offset: 0x003F9BF1
		public static int _PatternMask3 { get; private set; }

		// Token: 0x1700213F RID: 8511
		// (get) Token: 0x06009B96 RID: 39830 RVA: 0x003FB7F9 File Offset: 0x003F9BF9
		// (set) Token: 0x06009B97 RID: 39831 RVA: 0x003FB800 File Offset: 0x003F9C00
		public static int _Color1_2 { get; private set; } = Shader.PropertyToID("_Color1_2");

		// Token: 0x17002140 RID: 8512
		// (get) Token: 0x06009B98 RID: 39832 RVA: 0x003FB808 File Offset: 0x003F9C08
		// (set) Token: 0x06009B99 RID: 39833 RVA: 0x003FB80F File Offset: 0x003F9C0F
		public static int _Color2_2 { get; private set; } = Shader.PropertyToID("_Color2_2");

		// Token: 0x17002141 RID: 8513
		// (get) Token: 0x06009B9A RID: 39834 RVA: 0x003FB817 File Offset: 0x003F9C17
		// (set) Token: 0x06009B9B RID: 39835 RVA: 0x003FB81E File Offset: 0x003F9C1E
		public static int _Color3_2 { get; private set; } = Shader.PropertyToID("_Color3_2");

		// Token: 0x17002142 RID: 8514
		// (get) Token: 0x06009B9C RID: 39836 RVA: 0x003FB826 File Offset: 0x003F9C26
		// (set) Token: 0x06009B9D RID: 39837 RVA: 0x003FB82D File Offset: 0x003F9C2D
		public static int _patternuv1 { get; private set; }

		// Token: 0x17002143 RID: 8515
		// (get) Token: 0x06009B9E RID: 39838 RVA: 0x003FB835 File Offset: 0x003F9C35
		// (set) Token: 0x06009B9F RID: 39839 RVA: 0x003FB83C File Offset: 0x003F9C3C
		public static int _patternuv2 { get; private set; }

		// Token: 0x17002144 RID: 8516
		// (get) Token: 0x06009BA0 RID: 39840 RVA: 0x003FB844 File Offset: 0x003F9C44
		// (set) Token: 0x06009BA1 RID: 39841 RVA: 0x003FB84B File Offset: 0x003F9C4B
		public static int _patternuv3 { get; private set; }

		// Token: 0x17002145 RID: 8517
		// (get) Token: 0x06009BA2 RID: 39842 RVA: 0x003FB853 File Offset: 0x003F9C53
		// (set) Token: 0x06009BA3 RID: 39843 RVA: 0x003FB85A File Offset: 0x003F9C5A
		public static int _patternuv1Rotator { get; private set; }

		// Token: 0x17002146 RID: 8518
		// (get) Token: 0x06009BA4 RID: 39844 RVA: 0x003FB862 File Offset: 0x003F9C62
		// (set) Token: 0x06009BA5 RID: 39845 RVA: 0x003FB869 File Offset: 0x003F9C69
		public static int _patternuv2Rotator { get; private set; }

		// Token: 0x17002147 RID: 8519
		// (get) Token: 0x06009BA6 RID: 39846 RVA: 0x003FB871 File Offset: 0x003F9C71
		// (set) Token: 0x06009BA7 RID: 39847 RVA: 0x003FB878 File Offset: 0x003F9C78
		public static int _patternuv3Rotator { get; private set; }

		// Token: 0x17002148 RID: 8520
		// (get) Token: 0x06009BA8 RID: 39848 RVA: 0x003FB880 File Offset: 0x003F9C80
		// (set) Token: 0x06009BA9 RID: 39849 RVA: 0x003FB887 File Offset: 0x003F9C87
		public static int _patternclamp1 { get; private set; }

		// Token: 0x17002149 RID: 8521
		// (get) Token: 0x06009BAA RID: 39850 RVA: 0x003FB88F File Offset: 0x003F9C8F
		// (set) Token: 0x06009BAB RID: 39851 RVA: 0x003FB896 File Offset: 0x003F9C96
		public static int _patternclamp2 { get; private set; }

		// Token: 0x1700214A RID: 8522
		// (get) Token: 0x06009BAC RID: 39852 RVA: 0x003FB89E File Offset: 0x003F9C9E
		// (set) Token: 0x06009BAD RID: 39853 RVA: 0x003FB8A5 File Offset: 0x003F9CA5
		public static int _patternclamp3 { get; private set; }

		// Token: 0x1700214B RID: 8523
		// (get) Token: 0x06009BAE RID: 39854 RVA: 0x003FB8AD File Offset: 0x003F9CAD
		// (set) Token: 0x06009BAF RID: 39855 RVA: 0x003FB8B4 File Offset: 0x003F9CB4
		public static int _alpha { get; private set; }

		// Token: 0x1700214C RID: 8524
		// (get) Token: 0x06009BB0 RID: 39856 RVA: 0x003FB8BC File Offset: 0x003F9CBC
		// (set) Token: 0x06009BB1 RID: 39857 RVA: 0x003FB8C3 File Offset: 0x003F9CC3
		public static int _EmissionColor { get; private set; }

		// Token: 0x1700214D RID: 8525
		// (get) Token: 0x06009BB2 RID: 39858 RVA: 0x003FB8CB File Offset: 0x003F9CCB
		// (set) Token: 0x06009BB3 RID: 39859 RVA: 0x003FB8D2 File Offset: 0x003F9CD2
		public static int _EmissionStrength { get; private set; }

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x06009BB4 RID: 39860 RVA: 0x003FB8DA File Offset: 0x003F9CDA
		// (set) Token: 0x06009BB5 RID: 39861 RVA: 0x003FB8E1 File Offset: 0x003F9CE1
		public static int _LightCancel { get; private set; }

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x06009BB6 RID: 39862 RVA: 0x003FB8E9 File Offset: 0x003F9CE9
		// (set) Token: 0x06009BB7 RID: 39863 RVA: 0x003FB8F0 File Offset: 0x003F9CF0
		public static int _MainTex { get; private set; }

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x06009BB8 RID: 39864 RVA: 0x003FB8F8 File Offset: 0x003F9CF8
		// (set) Token: 0x06009BB9 RID: 39865 RVA: 0x003FB8FF File Offset: 0x003F9CFF
		public static int _Metallic { get; private set; }

		// Token: 0x17002151 RID: 8529
		// (get) Token: 0x06009BBA RID: 39866 RVA: 0x003FB907 File Offset: 0x003F9D07
		// (set) Token: 0x06009BBB RID: 39867 RVA: 0x003FB90E File Offset: 0x003F9D0E
		public static int _Metallic2 { get; private set; }

		// Token: 0x17002152 RID: 8530
		// (get) Token: 0x06009BBC RID: 39868 RVA: 0x003FB916 File Offset: 0x003F9D16
		// (set) Token: 0x06009BBD RID: 39869 RVA: 0x003FB91D File Offset: 0x003F9D1D
		public static int _Metallic3 { get; private set; }

		// Token: 0x17002153 RID: 8531
		// (get) Token: 0x06009BBE RID: 39870 RVA: 0x003FB925 File Offset: 0x003F9D25
		// (set) Token: 0x06009BBF RID: 39871 RVA: 0x003FB92C File Offset: 0x003F9D2C
		public static int _Metallic4 { get; private set; }

		// Token: 0x17002154 RID: 8532
		// (get) Token: 0x06009BC0 RID: 39872 RVA: 0x003FB934 File Offset: 0x003F9D34
		// (set) Token: 0x06009BC1 RID: 39873 RVA: 0x003FB93B File Offset: 0x003F9D3B
		public static int _Glossiness { get; private set; }

		// Token: 0x17002155 RID: 8533
		// (get) Token: 0x06009BC2 RID: 39874 RVA: 0x003FB943 File Offset: 0x003F9D43
		// (set) Token: 0x06009BC3 RID: 39875 RVA: 0x003FB94A File Offset: 0x003F9D4A
		public static int _Glossiness2 { get; private set; }

		// Token: 0x17002156 RID: 8534
		// (get) Token: 0x06009BC4 RID: 39876 RVA: 0x003FB952 File Offset: 0x003F9D52
		// (set) Token: 0x06009BC5 RID: 39877 RVA: 0x003FB959 File Offset: 0x003F9D59
		public static int _Glossiness3 { get; private set; }

		// Token: 0x17002157 RID: 8535
		// (get) Token: 0x06009BC6 RID: 39878 RVA: 0x003FB961 File Offset: 0x003F9D61
		// (set) Token: 0x06009BC7 RID: 39879 RVA: 0x003FB968 File Offset: 0x003F9D68
		public static int _Glossiness4 { get; private set; }

		// Token: 0x17002158 RID: 8536
		// (get) Token: 0x06009BC8 RID: 39880 RVA: 0x003FB970 File Offset: 0x003F9D70
		// (set) Token: 0x06009BC9 RID: 39881 RVA: 0x003FB977 File Offset: 0x003F9D77
		public static int _UsesWaterVolume { get; private set; }
	}
}
