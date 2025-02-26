using System;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ADV.Commands.Effect
{
	// Token: 0x0200073A RID: 1850
	public class Filter : CommandBase
	{
		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002BD0 RID: 11216 RVA: 0x000FD18A File Offset: 0x000FB58A
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"Type",
					"Color",
					"Time"
				};
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002BD1 RID: 11217 RVA: 0x000FD1AA File Offset: 0x000FB5AA
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					"front",
					"clear",
					"0"
				};
			}
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x000FD1CC File Offset: 0x000FB5CC
		public override void Do()
		{
			base.Do();
			int num = 0;
			num++;
			this.color = this.args[num++].GetColor();
			num++;
			this.filterImage = base.scenario.FilterImage;
			this.filterImage.color = this.color;
		}

		// Token: 0x04002B3B RID: 11067
		private const string front = "front";

		// Token: 0x04002B3C RID: 11068
		private bool isFront = true;

		// Token: 0x04002B3D RID: 11069
		private float time;

		// Token: 0x04002B3E RID: 11070
		private Color color = Color.clear;

		// Token: 0x04002B3F RID: 11071
		private Image filterImage;
	}
}
