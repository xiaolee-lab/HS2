using System;
using AIProject.Animal.Resources;

namespace AIProject.Animal
{
	// Token: 0x02000B4C RID: 2892
	public struct AnimalInfo
	{
		// Token: 0x06005641 RID: 22081 RVA: 0x00258E2F File Offset: 0x0025722F
		public AnimalInfo(AnimalTypes _animalType, BreedingTypes _breedingType, string _name, string _identifierName, int _animalID, int _chunkID, AnimalModelInfo _modelInfo)
		{
			this.AnimalType = _animalType;
			this.BreedingType = _breedingType;
			this.Name = _name;
			this.IdentifierName = _identifierName;
			this.AnimalID = _animalID;
			this.ModelInfo = _modelInfo;
		}

		// Token: 0x04004F9E RID: 20382
		public AnimalTypes AnimalType;

		// Token: 0x04004F9F RID: 20383
		public BreedingTypes BreedingType;

		// Token: 0x04004FA0 RID: 20384
		public string Name;

		// Token: 0x04004FA1 RID: 20385
		public string IdentifierName;

		// Token: 0x04004FA2 RID: 20386
		public int AnimalID;

		// Token: 0x04004FA3 RID: 20387
		public AnimalModelInfo ModelInfo;
	}
}
