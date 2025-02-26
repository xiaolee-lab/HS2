using System;

namespace ConfigScene
{
	// Token: 0x02000855 RID: 2133
	public class DebugSystem : BaseSystem
	{
		// Token: 0x0600365D RID: 13917 RVA: 0x00140B19 File Offset: 0x0013EF19
		public DebugSystem(string elementName) : base(elementName)
		{
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x00140B22 File Offset: 0x0013EF22
		public override void Init()
		{
			this.FPS = false;
		}

		// Token: 0x040036B0 RID: 14000
		public bool FPS;
	}
}
