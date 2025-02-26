using System;
using System.Collections.Generic;

namespace CharaCustom
{
	// Token: 0x020009A4 RID: 2468
	public class CustomCharaFileInfo
	{
		// Token: 0x04004215 RID: 16917
		public CustomCharaScrollViewInfo cssvi;

		// Token: 0x04004216 RID: 16918
		public int index;

		// Token: 0x04004217 RID: 16919
		public string FullPath = string.Empty;

		// Token: 0x04004218 RID: 16920
		public string FileName = string.Empty;

		// Token: 0x04004219 RID: 16921
		public DateTime time;

		// Token: 0x0400421A RID: 16922
		public string name = string.Empty;

		// Token: 0x0400421B RID: 16923
		public string personality = string.Empty;

		// Token: 0x0400421C RID: 16924
		public int type;

		// Token: 0x0400421D RID: 16925
		public int height;

		// Token: 0x0400421E RID: 16926
		public int bustSize;

		// Token: 0x0400421F RID: 16927
		public int hair;

		// Token: 0x04004220 RID: 16928
		public int birthMonth = 1;

		// Token: 0x04004221 RID: 16929
		public int birthDay = 1;

		// Token: 0x04004222 RID: 16930
		public string strBirthDay = string.Empty;

		// Token: 0x04004223 RID: 16931
		public int lifestyle = -1;

		// Token: 0x04004224 RID: 16932
		public int pheromone;

		// Token: 0x04004225 RID: 16933
		public int reliability;

		// Token: 0x04004226 RID: 16934
		public int reason;

		// Token: 0x04004227 RID: 16935
		public int instinct;

		// Token: 0x04004228 RID: 16936
		public int dirty;

		// Token: 0x04004229 RID: 16937
		public int wariness;

		// Token: 0x0400422A RID: 16938
		public int sociability;

		// Token: 0x0400422B RID: 16939
		public int darkness;

		// Token: 0x0400422C RID: 16940
		public int skill_n01 = -1;

		// Token: 0x0400422D RID: 16941
		public int skill_n02 = -1;

		// Token: 0x0400422E RID: 16942
		public int skill_n03 = -1;

		// Token: 0x0400422F RID: 16943
		public int skill_n04 = -1;

		// Token: 0x04004230 RID: 16944
		public int skill_n05 = -1;

		// Token: 0x04004231 RID: 16945
		public int skill_h01 = -1;

		// Token: 0x04004232 RID: 16946
		public int skill_h02 = -1;

		// Token: 0x04004233 RID: 16947
		public int skill_h03 = -1;

		// Token: 0x04004234 RID: 16948
		public int skill_h04 = -1;

		// Token: 0x04004235 RID: 16949
		public int skill_h05 = -1;

		// Token: 0x04004236 RID: 16950
		public int wish_01 = -1;

		// Token: 0x04004237 RID: 16951
		public int wish_02 = -1;

		// Token: 0x04004238 RID: 16952
		public int wish_03 = -1;

		// Token: 0x04004239 RID: 16953
		public int sex;

		// Token: 0x0400423A RID: 16954
		public byte[] pngData;

		// Token: 0x0400423B RID: 16955
		public bool gameRegistration;

		// Token: 0x0400423C RID: 16956
		public Dictionary<int, int> flavorState = new Dictionary<int, int>();

		// Token: 0x0400423D RID: 16957
		public int phase;

		// Token: 0x0400423E RID: 16958
		public Dictionary<int, int> normalSkill = new Dictionary<int, int>();

		// Token: 0x0400423F RID: 16959
		public Dictionary<int, int> hSkill = new Dictionary<int, int>();

		// Token: 0x04004240 RID: 16960
		public int favoritePlace;

		// Token: 0x04004241 RID: 16961
		public bool futanari;

		// Token: 0x04004242 RID: 16962
		public bool isInSaveData;

		// Token: 0x04004243 RID: 16963
		public string data_uuid = string.Empty;

		// Token: 0x04004244 RID: 16964
		public CharaCategoryKind cateKind = CharaCategoryKind.Female;
	}
}
