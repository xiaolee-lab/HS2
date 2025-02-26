using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000EF7 RID: 3831
public class HouseName : MonoBehaviour
{
	// Token: 0x06007D16 RID: 32022 RVA: 0x00358218 File Offset: 0x00356618
	private void Start()
	{
		if (this.nameTextObj != null)
		{
			this.nameTextObj.GetComponent<Button>().onClick.AddListener(new UnityAction(this.OnClickNameTex));
		}
		if (this.decide != null)
		{
			this.decide.onClick.AddListener(new UnityAction(this.ChangeTex));
		}
		if (this.cancel != null)
		{
			this.cancel.onClick.AddListener(new UnityAction(this.CloseInputForm));
		}
	}

	// Token: 0x06007D17 RID: 32023 RVA: 0x003582B1 File Offset: 0x003566B1
	private void ChangeTex()
	{
		this.nameTextObj.GetComponentInChildren<Text>().text = this.Name.text;
		this.nameInputForm.SetActive(false);
	}

	// Token: 0x06007D18 RID: 32024 RVA: 0x003582DA File Offset: 0x003566DA
	private void CloseInputForm()
	{
		this.nameInputForm.SetActive(false);
	}

	// Token: 0x06007D19 RID: 32025 RVA: 0x003582E8 File Offset: 0x003566E8
	private void OnClickNameTex()
	{
		if (this.nameInputForm != null)
		{
			this.nameInputForm.SetActive(true);
		}
		this.prevNameTextObj.text = this.nameTextObj.GetComponentInChildren<Text>().text;
	}

	// Token: 0x0400651C RID: 25884
	[SerializeField]
	private GameObject nameTextObj;

	// Token: 0x0400651D RID: 25885
	[SerializeField]
	private Text prevNameTextObj;

	// Token: 0x0400651E RID: 25886
	[SerializeField]
	private GameObject nameInputForm;

	// Token: 0x0400651F RID: 25887
	[SerializeField]
	private Text Name;

	// Token: 0x04006520 RID: 25888
	[SerializeField]
	private Button decide;

	// Token: 0x04006521 RID: 25889
	[SerializeField]
	private Button cancel;
}
