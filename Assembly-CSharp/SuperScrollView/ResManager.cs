using System;
using System.Collections.Generic;
using UnityEngine;

namespace SuperScrollView
{
	// Token: 0x020005C8 RID: 1480
	public class ResManager : MonoBehaviour
	{
		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x000BB7FE File Offset: 0x000B9BFE
		public static ResManager Get
		{
			get
			{
				if (ResManager.instance == null)
				{
					ResManager.instance = UnityEngine.Object.FindObjectOfType<ResManager>();
				}
				return ResManager.instance;
			}
		}

		// Token: 0x06002211 RID: 8721 RVA: 0x000BB820 File Offset: 0x000B9C20
		private void InitData()
		{
			this.spriteObjDict.Clear();
			foreach (Sprite sprite in this.spriteObjArray)
			{
				this.spriteObjDict[sprite.name] = sprite;
			}
		}

		// Token: 0x06002212 RID: 8722 RVA: 0x000BB869 File Offset: 0x000B9C69
		private void Awake()
		{
			ResManager.instance = null;
			this.InitData();
		}

		// Token: 0x06002213 RID: 8723 RVA: 0x000BB878 File Offset: 0x000B9C78
		public Sprite GetSpriteByName(string spriteName)
		{
			Sprite result = null;
			if (this.spriteObjDict.TryGetValue(spriteName, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x000BB8A0 File Offset: 0x000B9CA0
		public string GetRandomSpriteName()
		{
			int max = this.spriteObjArray.Length;
			int num = UnityEngine.Random.Range(0, max);
			return this.spriteObjArray[num].name;
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x000BB8CB File Offset: 0x000B9CCB
		public int SpriteCount
		{
			get
			{
				return this.spriteObjArray.Length;
			}
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x000BB8D5 File Offset: 0x000B9CD5
		public Sprite GetSpriteByIndex(int index)
		{
			if (index < 0 || index >= this.spriteObjArray.Length)
			{
				return null;
			}
			return this.spriteObjArray[index];
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x000BB8F6 File Offset: 0x000B9CF6
		public string GetSpriteNameByIndex(int index)
		{
			if (index < 0 || index >= this.spriteObjArray.Length)
			{
				return string.Empty;
			}
			return this.spriteObjArray[index].name;
		}

		// Token: 0x040021C3 RID: 8643
		public Sprite[] spriteObjArray;

		// Token: 0x040021C4 RID: 8644
		private static ResManager instance;

		// Token: 0x040021C5 RID: 8645
		private string[] mWordList;

		// Token: 0x040021C6 RID: 8646
		private Dictionary<string, Sprite> spriteObjDict = new Dictionary<string, Sprite>();
	}
}
