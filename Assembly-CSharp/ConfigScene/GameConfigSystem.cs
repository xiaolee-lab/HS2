using System;

namespace ConfigScene
{
	// Token: 0x02000856 RID: 2134
	public class GameConfigSystem : BaseSystem
	{
		// Token: 0x0600365F RID: 13919 RVA: 0x00140B2C File Offset: 0x0013EF2C
		public GameConfigSystem(string elementName) : base(elementName)
		{
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x00140B84 File Offset: 0x0013EF84
		public override void Init()
		{
			this.FontSpeed = 40;
			this.ReadSkip = true;
			this.NextVoiceStop = true;
			this.AutoWaitTime = 3f;
			this.ChoicesSkip = false;
			this.ChoicesAuto = false;
			this.TextWindowOption = true;
			this.ActionGuide = true;
			this.StoryHelp = true;
			this.MiniMap = true;
			this.CenterPointer = true;
			this.ParameterLock = false;
		}

		// Token: 0x040036B1 RID: 14001
		public int FontSpeed = 40;

		// Token: 0x040036B2 RID: 14002
		public bool ReadSkip = true;

		// Token: 0x040036B3 RID: 14003
		public bool NextVoiceStop = true;

		// Token: 0x040036B4 RID: 14004
		public float AutoWaitTime = 3f;

		// Token: 0x040036B5 RID: 14005
		public bool ChoicesSkip;

		// Token: 0x040036B6 RID: 14006
		public bool ChoicesAuto;

		// Token: 0x040036B7 RID: 14007
		public bool TextWindowOption = true;

		// Token: 0x040036B8 RID: 14008
		public bool ActionGuide = true;

		// Token: 0x040036B9 RID: 14009
		public bool StoryHelp = true;

		// Token: 0x040036BA RID: 14010
		public bool MiniMap = true;

		// Token: 0x040036BB RID: 14011
		public bool CenterPointer = true;

		// Token: 0x040036BC RID: 14012
		public bool ParameterLock;
	}
}
