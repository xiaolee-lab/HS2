using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Studio
{
	// Token: 0x02001258 RID: 4696
	public class ItemColorData : SerializedScriptableObject
	{
		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x06009AE0 RID: 39648 RVA: 0x003F873C File Offset: 0x003F6B3C
		// (set) Token: 0x06009AE1 RID: 39649 RVA: 0x003F8744 File Offset: 0x003F6B44
		public Dictionary<int, Dictionary<int, Dictionary<int, ItemColorData.ColorData>>> ColorDatas
		{
			get
			{
				return this.colorDatas;
			}
			set
			{
				this.colorDatas = value;
			}
		}

		// Token: 0x04007B81 RID: 31617
		[SerializeField]
		[ReadOnly]
		private Dictionary<int, Dictionary<int, Dictionary<int, ItemColorData.ColorData>>> colorDatas;

		// Token: 0x02001259 RID: 4697
		[Serializable]
		public class ColorData
		{
			// Token: 0x06009AE2 RID: 39650 RVA: 0x003F8750 File Offset: 0x003F6B50
			public ColorData(ItemComponent _itemComponent, ParticleComponent _particleComponent)
			{
				bool[] useColor = _itemComponent.useColor;
				bool flag = _particleComponent != null && _particleComponent.check;
				this.colors = new bool[]
				{
					useColor.SafeGet(0) || flag,
					useColor.SafeGet(1),
					useColor.SafeGet(2),
					_itemComponent.checkGlass
				};
			}

			// Token: 0x06009AE3 RID: 39651 RVA: 0x003F87B5 File Offset: 0x003F6BB5
			public ColorData(ItemColorData.ColorData _src)
			{
				this.colors = new bool[]
				{
					_src.IsColor1,
					_src.IsColor2,
					_src.IsColor3,
					_src.IsColor4
				};
			}

			// Token: 0x1700210F RID: 8463
			// (get) Token: 0x06009AE4 RID: 39652 RVA: 0x003F87ED File Offset: 0x003F6BED
			public bool IsColor1
			{
				[CompilerGenerated]
				get
				{
					return this.colors.SafeGet(0);
				}
			}

			// Token: 0x17002110 RID: 8464
			// (get) Token: 0x06009AE5 RID: 39653 RVA: 0x003F87FB File Offset: 0x003F6BFB
			public bool IsColor2
			{
				[CompilerGenerated]
				get
				{
					return this.colors.SafeGet(1);
				}
			}

			// Token: 0x17002111 RID: 8465
			// (get) Token: 0x06009AE6 RID: 39654 RVA: 0x003F8809 File Offset: 0x003F6C09
			public bool IsColor3
			{
				[CompilerGenerated]
				get
				{
					return this.colors.SafeGet(2);
				}
			}

			// Token: 0x17002112 RID: 8466
			// (get) Token: 0x06009AE7 RID: 39655 RVA: 0x003F8817 File Offset: 0x003F6C17
			public bool IsColor4
			{
				[CompilerGenerated]
				get
				{
					return this.colors.SafeGet(3);
				}
			}

			// Token: 0x17002113 RID: 8467
			// (get) Token: 0x06009AE8 RID: 39656 RVA: 0x003F8825 File Offset: 0x003F6C25
			public int Count
			{
				[CompilerGenerated]
				get
				{
					return this.colors.Count((bool _b) => _b);
				}
			}

			// Token: 0x04007B82 RID: 31618
			[SerializeField]
			[ReadOnly]
			private bool[] colors;
		}
	}
}
