using System;
using System.Collections.Generic;

namespace GameLoadCharaFileSystem
{
	// Token: 0x02000874 RID: 2164
	public class GameCharaFileInfo
	{
		// Token: 0x040037A9 RID: 14249
		public GameCharaFileInfoComponent fic;

		// Token: 0x040037AA RID: 14250
		public int index;

		// Token: 0x040037AB RID: 14251
		public string FullPath = string.Empty;

		// Token: 0x040037AC RID: 14252
		public string FileName = string.Empty;

		// Token: 0x040037AD RID: 14253
		public DateTime time;

		// Token: 0x040037AE RID: 14254
		public string name = string.Empty;

		// Token: 0x040037AF RID: 14255
		public string personality = string.Empty;

		// Token: 0x040037B0 RID: 14256
		public int voice;

		// Token: 0x040037B1 RID: 14257
		public int height;

		// Token: 0x040037B2 RID: 14258
		public int bustSize;

		// Token: 0x040037B3 RID: 14259
		public int hair;

		// Token: 0x040037B4 RID: 14260
		public int bloodType;

		// Token: 0x040037B5 RID: 14261
		public int birthMonth = 1;

		// Token: 0x040037B6 RID: 14262
		public int birthDay = 1;

		// Token: 0x040037B7 RID: 14263
		public string strBirthDay = string.Empty;

		// Token: 0x040037B8 RID: 14264
		public int sex;

		// Token: 0x040037B9 RID: 14265
		public int[] usePackage;

		// Token: 0x040037BA RID: 14266
		public byte[] pngData;

		// Token: 0x040037BB RID: 14267
		public bool gameRegistration;

		// Token: 0x040037BC RID: 14268
		public Dictionary<int, int> flavorState = new Dictionary<int, int>();

		// Token: 0x040037BD RID: 14269
		public int phase;

		// Token: 0x040037BE RID: 14270
		public Dictionary<int, int> normalSkill = new Dictionary<int, int>();

		// Token: 0x040037BF RID: 14271
		public Dictionary<int, int> hSkill = new Dictionary<int, int>();

		// Token: 0x040037C0 RID: 14272
		public int favoritePlace;

		// Token: 0x040037C1 RID: 14273
		public bool futanari;

		// Token: 0x040037C2 RID: 14274
		public int lifeStyle = -1;

		// Token: 0x040037C3 RID: 14275
		public bool isInSaveData;

		// Token: 0x040037C4 RID: 14276
		public string data_uuid = string.Empty;

		// Token: 0x040037C5 RID: 14277
		public CategoryKind cateKind = CategoryKind.Female;
	}
}
