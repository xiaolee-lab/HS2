using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AIProject;
using UnityEngine;

namespace ADV.Commands.Base
{
	// Token: 0x02000701 RID: 1793
	public class Motion : CommandBase
	{
		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x000F7DBC File Offset: 0x000F61BC
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"No",
					"State",
					"Bundle",
					"Asset",
					"IKBundle",
					"IKAsset",
					"ShakeBundle",
					"ShakeAsset",
					"OverrideBundle",
					"OverrideAsset",
					"LayerNo"
				};
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x000F7E2C File Offset: 0x000F622C
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					int.MaxValue.ToString()
				};
			}
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000F7E58 File Offset: 0x000F6258
		public static List<Motion.Data> Convert(ref string[] args, TextScenario scenario, int argsLen)
		{
			List<Motion.Data> list = new List<Motion.Data>();
			if (args.Length > 1)
			{
				int num = 0;
				while (!args.IsNullOrEmpty(num))
				{
					string check = null;
					args.SafeProc(num + 1, delegate(string s)
					{
						check = s;
					});
					string[] array;
					if (check != null && scenario.commandController.motionDic.TryGetValue(check, out array))
					{
						string[] array2 = Enumerable.Repeat<string>(string.Empty, argsLen - 1).ToArray<string>();
						int num2 = Mathf.Min(array2.Length, array.Length);
						for (int i = 0; i < num2; i++)
						{
							if (array[i] != null)
							{
								array2[i] = array[i];
							}
						}
						args = args.Take(num + 1).Concat(array2).Concat(args.Skip(num + 2)).ToArray<string>();
					}
					list.Add(new Motion.Data(args, ref num));
				}
			}
			return list;
		}

		// Token: 0x06002AC8 RID: 10952 RVA: 0x000F7F58 File Offset: 0x000F6358
		public override void Do()
		{
			base.Do();
			base.scenario.currentCharaData.CreateMotionList();
			base.scenario.currentCharaData.motionList.Add(Motion.Convert(ref this.args, base.scenario, this.ArgsLabel.Length).ToArray());
		}

		// Token: 0x02000702 RID: 1794
		public class Data : TextScenario.IMotion, TextScenario.IChara
		{
			// Token: 0x06002AC9 RID: 10953 RVA: 0x000F7FB0 File Offset: 0x000F63B0
			public Data(string[] args, ref int cnt)
			{
				try
				{
					this.no = int.Parse(args[cnt++]);
					this.stateName = args[cnt++];
					this.assetBundleName = args.SafeGet(cnt++);
					this.assetName = args.SafeGet(cnt++);
					this.ikAssetBundleName = args.SafeGet(cnt++);
					this.ikAssetName = args.SafeGet(cnt++);
					this.shakeAssetBundleName = args.SafeGet(cnt++);
					this.shakeAssetName = args.SafeGet(cnt++);
					this.overrideAssetBundleName = args.SafeGet(cnt++);
					this.overrideAssetName = args.SafeGet(cnt++);
					args.SafeProc(cnt++, delegate(string s)
					{
						IEnumerable<string> source = s.Split(new char[]
						{
							','
						});
						if (Motion.Data.<>f__mg$cache0 == null)
						{
							Motion.Data.<>f__mg$cache0 = new Func<string, int>(int.Parse);
						}
						this.layerNo = source.Select(Motion.Data.<>f__mg$cache0).ToArray<int>();
					});
					bool flag = false;
					int postureID;
					flag |= int.TryParse(this.stateName, out postureID);
					int poseID;
					flag |= int.TryParse(this.assetBundleName, out poseID);
					if (flag)
					{
						this.pair = new PoseKeyPair?(new PoseKeyPair
						{
							postureID = postureID,
							poseID = poseID
						});
					}
				}
				catch (Exception)
				{
				}
			}

			// Token: 0x06002ACA RID: 10954 RVA: 0x000F8124 File Offset: 0x000F6524
			public Motion.Data[] Get()
			{
				string[] array = this.stateName.Split(new char[]
				{
					','
				});
				if (array.Length == 1)
				{
					return new Motion.Data[]
					{
						this
					};
				}
				List<string[]> list = new List<string[]>();
				list.Add(array);
				list.Add(this.assetBundleName.Split(new char[]
				{
					','
				}));
				list.Add(this.assetName.Split(new char[]
				{
					','
				}));
				list.Add(this.ikAssetBundleName.Split(new char[]
				{
					','
				}));
				list.Add(this.ikAssetName.Split(new char[]
				{
					','
				}));
				list.Add(this.shakeAssetBundleName.Split(new char[]
				{
					','
				}));
				list.Add(this.shakeAssetName.Split(new char[]
				{
					','
				}));
				list.Add(this.overrideAssetBundleName.Split(new char[]
				{
					','
				}));
				list.Add(this.overrideAssetName.Split(new char[]
				{
					','
				}));
				List<string[]> row = new List<string[]>();
				for (int j = 0; j < array.Length; j++)
				{
					List<string> list2 = new List<string>();
					list2.Add(this.no.ToString());
					foreach (string[] array2 in list)
					{
						list2.Add(array2.SafeGet(j) ?? string.Empty);
					}
					list2.Add(string.Join(",", (from no in this.layerNo
					select no.ToString()).ToArray<string>()));
					row.Add(list2.ToArray());
				}
				return (from i in Enumerable.Range(0, array.Length)
				select new Motion.Data(row[i], ref i)).ToArray<Motion.Data>();
			}

			// Token: 0x170006A0 RID: 1696
			// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000F8360 File Offset: 0x000F6760
			// (set) Token: 0x06002ACC RID: 10956 RVA: 0x000F8368 File Offset: 0x000F6768
			public int no { get; private set; }

			// Token: 0x170006A1 RID: 1697
			// (get) Token: 0x06002ACD RID: 10957 RVA: 0x000F8371 File Offset: 0x000F6771
			// (set) Token: 0x06002ACE RID: 10958 RVA: 0x000F8379 File Offset: 0x000F6779
			public string stateName { get; private set; }

			// Token: 0x170006A2 RID: 1698
			// (get) Token: 0x06002ACF RID: 10959 RVA: 0x000F8382 File Offset: 0x000F6782
			// (set) Token: 0x06002AD0 RID: 10960 RVA: 0x000F838A File Offset: 0x000F678A
			public string assetBundleName { get; private set; }

			// Token: 0x170006A3 RID: 1699
			// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x000F8393 File Offset: 0x000F6793
			// (set) Token: 0x06002AD2 RID: 10962 RVA: 0x000F839B File Offset: 0x000F679B
			public string assetName { get; private set; }

			// Token: 0x170006A4 RID: 1700
			// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000F83A4 File Offset: 0x000F67A4
			// (set) Token: 0x06002AD4 RID: 10964 RVA: 0x000F83AC File Offset: 0x000F67AC
			public string ikAssetBundleName { get; private set; }

			// Token: 0x170006A5 RID: 1701
			// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x000F83B5 File Offset: 0x000F67B5
			// (set) Token: 0x06002AD6 RID: 10966 RVA: 0x000F83BD File Offset: 0x000F67BD
			public string ikAssetName { get; private set; }

			// Token: 0x170006A6 RID: 1702
			// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000F83C6 File Offset: 0x000F67C6
			// (set) Token: 0x06002AD8 RID: 10968 RVA: 0x000F83CE File Offset: 0x000F67CE
			public string shakeAssetBundleName { get; private set; }

			// Token: 0x170006A7 RID: 1703
			// (get) Token: 0x06002AD9 RID: 10969 RVA: 0x000F83D7 File Offset: 0x000F67D7
			// (set) Token: 0x06002ADA RID: 10970 RVA: 0x000F83DF File Offset: 0x000F67DF
			public string shakeAssetName { get; private set; }

			// Token: 0x170006A8 RID: 1704
			// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000F83E8 File Offset: 0x000F67E8
			// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000F83F0 File Offset: 0x000F67F0
			public string overrideAssetBundleName { get; private set; }

			// Token: 0x170006A9 RID: 1705
			// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000F83F9 File Offset: 0x000F67F9
			// (set) Token: 0x06002ADE RID: 10974 RVA: 0x000F8401 File Offset: 0x000F6801
			public string overrideAssetName { get; private set; }

			// Token: 0x170006AA RID: 1706
			// (get) Token: 0x06002ADF RID: 10975 RVA: 0x000F840A File Offset: 0x000F680A
			// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x000F8412 File Offset: 0x000F6812
			public int[] layerNo { get; private set; }

			// Token: 0x170006AB RID: 1707
			// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x000F841B File Offset: 0x000F681B
			// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x000F8423 File Offset: 0x000F6823
			public PoseKeyPair? pair { get; private set; }

			// Token: 0x06002AE3 RID: 10979 RVA: 0x000F842C File Offset: 0x000F682C
			public void Play(TextScenario scenario)
			{
				this.GetChara(scenario).MotionPlay(this, false);
			}

			// Token: 0x06002AE4 RID: 10980 RVA: 0x000F843C File Offset: 0x000F683C
			public CharaData GetChara(TextScenario scenario)
			{
				return scenario.commandController.GetChara(this.no);
			}

			// Token: 0x04002B11 RID: 11025
			[CompilerGenerated]
			private static Func<string, int> <>f__mg$cache0;
		}
	}
}
