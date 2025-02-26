using System;

namespace ConfigScene
{
	// Token: 0x02000858 RID: 2136
	public class HSystem : BaseSystem
	{
		// Token: 0x06003665 RID: 13925 RVA: 0x00141688 File Offset: 0x0013FA88
		public HSystem(string elementName) : base(elementName)
		{
		}

		// Token: 0x06003666 RID: 13926 RVA: 0x001416F0 File Offset: 0x0013FAF0
		public override void Init()
		{
			this.Visible = true;
			this.Son = true;
			this.Cloth = true;
			this.Accessory = true;
			this.Shoes = true;
			this.Siru = 1;
			this.Urine = false;
			this.Sio = false;
			this.Gloss = true;
			this.FeelingGauge = true;
			this.ActionGuide = true;
			this.MenuIcon = true;
			this.FinishButton = true;
			this.InitCamera = true;
			this.EyeDir0 = false;
			this.NeckDir0 = false;
			this.EyeDir1 = false;
			this.NeckDir1 = false;
		}

		// Token: 0x040036D2 RID: 14034
		public bool Visible = true;

		// Token: 0x040036D3 RID: 14035
		public bool Son = true;

		// Token: 0x040036D4 RID: 14036
		public bool Cloth = true;

		// Token: 0x040036D5 RID: 14037
		public bool Accessory = true;

		// Token: 0x040036D6 RID: 14038
		public bool Shoes = true;

		// Token: 0x040036D7 RID: 14039
		public int Siru = 1;

		// Token: 0x040036D8 RID: 14040
		public bool Urine;

		// Token: 0x040036D9 RID: 14041
		public bool Sio;

		// Token: 0x040036DA RID: 14042
		public bool Gloss = true;

		// Token: 0x040036DB RID: 14043
		public bool FeelingGauge = true;

		// Token: 0x040036DC RID: 14044
		public bool ActionGuide = true;

		// Token: 0x040036DD RID: 14045
		public bool MenuIcon = true;

		// Token: 0x040036DE RID: 14046
		public bool FinishButton = true;

		// Token: 0x040036DF RID: 14047
		public bool InitCamera = true;

		// Token: 0x040036E0 RID: 14048
		public bool EyeDir0;

		// Token: 0x040036E1 RID: 14049
		public bool NeckDir0;

		// Token: 0x040036E2 RID: 14050
		public bool EyeDir1;

		// Token: 0x040036E3 RID: 14051
		public bool NeckDir1;
	}
}
