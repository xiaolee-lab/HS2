using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000EF6 RID: 3830
public class CraftSelectPartsInfo : MonoBehaviour
{
	// Token: 0x1700188D RID: 6285
	// (get) Token: 0x06007D0F RID: 32015 RVA: 0x00358167 File Offset: 0x00356567
	// (set) Token: 0x06007D10 RID: 32016 RVA: 0x00358174 File Offset: 0x00356574
	public string szItemName
	{
		get
		{
			return this._szItemName.Value;
		}
		set
		{
			this._szItemName.Value = value;
		}
	}

	// Token: 0x06007D11 RID: 32017 RVA: 0x00358184 File Offset: 0x00356584
	private void Start()
	{
		this.ItemName = ((this.Item != null) ? this.Item.GetComponentInChildren<Text>() : null);
		this.szPrevItemName = string.Empty;
		(from x in this._szItemName
		where x != this.szPrevItemName
		select x).Subscribe(delegate(string x)
		{
			this.ChangItemIcon(x);
		});
	}

	// Token: 0x06007D12 RID: 32018 RVA: 0x003581E4 File Offset: 0x003565E4
	private void ChangItemIcon(string name)
	{
		this.szPrevItemName = name;
		this.ItemName.text = name;
	}

	// Token: 0x04006518 RID: 25880
	[SerializeField]
	private Image Item;

	// Token: 0x04006519 RID: 25881
	private Text ItemName;

	// Token: 0x0400651A RID: 25882
	private StringReactiveProperty _szItemName = new StringReactiveProperty(string.Empty);

	// Token: 0x0400651B RID: 25883
	private string szPrevItemName;
}
