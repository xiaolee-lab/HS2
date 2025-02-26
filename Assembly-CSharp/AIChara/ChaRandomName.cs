using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace AIChara
{
	// Token: 0x020007C0 RID: 1984
	public class ChaRandomName
	{
		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x0600312E RID: 12590 RVA: 0x00123B0C File Offset: 0x00121F0C
		// (set) Token: 0x0600312F RID: 12591 RVA: 0x00123B14 File Offset: 0x00121F14
		public List<string> lstRandLastNameH { get; set; } = new List<string>();

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06003130 RID: 12592 RVA: 0x00123B1D File Offset: 0x00121F1D
		// (set) Token: 0x06003131 RID: 12593 RVA: 0x00123B25 File Offset: 0x00121F25
		public List<string> lstRandLastNameK { get; set; } = new List<string>();

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x00123B2E File Offset: 0x00121F2E
		// (set) Token: 0x06003133 RID: 12595 RVA: 0x00123B36 File Offset: 0x00121F36
		public List<string> lstRandFirstNameHF { get; set; } = new List<string>();

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x00123B3F File Offset: 0x00121F3F
		// (set) Token: 0x06003135 RID: 12597 RVA: 0x00123B47 File Offset: 0x00121F47
		public List<string> lstRandFirstNameKF { get; set; } = new List<string>();

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x00123B50 File Offset: 0x00121F50
		// (set) Token: 0x06003137 RID: 12599 RVA: 0x00123B58 File Offset: 0x00121F58
		public List<string> lstRandFirstNameHM { get; set; } = new List<string>();

		// Token: 0x1700087B RID: 2171
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x00123B61 File Offset: 0x00121F61
		// (set) Token: 0x06003139 RID: 12601 RVA: 0x00123B69 File Offset: 0x00121F69
		public List<string> lstRandFirstNameKM { get; set; } = new List<string>();

		// Token: 0x1700087C RID: 2172
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x00123B72 File Offset: 0x00121F72
		// (set) Token: 0x0600313B RID: 12603 RVA: 0x00123B7A File Offset: 0x00121F7A
		public List<string> lstRandMiddleName { get; set; } = new List<string>();

		// Token: 0x0600313C RID: 12604 RVA: 0x00123B84 File Offset: 0x00121F84
		public void Initialize()
		{
			List<ExcelData.Param> source = ChaListControl.LoadExcelData("list/characustom/namelist.unity3d", "RandNameList_Name", 2, 1);
			this.lstRandLastNameH = (from x in source
			select x.list[0] into x
			where x != "0" && x != string.Empty
			select x).ToList<string>();
			this.lstRandLastNameK = (from x in source
			select x.list[1] into x
			where x != "0" && x != string.Empty
			select x).ToList<string>();
			this.lstRandFirstNameHF = (from x in source
			select x.list[2] into x
			where x != "0" && x != string.Empty
			select x).ToList<string>();
			this.lstRandFirstNameKF = (from x in source
			select x.list[3] into x
			where x != "0" && x != string.Empty
			select x).ToList<string>();
			this.lstRandFirstNameHM = (from x in source
			select x.list[4] into x
			where x != "0" && x != string.Empty
			select x).ToList<string>();
			this.lstRandFirstNameKM = (from x in source
			select x.list[5] into x
			where x != "0" && x != string.Empty
			select x).ToList<string>();
			this.lstRandMiddleName = (from x in source
			select x.list[6] into x
			where x != "0" && x != string.Empty
			select x).ToList<string>();
		}

		// Token: 0x0600313D RID: 12605 RVA: 0x00123DD4 File Offset: 0x001221D4
		public string GetRandName(byte Sex)
		{
			StringBuilder stringBuilder = new StringBuilder(64);
			if (ChaRandomName.GetRandomIndex(new int[]
			{
				5,
				95
			}) == 0)
			{
				if (ChaRandomName.GetRandomIndex(new int[]
				{
					10,
					90
				}) == 0)
				{
					if (Sex == 0)
					{
						if (this.lstRandFirstNameKM.Count != 0)
						{
							stringBuilder.Append(this.lstRandFirstNameKM[UnityEngine.Random.Range(0, this.lstRandFirstNameKM.Count)]);
						}
					}
					else if (this.lstRandFirstNameKF.Count != 0)
					{
						stringBuilder.Append(this.lstRandFirstNameKF[UnityEngine.Random.Range(0, this.lstRandFirstNameKF.Count)]);
					}
				}
				else if (Sex == 0)
				{
					if (this.lstRandFirstNameHM.Count != 0)
					{
						stringBuilder.Append(this.lstRandFirstNameHM[UnityEngine.Random.Range(0, this.lstRandFirstNameKM.Count)]);
					}
				}
				else if (this.lstRandFirstNameHF.Count != 0)
				{
					stringBuilder.Append(this.lstRandFirstNameHF[UnityEngine.Random.Range(0, this.lstRandFirstNameKF.Count)]);
				}
			}
			else
			{
				if (ChaRandomName.GetRandomIndex(new int[]
				{
					10,
					90
				}) == 0)
				{
					if (Sex == 0)
					{
						if (this.lstRandFirstNameKM.Count != 0)
						{
							stringBuilder.Append(this.lstRandFirstNameKM[UnityEngine.Random.Range(0, this.lstRandFirstNameKM.Count)]);
						}
					}
					else if (this.lstRandFirstNameKF.Count != 0)
					{
						stringBuilder.Append(this.lstRandFirstNameKF[UnityEngine.Random.Range(0, this.lstRandFirstNameKF.Count)]);
					}
					stringBuilder.Append("・");
					string text = string.Empty;
					while (this.lstRandLastNameK.Count != 0)
					{
						text = this.lstRandLastNameK[UnityEngine.Random.Range(0, this.lstRandLastNameK.Count)];
						if (text.Length + stringBuilder.Length < 16)
						{
							IL_214:
							if (string.Empty != text && stringBuilder.Length + text.Length < 10)
							{
								if (ChaRandomName.GetRandomIndex(new int[]
								{
									10,
									90
								}) == 0)
								{
									string value = this.lstRandMiddleName[UnityEngine.Random.Range(0, this.lstRandMiddleName.Count)];
									stringBuilder.Append(value).Append("・").Append(text);
								}
								else
								{
									stringBuilder.Append(text);
								}
							}
							else
							{
								stringBuilder.Append(text);
							}
							goto IL_356;
						}
					}
					goto IL_214;
				}
				if (this.lstRandLastNameH.Count != 0)
				{
					stringBuilder.Append(this.lstRandLastNameH[UnityEngine.Random.Range(0, this.lstRandLastNameH.Count)]);
				}
				stringBuilder.Append(" ");
				if (Sex == 0)
				{
					if (this.lstRandFirstNameHM.Count != 0)
					{
						stringBuilder.Append(this.lstRandFirstNameHM[UnityEngine.Random.Range(0, this.lstRandFirstNameHM.Count)]);
					}
				}
				else if (this.lstRandFirstNameHF.Count != 0)
				{
					stringBuilder.Append(this.lstRandFirstNameHF[UnityEngine.Random.Range(0, this.lstRandFirstNameHF.Count)]);
				}
			}
			IL_356:
			if (stringBuilder.Length == 0)
			{
				return string.Empty;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600313E RID: 12606 RVA: 0x00124150 File Offset: 0x00122550
		public static int GetRandomIndex(params int[] weightTable)
		{
			int num = weightTable.Sum();
			int num2 = UnityEngine.Random.Range(1, num + 1);
			int result = -1;
			for (int i = 0; i < weightTable.Length; i++)
			{
				if (weightTable[i] >= num2)
				{
					result = i;
					break;
				}
				num2 -= weightTable[i];
			}
			return result;
		}
	}
}
