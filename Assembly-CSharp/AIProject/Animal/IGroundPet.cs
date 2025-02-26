using System;

namespace AIProject.Animal
{
	// Token: 0x02000B51 RID: 2897
	public interface IGroundPet : IPetAnimal
	{
		// Token: 0x06005660 RID: 22112
		void ReturnHome();

		// Token: 0x17000FAA RID: 4010
		// (get) Token: 0x06005661 RID: 22113
		// (set) Token: 0x06005662 RID: 22114
		bool ChaseActor { get; set; }
	}
}
