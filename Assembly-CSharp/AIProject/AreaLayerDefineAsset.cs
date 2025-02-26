using System;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000BEA RID: 3050
	public class AreaLayerDefineAsset : ScriptableObject
	{
		// Token: 0x06005D35 RID: 23861 RVA: 0x00276386 File Offset: 0x00274786
		public AreaLayerDefineAsset()
		{
			this._values = new string[32];
			this._values[0] = "Normal";
		}

		// Token: 0x06005D36 RID: 23862 RVA: 0x002763B5 File Offset: 0x002747B5
		public string GetLayerName(int layerID)
		{
			if (layerID >= 0 || layerID <= 31)
			{
				return this._values[layerID];
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x06005D37 RID: 23863 RVA: 0x002763D4 File Offset: 0x002747D4
		public int GetLayerByName(string name)
		{
			for (int i = 0; i < this._values.Length; i++)
			{
				string a = this._values[i];
				if (a == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04005396 RID: 21398
		[SerializeField]
		private string[] _values = new string[32];
	}
}
