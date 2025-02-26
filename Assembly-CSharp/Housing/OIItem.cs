using System;
using System.Runtime.CompilerServices;
using MessagePack;
using UnityEngine;

namespace Housing
{
	// Token: 0x02000892 RID: 2194
	[MessagePackObject(false)]
	public class OIItem : IObjectInfo
	{
		// Token: 0x060038F9 RID: 14585 RVA: 0x00150BF0 File Offset: 0x0014EFF0
		public OIItem()
		{
			this.Color1 = Color.white;
			this.Color2 = Color.white;
			this.Color3 = Color.white;
			this.EmissionColor = Color.clear;
			this.VisibleOption = true;
		}

		// Token: 0x060038FA RID: 14586 RVA: 0x00150C54 File Offset: 0x0014F054
		public OIItem(OIItem _src)
		{
			this.Pos = _src.Pos;
			this.Rot = _src.Rot;
			this.Category = _src.Category;
			this.ID = _src.ID;
			this.Color1 = _src.Color1;
			this.Color2 = _src.Color2;
			this.Color3 = _src.Color3;
			this.EmissionColor = _src.EmissionColor;
			this.VisibleOption = _src.VisibleOption;
		}

		// Token: 0x060038FB RID: 14587 RVA: 0x00150CF0 File Offset: 0x0014F0F0
		public OIItem(OIItem _src, bool _idCopy)
		{
			this.Pos = _src.Pos;
			this.Rot = _src.Rot;
			this.Category = _src.Category;
			this.ID = _src.ID;
			if (_idCopy)
			{
				this.ActionPoints = this.Clone(_src.ActionPoints);
				this.FarmPoints = this.Clone(_src.FarmPoints);
				this.PetHomePoints = this.Clone(_src.PetHomePoints);
				this.JukePoints = this.Clone(_src.JukePoints);
				this.CraftPoints = this.Clone(_src.CraftPoints);
				this.LightSwitchPoints = this.Clone(_src.LightSwitchPoints);
			}
			this.Color1 = _src.Color1;
			this.Color2 = _src.Color2;
			this.Color3 = _src.Color3;
			this.EmissionColor = _src.EmissionColor;
			this.VisibleOption = _src.VisibleOption;
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060038FC RID: 14588 RVA: 0x00150DFE File Offset: 0x0014F1FE
		[IgnoreMember]
		public int Kind
		{
			[CompilerGenerated]
			get
			{
				return 0;
			}
		}

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x060038FD RID: 14589 RVA: 0x00150E01 File Offset: 0x0014F201
		// (set) Token: 0x060038FE RID: 14590 RVA: 0x00150E09 File Offset: 0x0014F209
		[Key(0)]
		public Vector3 Pos { get; set; } = Vector3.zero;

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x060038FF RID: 14591 RVA: 0x00150E12 File Offset: 0x0014F212
		// (set) Token: 0x06003900 RID: 14592 RVA: 0x00150E1A File Offset: 0x0014F21A
		[Key(1)]
		public Vector3 Rot { get; set; } = Vector3.zero;

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x06003901 RID: 14593 RVA: 0x00150E23 File Offset: 0x0014F223
		// (set) Token: 0x06003902 RID: 14594 RVA: 0x00150E2B File Offset: 0x0014F22B
		[Key(2)]
		public int Category { get; set; }

		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x06003903 RID: 14595 RVA: 0x00150E34 File Offset: 0x0014F234
		// (set) Token: 0x06003904 RID: 14596 RVA: 0x00150E3C File Offset: 0x0014F23C
		[Key(3)]
		public int ID { get; set; }

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003905 RID: 14597 RVA: 0x00150E45 File Offset: 0x0014F245
		// (set) Token: 0x06003906 RID: 14598 RVA: 0x00150E4D File Offset: 0x0014F24D
		[Key(4)]
		public int[] ActionPoints { get; set; }

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003907 RID: 14599 RVA: 0x00150E56 File Offset: 0x0014F256
		// (set) Token: 0x06003908 RID: 14600 RVA: 0x00150E5E File Offset: 0x0014F25E
		[Key(5)]
		public int[] FarmPoints { get; set; }

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003909 RID: 14601 RVA: 0x00150E67 File Offset: 0x0014F267
		// (set) Token: 0x0600390A RID: 14602 RVA: 0x00150E6F File Offset: 0x0014F26F
		[Key(6)]
		public Color Color1 { get; set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x0600390B RID: 14603 RVA: 0x00150E78 File Offset: 0x0014F278
		// (set) Token: 0x0600390C RID: 14604 RVA: 0x00150E80 File Offset: 0x0014F280
		[Key(7)]
		public Color Color2 { get; set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600390D RID: 14605 RVA: 0x00150E89 File Offset: 0x0014F289
		// (set) Token: 0x0600390E RID: 14606 RVA: 0x00150E91 File Offset: 0x0014F291
		[Key(8)]
		public Color Color3 { get; set; }

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x00150E9A File Offset: 0x0014F29A
		// (set) Token: 0x06003910 RID: 14608 RVA: 0x00150EA2 File Offset: 0x0014F2A2
		[Key(9)]
		public Color EmissionColor { get; set; }

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x00150EAB File Offset: 0x0014F2AB
		// (set) Token: 0x06003912 RID: 14610 RVA: 0x00150EB3 File Offset: 0x0014F2B3
		[Key(10)]
		public int[] PetHomePoints { get; set; }

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x00150EBC File Offset: 0x0014F2BC
		// (set) Token: 0x06003914 RID: 14612 RVA: 0x00150EC4 File Offset: 0x0014F2C4
		[Key(11)]
		public int[] JukePoints { get; set; }

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x00150ECD File Offset: 0x0014F2CD
		// (set) Token: 0x06003916 RID: 14614 RVA: 0x00150ED5 File Offset: 0x0014F2D5
		[Key(12)]
		public int[] CraftPoints { get; set; }

		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003917 RID: 14615 RVA: 0x00150EDE File Offset: 0x0014F2DE
		// (set) Token: 0x06003918 RID: 14616 RVA: 0x00150EE6 File Offset: 0x0014F2E6
		[Key(13)]
		public bool VisibleOption { get; set; } = true;

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x06003919 RID: 14617 RVA: 0x00150EEF File Offset: 0x0014F2EF
		// (set) Token: 0x0600391A RID: 14618 RVA: 0x00150EF7 File Offset: 0x0014F2F7
		[Key(14)]
		public int[] LightSwitchPoints { get; set; }

		// Token: 0x0600391B RID: 14619 RVA: 0x00150F00 File Offset: 0x0014F300
		private int[] Clone(int[] _src)
		{
			if (_src.IsNullOrEmpty<int>())
			{
				return null;
			}
			int[] array = new int[_src.Length];
			Array.Copy(_src, array, _src.Length);
			return array;
		}
	}
}
