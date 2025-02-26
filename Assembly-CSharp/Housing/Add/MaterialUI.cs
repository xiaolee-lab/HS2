using System;
using System.Runtime.CompilerServices;
using AIProject;
using AIProject.SaveData;
using Illusion.Extensions;
using Manager;
using UnityEngine;
using UnityEngine.UI;

namespace Housing.Add
{
	// Token: 0x020008A2 RID: 2210
	public class MaterialUI : MonoBehaviour
	{
		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06003968 RID: 14696 RVA: 0x0015212B File Offset: 0x0015052B
		public MaterialUI.MaterialInfo[] MaterialInfos
		{
			[CompilerGenerated]
			get
			{
				return this.materialInfos;
			}
		}

		// Token: 0x06003969 RID: 14697 RVA: 0x00152134 File Offset: 0x00150534
		public bool UpdateUI(Housing.LoadInfo _loadInfo)
		{
			bool flag = true;
			if (_loadInfo.requiredMaterials.IsNullOrEmpty<Housing.RequiredMaterial>())
			{
				foreach (MaterialUI.MaterialInfo materialInfo in this.materialInfos)
				{
					materialInfo.Active = false;
				}
				return true;
			}
			for (int j = 0; j < this.materialInfos.Length; j++)
			{
				Housing.RequiredMaterial requiredMaterial = _loadInfo.requiredMaterials.SafeGet(j);
				if (requiredMaterial == null)
				{
					this.materialInfos[j].Active = false;
				}
				else
				{
					StuffItemInfo item = Singleton<Manager.Resources>.Instance.GameInfo.GetItem(requiredMaterial.category, requiredMaterial.no);
					if (item == null)
					{
						this.materialInfos[j].Active = false;
					}
					else
					{
						int num = 0;
						StuffItem item2 = new StuffItem(requiredMaterial.category, requiredMaterial.no, 0);
						StuffItem stuffItem = Singleton<Map>.Instance.Player.PlayerData.ItemList.FindItem(item2);
						if (stuffItem != null)
						{
							num += stuffItem.Count;
						}
						stuffItem = Singleton<Game>.Instance.Environment.ItemListInStorage.FindItem(item2);
						if (stuffItem != null)
						{
							num += stuffItem.Count;
						}
						flag &= this.materialInfos[j].Set(item.Name, num, requiredMaterial.num);
						this.materialInfos[j].Active = true;
					}
				}
			}
			return flag;
		}

		// Token: 0x0400390F RID: 14607
		[SerializeField]
		private MaterialUI.MaterialInfo[] materialInfos;

		// Token: 0x020008A3 RID: 2211
		[Serializable]
		public class MaterialInfo
		{
			// Token: 0x17000A55 RID: 2645
			// (set) Token: 0x0600396B RID: 14699 RVA: 0x001522A9 File Offset: 0x001506A9
			public bool Active
			{
				set
				{
					this.gameObject.SetActiveIfDifferent(value);
				}
			}

			// Token: 0x0600396C RID: 14700 RVA: 0x001522B8 File Offset: 0x001506B8
			public bool Set(string _name, int _num, int _need)
			{
				bool flag = _num < _need;
				this.gameObject.SetActiveIfDifferent(true);
				this.textName.text = _name;
				this.textNum.text = ((!flag) ? _num.ToString() : string.Format("<color=red>{0}</color>", _num));
				this.textNeed.text = string.Format("/{0}", _need);
				return !flag;
			}

			// Token: 0x04003910 RID: 14608
			public GameObject gameObject;

			// Token: 0x04003911 RID: 14609
			public Text textName;

			// Token: 0x04003912 RID: 14610
			public Text textNum;

			// Token: 0x04003913 RID: 14611
			public Text textNeed;
		}
	}
}
