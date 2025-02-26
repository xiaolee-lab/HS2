using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ADV;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x02000971 RID: 2417
	[MessagePackObject(false)]
	public class Skill : ICommandData
	{
		// Token: 0x060043DF RID: 17375 RVA: 0x001A81F4 File Offset: 0x001A65F4
		public Skill()
		{
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x001A8228 File Offset: 0x001A6628
		public Skill(Skill other)
		{
			this.Level = other.Level;
			this.Experience = other.Experience;
			this.Parameter = other.Parameter;
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x001A8289 File Offset: 0x001A6689
		// (set) Token: 0x060043E2 RID: 17378 RVA: 0x001A8291 File Offset: 0x001A6691
		[Key(0)]
		public int Level { get; set; } = 1;

		// Token: 0x17000CF5 RID: 3317
		// (get) Token: 0x060043E3 RID: 17379 RVA: 0x001A829A File Offset: 0x001A669A
		// (set) Token: 0x060043E4 RID: 17380 RVA: 0x001A82A2 File Offset: 0x001A66A2
		[Key(1)]
		public float Experience { get; set; }

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x001A82AB File Offset: 0x001A66AB
		// (set) Token: 0x060043E6 RID: 17382 RVA: 0x001A82B3 File Offset: 0x001A66B3
		[Key(2)]
		public int Parameter { get; set; }

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x060043E7 RID: 17383 RVA: 0x001A82BC File Offset: 0x001A66BC
		// (set) Token: 0x060043E8 RID: 17384 RVA: 0x001A82C4 File Offset: 0x001A66C4
		[IgnoreMember]
		public Func<int, int> CalculationNextExp { get; set; } = (int x) => 100 * x;

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x060043E9 RID: 17385 RVA: 0x001A82CD File Offset: 0x001A66CD
		[IgnoreMember]
		public int NextExperience
		{
			[CompilerGenerated]
			get
			{
				return this.CalculationNextExp(this.Level);
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x060043EA RID: 17386 RVA: 0x001A82E0 File Offset: 0x001A66E0
		// (set) Token: 0x060043EB RID: 17387 RVA: 0x001A82E8 File Offset: 0x001A66E8
		[IgnoreMember]
		public Action<int, float> OnStatsChanged { get; set; }

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x060043EC RID: 17388 RVA: 0x001A82F1 File Offset: 0x001A66F1
		// (set) Token: 0x060043ED RID: 17389 RVA: 0x001A82F9 File Offset: 0x001A66F9
		[IgnoreMember]
		public Action<int, int> OnLevelChanged { get; set; }

		// Token: 0x060043EE RID: 17390 RVA: 0x001A8304 File Offset: 0x001A6704
		public void AddExperience(float exp)
		{
			int num = this.CalculationNextExp(this.Level);
			this.Experience += exp;
			if (Mathf.Sign(exp) > 0f)
			{
				if (this.Experience >= (float)num)
				{
					if (this.Level >= 9999)
					{
						this.Experience = (float)num;
						return;
					}
					float num2 = this.Experience / (float)num;
					while (num2 >= 1f && this.Level < 9999)
					{
						int level = this.Level;
						int num3 = ++this.Level;
						this.Experience -= (float)num;
						num = this.CalculationNextExp(num3);
						num2 = this.Experience / (float)num;
						UnityEngine.Debug.Log(string.Format("レベルアップ： {0} -> {1}", level.ToString(), num3.ToString()));
						Action<int, int> onLevelChanged = this.OnLevelChanged;
						if (onLevelChanged != null)
						{
							onLevelChanged(num3, level);
						}
					}
				}
				UnityEngine.Debug.Log(string.Format("経験値増加: {0}", exp.ToString()));
			}
			else if (this.Experience < 0f)
			{
				if (this.Level <= 1)
				{
					this.Experience = 0f;
					return;
				}
				int num4 = this.CalculationNextExp(this.Level - 1);
				float a = this.Experience / (float)num4;
				while (Mathf.Approximately(a, 0f) && this.Level > 1)
				{
					int level2 = this.Level;
					int num5 = --this.Level;
					num4 = this.CalculationNextExp(num5);
					this.Experience += (float)num4;
					a = this.Experience / (float)num4;
					UnityEngine.Debug.Log(string.Format("レベルダウン： {0} -> {1}", level2.ToString(), num5.ToString()));
					Action<int, int> onLevelChanged2 = this.OnLevelChanged;
					if (onLevelChanged2 != null)
					{
						onLevelChanged2(num5, level2);
					}
				}
			}
			Action<int, float> onStatsChanged = this.OnStatsChanged;
			if (onStatsChanged != null)
			{
				onStatsChanged(this.Level, this.Experience);
			}
		}

		// Token: 0x060043EF RID: 17391 RVA: 0x001A8548 File Offset: 0x001A6948
		public IEnumerable<CommandData> CreateCommandData(string head)
		{
			return new CommandData[]
			{
				new CommandData(CommandData.Command.Int, head + string.Format(".{0}", "Level"), () => this.Level, null),
				new CommandData(CommandData.Command.FLOAT, head + string.Format(".{0}", "Experience"), () => this.Experience, null),
				new CommandData(CommandData.Command.Int, head + string.Format(".{0}", "Parameter"), () => this.Parameter, null)
			};
		}
	}
}
