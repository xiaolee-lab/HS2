using System;
using System.Collections.Generic;
using UnityEngine;

namespace DeepSky.Haze
{
	// Token: 0x020002E8 RID: 744
	[AddComponentMenu("")]
	[Serializable]
	public class DS_HazeContext
	{
		// Token: 0x06000C96 RID: 3222 RVA: 0x000335E0 File Offset: 0x000319E0
		public DS_HazeContext()
		{
			this.m_ContextItems = new List<DS_HazeContextItem>();
			DS_HazeContextItem ds_HazeContextItem = new DS_HazeContextItem();
			ds_HazeContextItem.m_Name = "Default";
			this.m_ContextItems.Add(ds_HazeContextItem);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000C97 RID: 3223 RVA: 0x00033622 File Offset: 0x00031A22
		public int Solo
		{
			get
			{
				return this.m_SoloItem;
			}
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0003362C File Offset: 0x00031A2C
		public void DuplicateContextItem(int index)
		{
			if (index < 0 || index >= this.m_ContextItems.Count)
			{
				return;
			}
			DS_HazeContextItem ds_HazeContextItem = new DS_HazeContextItem();
			ds_HazeContextItem.CopyFrom(this.m_ContextItems[index]);
			DS_HazeContextItem ds_HazeContextItem2 = ds_HazeContextItem;
			ds_HazeContextItem2.m_Name += "_Copy";
			this.m_ContextItems.Add(ds_HazeContextItem);
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0003368C File Offset: 0x00031A8C
		public void RemoveContextItem(int index)
		{
			if (index < 0 || index >= this.m_ContextItems.Count || this.m_ContextItems.Count == 1)
			{
				return;
			}
			this.m_ContextItems.RemoveAt(index);
			if (this.m_SoloItem == -1)
			{
				return;
			}
			if (this.m_SoloItem == index)
			{
				this.m_SoloItem = -1;
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000336F0 File Offset: 0x00031AF0
		public void MoveContextItemUp(int index)
		{
			if (index < 1 || index >= this.m_ContextItems.Count)
			{
				return;
			}
			DS_HazeContextItem item = this.m_ContextItems[index];
			this.m_ContextItems.RemoveAt(index);
			this.m_ContextItems.Insert(index - 1, item);
			if (this.m_SoloItem == -1)
			{
				return;
			}
			if (this.m_SoloItem == index)
			{
				this.m_SoloItem--;
			}
			else if (this.m_SoloItem == index - 1)
			{
				this.m_SoloItem++;
			}
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00033788 File Offset: 0x00031B88
		public void MoveContextItemDown(int index)
		{
			if (index < 0 || index >= this.m_ContextItems.Count - 1)
			{
				return;
			}
			DS_HazeContextItem item = this.m_ContextItems[index];
			this.m_ContextItems.RemoveAt(index);
			this.m_ContextItems.Insert(index + 1, item);
			if (this.m_SoloItem == -1)
			{
				return;
			}
			if (this.m_SoloItem == index)
			{
				this.m_SoloItem++;
			}
			else if (this.m_SoloItem == index + 1)
			{
				this.m_SoloItem--;
			}
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00033820 File Offset: 0x00031C20
		public DS_HazeContextItem GetContextItemBlended(float time = -1f)
		{
			DS_HazeContextItem ds_HazeContextItem = new DS_HazeContextItem();
			ds_HazeContextItem.CopyFrom(this.m_ContextItems[0]);
			if (this.m_ContextItems.Count == 1)
			{
				return ds_HazeContextItem;
			}
			time = Mathf.Clamp01(time);
			for (int i = 1; i < this.m_ContextItems.Count; i++)
			{
				float dt = this.m_ContextItems[i].m_Weight.Evaluate(time);
				ds_HazeContextItem.Lerp(this.m_ContextItems[i], dt);
			}
			return ds_HazeContextItem;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000338AE File Offset: 0x00031CAE
		public DS_HazeContextItem GetItemAtIndex(int index)
		{
			if (index < 0 || index >= this.m_ContextItems.Count)
			{
				return null;
			}
			return this.m_ContextItems[index];
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000338D8 File Offset: 0x00031CD8
		public void CopyFrom(DS_HazeContext other)
		{
			if (this.m_ContextItems.Count > 0)
			{
				this.m_ContextItems.Clear();
			}
			for (int i = 0; i < other.m_ContextItems.Count; i++)
			{
				DS_HazeContextItem ds_HazeContextItem = new DS_HazeContextItem();
				ds_HazeContextItem.CopyFrom(other.m_ContextItems[i]);
				this.m_ContextItems.Add(ds_HazeContextItem);
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00033944 File Offset: 0x00031D44
		public DS_HazeContextAsset GetContextAsset()
		{
			DS_HazeContextAsset ds_HazeContextAsset = ScriptableObject.CreateInstance<DS_HazeContextAsset>();
			ds_HazeContextAsset.Context.CopyFrom(this);
			ds_HazeContextAsset.Context.m_SoloItem = -1;
			return ds_HazeContextAsset;
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x00033970 File Offset: 0x00031D70
		public string[] GetItemNames()
		{
			string[] array = new string[this.m_ContextItems.Count];
			for (int i = 0; i < this.m_ContextItems.Count; i++)
			{
				array[i] = this.m_ContextItems[i].m_Name;
			}
			return array;
		}

		// Token: 0x04000BA6 RID: 2982
		[SerializeField]
		public List<DS_HazeContextItem> m_ContextItems;

		// Token: 0x04000BA7 RID: 2983
		[SerializeField]
		private int m_SoloItem = -1;
	}
}
