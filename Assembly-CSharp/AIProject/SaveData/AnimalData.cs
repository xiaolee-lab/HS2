using System;
using System.Collections.Generic;
using System.Linq;
using AIProject.Animal;
using MessagePack;
using UnityEngine;

namespace AIProject.SaveData
{
	// Token: 0x02000975 RID: 2421
	[MessagePackObject(false)]
	public class AnimalData
	{
		// Token: 0x06004409 RID: 17417 RVA: 0x001A88C8 File Offset: 0x001A6CC8
		public AnimalData()
		{
		}

		// Token: 0x0600440A RID: 17418 RVA: 0x001A8938 File Offset: 0x001A6D38
		public AnimalData(AnimalData data)
		{
			this.Copy(data);
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x0600440B RID: 17419 RVA: 0x001A89AC File Offset: 0x001A6DAC
		// (set) Token: 0x0600440C RID: 17420 RVA: 0x001A89B4 File Offset: 0x001A6DB4
		[Key(0)]
		public int AnimalID { get; set; }

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x0600440D RID: 17421 RVA: 0x001A89BD File Offset: 0x001A6DBD
		// (set) Token: 0x0600440E RID: 17422 RVA: 0x001A89C5 File Offset: 0x001A6DC5
		[Key(1)]
		public int RegisterID { get; set; } = -1;

		// Token: 0x17000D04 RID: 3332
		// (get) Token: 0x0600440F RID: 17423 RVA: 0x001A89CE File Offset: 0x001A6DCE
		// (set) Token: 0x06004410 RID: 17424 RVA: 0x001A89D6 File Offset: 0x001A6DD6
		[Key(2)]
		public AnimalTypes AnimalType { get; set; }

		// Token: 0x17000D05 RID: 3333
		// (get) Token: 0x06004411 RID: 17425 RVA: 0x001A89DF File Offset: 0x001A6DDF
		// (set) Token: 0x06004412 RID: 17426 RVA: 0x001A89E7 File Offset: 0x001A6DE7
		[Key(3)]
		public BreedingTypes BreedingType { get; set; } = BreedingTypes.Pet;

		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x06004413 RID: 17427 RVA: 0x001A89F0 File Offset: 0x001A6DF0
		// (set) Token: 0x06004414 RID: 17428 RVA: 0x001A89F8 File Offset: 0x001A6DF8
		[Key(4)]
		public string Nickname { get; set; } = string.Empty;

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x06004415 RID: 17429 RVA: 0x001A8A01 File Offset: 0x001A6E01
		// (set) Token: 0x06004416 RID: 17430 RVA: 0x001A8A09 File Offset: 0x001A6E09
		[Key(5)]
		public int ItemCategoryID { get; set; } = -1;

		// Token: 0x17000D08 RID: 3336
		// (get) Token: 0x06004417 RID: 17431 RVA: 0x001A8A12 File Offset: 0x001A6E12
		// (set) Token: 0x06004418 RID: 17432 RVA: 0x001A8A1A File Offset: 0x001A6E1A
		[Key(6)]
		public int ItemID { get; set; } = -1;

		// Token: 0x17000D09 RID: 3337
		// (get) Token: 0x06004419 RID: 17433 RVA: 0x001A8A23 File Offset: 0x001A6E23
		// (set) Token: 0x0600441A RID: 17434 RVA: 0x001A8A2B File Offset: 0x001A6E2B
		[Key(7)]
		public int ModelID { get; set; }

		// Token: 0x17000D0A RID: 3338
		// (get) Token: 0x0600441B RID: 17435 RVA: 0x001A8A34 File Offset: 0x001A6E34
		// (set) Token: 0x0600441C RID: 17436 RVA: 0x001A8A3C File Offset: 0x001A6E3C
		[Key(8)]
		public Vector3 Position { get; set; } = Vector3.zero;

		// Token: 0x17000D0B RID: 3339
		// (get) Token: 0x0600441D RID: 17437 RVA: 0x001A8A45 File Offset: 0x001A6E45
		// (set) Token: 0x0600441E RID: 17438 RVA: 0x001A8A4D File Offset: 0x001A6E4D
		[Key(9)]
		public Quaternion Rotation { get; set; } = Quaternion.identity;

		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x0600441F RID: 17439 RVA: 0x001A8A56 File Offset: 0x001A6E56
		// (set) Token: 0x06004420 RID: 17440 RVA: 0x001A8A5E File Offset: 0x001A6E5E
		[Key(10)]
		public Dictionary<int, float> Favorability { get; set; } = new Dictionary<int, float>();

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06004421 RID: 17441 RVA: 0x001A8A67 File Offset: 0x001A6E67
		// (set) Token: 0x06004422 RID: 17442 RVA: 0x001A8A6F File Offset: 0x001A6E6F
		[Key(11)]
		public List<AnimalData.ColorData> ColorList { get; set; } = new List<AnimalData.ColorData>();

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06004423 RID: 17443 RVA: 0x001A8A78 File Offset: 0x001A6E78
		// (set) Token: 0x06004424 RID: 17444 RVA: 0x001A8A80 File Offset: 0x001A6E80
		[Key(12)]
		public bool First { get; set; }

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06004425 RID: 17445 RVA: 0x001A8A89 File Offset: 0x001A6E89
		// (set) Token: 0x06004426 RID: 17446 RVA: 0x001A8A91 File Offset: 0x001A6E91
		[Key(13)]
		public int TextureID { get; set; }

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06004427 RID: 17447 RVA: 0x001A8A9A File Offset: 0x001A6E9A
		// (set) Token: 0x06004428 RID: 17448 RVA: 0x001A8AA2 File Offset: 0x001A6EA2
		[Key(14)]
		public int AnimalTypeID { get; set; } = -1;

		// Token: 0x17000D11 RID: 3345
		// (get) Token: 0x06004429 RID: 17449 RVA: 0x001A8AAB File Offset: 0x001A6EAB
		// (set) Token: 0x0600442A RID: 17450 RVA: 0x001A8AB3 File Offset: 0x001A6EB3
		[Key(15)]
		public bool InitAnimalTypeID { get; set; }

		// Token: 0x0600442B RID: 17451 RVA: 0x001A8ABC File Offset: 0x001A6EBC
		public void Copy(AnimalData data)
		{
			if (data == null)
			{
				return;
			}
			this.AnimalID = data.AnimalID;
			this.RegisterID = data.RegisterID;
			this.AnimalType = data.AnimalType;
			this.BreedingType = data.BreedingType;
			this.Nickname = data.Nickname;
			this.ItemCategoryID = data.ItemCategoryID;
			this.ItemID = data.ItemID;
			this.ModelID = data.ModelID;
			this.Position = data.Position;
			this.Rotation = data.Rotation;
			if (!data.Favorability.IsNullOrEmpty<int, float>())
			{
				this.Favorability = data.Favorability.ToDictionary((KeyValuePair<int, float> x) => x.Key, (KeyValuePair<int, float> x) => x.Value);
			}
			else
			{
				this.Favorability.Clear();
			}
			if (!data.ColorList.IsNullOrEmpty<AnimalData.ColorData>())
			{
				this.ColorList = (from x in data.ColorList
				where x != null
				select new AnimalData.ColorData(x)).ToList<AnimalData.ColorData>();
			}
			else
			{
				this.ColorList.Clear();
			}
			this.First = data.First;
			this.TextureID = data.TextureID;
			this.AnimalTypeID = data.AnimalTypeID;
			this.InitAnimalTypeID = data.InitAnimalTypeID;
		}

		// Token: 0x0600442C RID: 17452 RVA: 0x001A8C58 File Offset: 0x001A7058
		public float AddFavorability(int actorID, float value)
		{
			float num;
			if (!this.Favorability.TryGetValue(actorID, out num))
			{
				num = 0f;
			}
			float num2 = Mathf.Clamp(num + value, 0f, 100f);
			float num3 = num2;
			this.Favorability[actorID] = num3;
			return num3;
		}

		// Token: 0x0600442D RID: 17453 RVA: 0x001A8CA1 File Offset: 0x001A70A1
		public void SetFavorability(int actorID, float value)
		{
			this.Favorability[actorID] = Mathf.Clamp(value, 0f, 100f);
		}

		// Token: 0x0600442E RID: 17454 RVA: 0x001A8CC0 File Offset: 0x001A70C0
		public float GetFavorability(int actorID)
		{
			float num;
			return (!this.Favorability.TryGetValue(actorID, out num)) ? 0f : num;
		}

		// Token: 0x0600442F RID: 17455 RVA: 0x001A8CEB File Offset: 0x001A70EB
		public bool RemoveFavorability(int actorID)
		{
			return this.Favorability.Remove(actorID);
		}

		// Token: 0x06004430 RID: 17456 RVA: 0x001A8CFC File Offset: 0x001A70FC
		public void GetFavorabilityKeyPairs(ref List<KeyValuePair<int, float>> list)
		{
			if (list == null)
			{
				return;
			}
			foreach (KeyValuePair<int, float> item in this.Favorability)
			{
				list.Add(item);
			}
		}

		// Token: 0x06004431 RID: 17457 RVA: 0x001A8D64 File Offset: 0x001A7164
		public bool MostFavorabilityActor(int actorID)
		{
			float num = -1f;
			int num2 = -1;
			foreach (KeyValuePair<int, float> keyValuePair in this.Favorability)
			{
				if (num < keyValuePair.Value)
				{
					num = keyValuePair.Value;
					num2 = keyValuePair.Key;
				}
			}
			return actorID == num2 && 0f <= num;
		}

		// Token: 0x02000976 RID: 2422
		[MessagePackObject(false)]
		public class ColorData
		{
			// Token: 0x06004436 RID: 17462 RVA: 0x001A8E17 File Offset: 0x001A7217
			public ColorData()
			{
			}

			// Token: 0x06004437 RID: 17463 RVA: 0x001A8E2A File Offset: 0x001A722A
			public ColorData(AnimalData.ColorData data)
			{
				this.Copy(data);
			}

			// Token: 0x17000D12 RID: 3346
			// (get) Token: 0x06004438 RID: 17464 RVA: 0x001A8E44 File Offset: 0x001A7244
			// (set) Token: 0x06004439 RID: 17465 RVA: 0x001A8E4C File Offset: 0x001A724C
			[Key(0)]
			public float r { get; set; }

			// Token: 0x17000D13 RID: 3347
			// (get) Token: 0x0600443A RID: 17466 RVA: 0x001A8E55 File Offset: 0x001A7255
			// (set) Token: 0x0600443B RID: 17467 RVA: 0x001A8E5D File Offset: 0x001A725D
			[Key(1)]
			public float g { get; set; }

			// Token: 0x17000D14 RID: 3348
			// (get) Token: 0x0600443C RID: 17468 RVA: 0x001A8E66 File Offset: 0x001A7266
			// (set) Token: 0x0600443D RID: 17469 RVA: 0x001A8E6E File Offset: 0x001A726E
			[Key(2)]
			public float b { get; set; }

			// Token: 0x17000D15 RID: 3349
			// (get) Token: 0x0600443E RID: 17470 RVA: 0x001A8E77 File Offset: 0x001A7277
			// (set) Token: 0x0600443F RID: 17471 RVA: 0x001A8E7F File Offset: 0x001A727F
			[Key(3)]
			public float a { get; set; } = 1f;

			// Token: 0x06004440 RID: 17472 RVA: 0x001A8E88 File Offset: 0x001A7288
			public void Copy(AnimalData.ColorData data)
			{
				if (data == null)
				{
					return;
				}
				this.r = data.r;
				this.g = data.g;
				this.b = data.b;
				this.a = data.a;
			}
		}
	}
}
