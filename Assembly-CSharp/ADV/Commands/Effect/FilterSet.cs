using System;
using Illusion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace ADV.Commands.Effect
{
	// Token: 0x0200073B RID: 1851
	public class FilterSet : CommandBase
	{
		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06002BD4 RID: 11220 RVA: 0x000FD247 File Offset: 0x000FB647
		public override string[] ArgsLabel
		{
			get
			{
				return new string[]
				{
					"From",
					"To",
					"Time",
					"Type"
				};
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000FD26F File Offset: 0x000FB66F
		public override string[] ArgsDefault
		{
			get
			{
				return new string[]
				{
					string.Empty,
					"clear",
					"0",
					"front"
				};
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000FD298 File Offset: 0x000FB698
		public override void Do()
		{
			base.Do();
			this.timer = 0f;
			int num = 0;
			this.filterImage = base.scenario.FilterImage;
			Color? colorCheck = this.args[num++].GetColorCheck();
			this.initColor = ((colorCheck == null) ? this.filterImage.color : colorCheck.Value);
			this.color = this.args[num++].GetColor();
			this.time = float.Parse(this.args[num++]);
			num++;
		}

		// Token: 0x06002BD7 RID: 11223 RVA: 0x000FD338 File Offset: 0x000FB738
		public override bool Process()
		{
			base.Process();
			this.timer = Mathf.Min(this.timer + Time.deltaTime, this.time);
			float t = (this.time != 0f) ? Mathf.InverseLerp(0f, this.time, this.timer) : 1f;
			this.filterImage.color = Color.Lerp(this.initColor, this.color, t);
			return this.timer >= this.time;
		}

		// Token: 0x06002BD8 RID: 11224 RVA: 0x000FD3C8 File Offset: 0x000FB7C8
		public override void Result(bool processEnd)
		{
			base.Result(processEnd);
			if (!processEnd)
			{
				this.filterImage.color = this.color;
			}
		}

		// Token: 0x04002B40 RID: 11072
		private const string front = "front";

		// Token: 0x04002B41 RID: 11073
		private Color initColor = Color.clear;

		// Token: 0x04002B42 RID: 11074
		private Color color = Color.clear;

		// Token: 0x04002B43 RID: 11075
		private float time;

		// Token: 0x04002B44 RID: 11076
		private float timer;

		// Token: 0x04002B45 RID: 11077
		private bool isFront = true;

		// Token: 0x04002B46 RID: 11078
		private Image filterImage;
	}
}
