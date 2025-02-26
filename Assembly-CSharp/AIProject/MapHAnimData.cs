using System;
using System.Collections.Generic;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000038 RID: 56
	public class MapHAnimData : ScriptableObject
	{
		// Token: 0x040000D1 RID: 209
		public List<MapHAnimData.Param> param = new List<MapHAnimData.Param>();

		// Token: 0x02000039 RID: 57
		[Serializable]
		public class Param
		{
			// Token: 0x040000D2 RID: 210
			public string AnimName;

			// Token: 0x040000D3 RID: 211
			public int ID;

			// Token: 0x040000D4 RID: 212
			public string MaleAssetBundle;

			// Token: 0x040000D5 RID: 213
			public string MaleAnimator;

			// Token: 0x040000D6 RID: 214
			public string IsMaleHitObject;

			// Token: 0x040000D7 RID: 215
			public string MaleFileMotionNeck;

			// Token: 0x040000D8 RID: 216
			public string FemaleAssetBundle;

			// Token: 0x040000D9 RID: 217
			public string FemaleAnimator;

			// Token: 0x040000DA RID: 218
			public string IsFemaleHitObject;

			// Token: 0x040000DB RID: 219
			public string FemaleFileMotionNeck;

			// Token: 0x040000DC RID: 220
			public int Action;

			// Token: 0x040000DD RID: 221
			public int Control;

			// Token: 0x040000DE RID: 222
			public string Position;

			// Token: 0x040000DF RID: 223
			public string Offset;

			// Token: 0x040000E0 RID: 224
			public string NeedItem;

			// Token: 0x040000E1 RID: 225
			public int DownPtn;

			// Token: 0x040000E2 RID: 226
			public int FaintnessLimit;

			// Token: 0x040000E3 RID: 227
			public string IyaAction;

			// Token: 0x040000E4 RID: 228
			public string CanMerchantMotion;

			// Token: 0x040000E5 RID: 229
			public int Hentai;

			// Token: 0x040000E6 RID: 230
			public int Phase;

			// Token: 0x040000E7 RID: 231
			public int InitiativeFemale;

			// Token: 0x040000E8 RID: 232
			public int FemaleProcivity;

			// Token: 0x040000E9 RID: 233
			public int BackInitiativeID;

			// Token: 0x040000EA RID: 234
			public string System;

			// Token: 0x040000EB RID: 235
			public int isMaleSon;

			// Token: 0x040000EC RID: 236
			public int FemaleUpperCloths0;

			// Token: 0x040000ED RID: 237
			public int FemaleLowerCloths0;

			// Token: 0x040000EE RID: 238
			public int FemaleUpperCloths1;

			// Token: 0x040000EF RID: 239
			public int FemaleLowerCloths1;

			// Token: 0x040000F0 RID: 240
			public int IsFeelHit;

			// Token: 0x040000F1 RID: 241
			public string NameCamera;

			// Token: 0x040000F2 RID: 242
			public string FileSiruPaste;

			// Token: 0x040000F3 RID: 243
			public string FileSE;

			// Token: 0x040000F4 RID: 244
			public int PlayShortBreash;

			// Token: 0x040000F5 RID: 245
			public string VoiceCategory;

			// Token: 0x040000F6 RID: 246
			public int VoiceKindID;

			// Token: 0x040000F7 RID: 247
			public int Iyagari;

			// Token: 0x040000F8 RID: 248
			public int Promiscuity;

			// Token: 0x040000F9 RID: 249
			public string AnimListiD;
		}
	}
}
