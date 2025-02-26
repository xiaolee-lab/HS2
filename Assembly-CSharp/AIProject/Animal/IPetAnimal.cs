using System;
using AIProject.SaveData;
using UnityEngine;

namespace AIProject.Animal
{
	// Token: 0x02000B50 RID: 2896
	public interface IPetAnimal
	{
		// Token: 0x17000FA1 RID: 4001
		// (get) Token: 0x0600564F RID: 22095
		AnimalTypes AnimalType { get; }

		// Token: 0x17000FA2 RID: 4002
		// (get) Token: 0x06005650 RID: 22096
		int AnimalTypeID { get; }

		// Token: 0x17000FA3 RID: 4003
		// (get) Token: 0x06005651 RID: 22097
		string Name { get; }

		// Token: 0x17000FA4 RID: 4004
		// (get) Token: 0x06005652 RID: 22098
		// (set) Token: 0x06005653 RID: 22099
		string Nickname { get; set; }

		// Token: 0x17000FA5 RID: 4005
		// (get) Token: 0x06005654 RID: 22100
		int AnimalID { get; }

		// Token: 0x17000FA6 RID: 4006
		// (get) Token: 0x06005655 RID: 22101
		// (set) Token: 0x06005656 RID: 22102
		Vector3 Position { get; set; }

		// Token: 0x17000FA7 RID: 4007
		// (get) Token: 0x06005657 RID: 22103
		// (set) Token: 0x06005658 RID: 22104
		Quaternion Rotation { get; set; }

		// Token: 0x17000FA8 RID: 4008
		// (get) Token: 0x06005659 RID: 22105
		// (set) Token: 0x0600565A RID: 22106
		AnimalData AnimalData { get; set; }

		// Token: 0x17000FA9 RID: 4009
		// (get) Token: 0x0600565B RID: 22107
		PetHomePoint HomePoint { get; }

		// Token: 0x0600565C RID: 22108
		void Initialize(PetHomePoint _homePoint);

		// Token: 0x0600565D RID: 22109
		void Initialize(AnimalData animalData);

		// Token: 0x0600565E RID: 22110
		AnimalInfo GetAnimalInfo();

		// Token: 0x0600565F RID: 22111
		void Release();
	}
}
