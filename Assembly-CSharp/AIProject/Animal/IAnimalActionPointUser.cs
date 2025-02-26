using System;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B62 RID: 2914
	public interface IAnimalActionPointUser
	{
		// Token: 0x1700100A RID: 4106
		// (get) Token: 0x060056F2 RID: 22258
		AnimalTypes AnimalType { get; }

		// Token: 0x1700100B RID: 4107
		// (get) Token: 0x060056F3 RID: 22259
		// (set) Token: 0x060056F4 RID: 22260
		Vector3 Position { get; set; }

		// Token: 0x1700100C RID: 4108
		// (get) Token: 0x060056F5 RID: 22261
		// (set) Token: 0x060056F6 RID: 22262
		Quaternion Rotation { get; set; }

		// Token: 0x1700100D RID: 4109
		// (get) Token: 0x060056F7 RID: 22263
		int InstanceID { get; }

		// Token: 0x060056F8 RID: 22264
		void CancelActionPoint();

		// Token: 0x060056F9 RID: 22265
		void MissingActionPoint();
	}
}
