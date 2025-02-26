using System;

namespace AIProject.Definitions
{
	// Token: 0x0200092D RID: 2349
	public static class State
	{
		// Token: 0x06004268 RID: 17000 RVA: 0x001A1A7C File Offset: 0x0019FE7C
		public static bool ContainsCommandableTypes(State.Type source)
		{
			foreach (State.Type type in State._commandableTypes)
			{
				if (source == type)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04003D8D RID: 15757
		private static State.Type[] _commandableTypes = new State.Type[]
		{
			State.Type.Sleep,
			State.Type.Toilet,
			State.Type.Bath,
			State.Type.Search,
			State.Type.Cook,
			State.Type.Collapse,
			State.Type.Cold,
			State.Type.Hurt
		};

		// Token: 0x0200092E RID: 2350
		public enum Type
		{
			// Token: 0x04003D8F RID: 15759
			Immobility,
			// Token: 0x04003D90 RID: 15760
			Normal,
			// Token: 0x04003D91 RID: 15761
			Greet,
			// Token: 0x04003D92 RID: 15762
			Commun,
			// Token: 0x04003D93 RID: 15763
			Sleep,
			// Token: 0x04003D94 RID: 15764
			Toilet,
			// Token: 0x04003D95 RID: 15765
			Bath,
			// Token: 0x04003D96 RID: 15766
			Search,
			// Token: 0x04003D97 RID: 15767
			Cook,
			// Token: 0x04003D98 RID: 15768
			Collapse,
			// Token: 0x04003D99 RID: 15769
			Cold,
			// Token: 0x04003D9A RID: 15770
			Hurt
		}
	}
}
