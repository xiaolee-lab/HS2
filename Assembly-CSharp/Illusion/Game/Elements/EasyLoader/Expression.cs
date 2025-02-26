using System;
using AIChara;

namespace Illusion.Game.Elements.EasyLoader
{
	// Token: 0x0200079E RID: 1950
	[Serializable]
	public class Expression
	{
		// Token: 0x06002E3A RID: 11834 RVA: 0x001056B0 File Offset: 0x00103AB0
		public virtual void Setting(ChaControl chaCtrl, int personality, string name)
		{
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x001056B2 File Offset: 0x00103AB2
		public virtual void Setting(ChaControl chaCtrl)
		{
			this.Setting(chaCtrl, this.personality, this.name);
		}

		// Token: 0x04002D1F RID: 11551
		public int personality;

		// Token: 0x04002D20 RID: 11552
		public string name;
	}
}
