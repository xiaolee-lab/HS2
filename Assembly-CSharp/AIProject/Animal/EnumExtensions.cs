using System;

namespace AIProject.Animal
{
	// Token: 0x02000B78 RID: 2936
	public static class EnumExtensions
	{
		// Token: 0x06005720 RID: 22304 RVA: 0x0025AF45 File Offset: 0x00259345
		public static bool Contains(this ActionTypes source, ActionTypes type)
		{
			return source == (source | type) && type != ActionTypes.None;
		}
	}
}
