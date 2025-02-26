using System;

namespace AIProject
{
	// Token: 0x02000BEB RID: 3051
	public struct AreaLayerMask
	{
		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x06005D38 RID: 23864 RVA: 0x00276412 File Offset: 0x00274812
		// (set) Token: 0x06005D39 RID: 23865 RVA: 0x0027641A File Offset: 0x0027481A
		public int Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06005D3A RID: 23866 RVA: 0x00276423 File Offset: 0x00274823
		public static implicit operator int(AreaLayerMask layerMask)
		{
			return layerMask.Value;
		}

		// Token: 0x06005D3B RID: 23867 RVA: 0x0027642C File Offset: 0x0027482C
		public static implicit operator AreaLayerMask(int value)
		{
			return new AreaLayerMask
			{
				Value = value
			};
		}

		// Token: 0x06005D3C RID: 23868 RVA: 0x0027644A File Offset: 0x0027484A
		public static string LayerToName(int layer)
		{
			return string.Empty;
		}

		// Token: 0x06005D3D RID: 23869 RVA: 0x00276451 File Offset: 0x00274851
		public static int NameToLayer(string name)
		{
			return 0;
		}

		// Token: 0x06005D3E RID: 23870 RVA: 0x00276454 File Offset: 0x00274854
		public static int GetMask(params string[] names)
		{
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			int num = 0;
			foreach (string name in names)
			{
				int num2 = AreaLayerMask.NameToLayer(name);
				if (num2 != -1)
				{
					num |= 1 << num2;
				}
			}
			return num;
		}

		// Token: 0x04005397 RID: 21399
		private int _value;
	}
}
