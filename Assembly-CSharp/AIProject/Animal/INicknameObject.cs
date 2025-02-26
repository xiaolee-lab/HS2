using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B52 RID: 2898
	public interface INicknameObject
	{
		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005663 RID: 22115
		Transform NicknameRoot { get; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005664 RID: 22116
		// (set) Token: 0x06005665 RID: 22117
		bool NicknameEnabled { get; set; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06005666 RID: 22118
		string Nickname { get; }

		// Token: 0x17000FAE RID: 4014
		// (get) Token: 0x06005667 RID: 22119
		// (set) Token: 0x06005668 RID: 22120
		Action ChangeNickNameEvent { get; set; }

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06005669 RID: 22121
		int InstanceID { get; }
	}
}
