using System;
using ADV.Commands.Base;

namespace ADV.Commands.Chara
{
	// Token: 0x02000713 RID: 1811
	public class Expression : Expression
	{
		// Token: 0x06002B37 RID: 11063 RVA: 0x000FA729 File Offset: 0x000F8B29
		public override void Do()
		{
			Expression.Convert(ref this.args, base.scenario).ForEach(delegate(Expression.Data p)
			{
				p.Play(base.scenario);
			});
		}
	}
}
