using System;
using AIChara;
using AIProject;
using Manager;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000B0F RID: 2831
public class HSceneSpriteChaChoice : MonoBehaviour
{
	// Token: 0x06005315 RID: 21269 RVA: 0x00245E94 File Offset: 0x00244294
	public void Init()
	{
		this.hScene = Singleton<HSceneFlagCtrl>.Instance.GetComponent<HScene>();
		this.hSceneManager = Singleton<HSceneManager>.Instance;
		this.females = this.hScene.GetFemales();
		this.Males = this.hScene.GetMales();
		this.actor = new Actor[4];
		for (int i = 0; i < this.charlist.Length; i++)
		{
			int no = i;
			bool flag;
			if (no < 2)
			{
				flag = (this.females[no] != null && this.females[no].objTop != null);
			}
			else
			{
				flag = (this.Males[no - 2] != null && this.Males[no - 2].objTop != null);
			}
			this.charlist[no].gameObject.SetActive(flag);
			if (flag)
			{
				if (no < 2)
				{
					this.actor[no] = this.females[no].GetComponentInParent<Actor>();
				}
				else
				{
					this.actor[no] = this.Males[no - 2].GetComponentInParent<Actor>();
				}
				if (no == 0)
				{
					Text labelText = this.LabelText;
					string text = (!this.actor[no]) ? this.charlist[no].name : this.actor[no].CharaName;
					this.charlist[no].GetComponentInChildren<Text>().text = text;
					labelText.text = text;
				}
				else
				{
					this.charlist[no].GetComponentInChildren<Text>().text = ((!this.actor[no]) ? this.charlist[no].name : this.actor[no].CharaName);
				}
			}
			this.charlist[no].onClick.AddListener(delegate()
			{
				this.hSceneManager.numFemaleClothCustom = no;
				this.OpenCharList[0].gameObject.SetActive(true);
				this.OpenCharList[1].gameObject.SetActive(false);
				this.Content.SetActive(false);
				this.LabelText.text = ((!this.actor[no]) ? this.charlist[no].name : this.actor[no].CharaName);
			});
		}
		this.OpenCharList[0].onClick.AddListener(delegate()
		{
			this.OpenCharList[0].gameObject.SetActive(false);
			this.OpenCharList[1].gameObject.SetActive(true);
			this.Content.SetActive(true);
		});
		this.OpenCharList[1].onClick.AddListener(delegate()
		{
			this.OpenCharList[0].gameObject.SetActive(true);
			this.OpenCharList[1].gameObject.SetActive(false);
			this.Content.SetActive(false);
		});
	}

	// Token: 0x06005316 RID: 21270 RVA: 0x00246130 File Offset: 0x00244530
	public void SetAction(UnityAction action)
	{
		for (int i = 0; i < this.charlist.Length; i++)
		{
			int num = i;
			this.charlist[num].onClick.AddListener(action);
		}
	}

	// Token: 0x06005317 RID: 21271 RVA: 0x0024616B File Offset: 0x0024456B
	public void CloseChoice()
	{
		this.OpenCharList[0].gameObject.SetActive(true);
		this.OpenCharList[1].gameObject.SetActive(false);
		this.Content.SetActive(false);
	}

	// Token: 0x06005318 RID: 21272 RVA: 0x002461A0 File Offset: 0x002445A0
	public void EndProc()
	{
		for (int i = 0; i < this.charlist.Length; i++)
		{
			int num = i;
			this.charlist[num].onClick.RemoveAllListeners();
		}
		this.OpenCharList[0].onClick.RemoveAllListeners();
		this.OpenCharList[1].onClick.RemoveAllListeners();
	}

	// Token: 0x06005319 RID: 21273 RVA: 0x00246200 File Offset: 0x00244600
	public void SetMaleSelectBtn(bool setVal)
	{
		for (int i = 0; i < this.Males.Length; i++)
		{
			bool flag = this.Males[i] != null && this.Males[i].objTop != null;
			if (flag)
			{
				this.charlist[i + 2].gameObject.SetActive(setVal);
			}
			else if (this.charlist[i + 2].gameObject.activeSelf)
			{
				this.charlist[i + 2].gameObject.SetActive(false);
			}
		}
		if (!setVal && this.hSceneManager.numFemaleClothCustom > 1)
		{
			this.hSceneManager.numFemaleClothCustom = 0;
			this.LabelText.text = ((!this.actor[0]) ? this.charlist[0].name : this.actor[0].CharaName);
		}
	}

	// Token: 0x04004D93 RID: 19859
	public Button[] OpenCharList;

	// Token: 0x04004D94 RID: 19860
	public Text LabelText;

	// Token: 0x04004D95 RID: 19861
	public GameObject Content;

	// Token: 0x04004D96 RID: 19862
	[SerializeField]
	private Button[] charlist;

	// Token: 0x04004D97 RID: 19863
	private ChaControl[] females;

	// Token: 0x04004D98 RID: 19864
	private ChaControl[] Males;

	// Token: 0x04004D99 RID: 19865
	private Actor[] actor;

	// Token: 0x04004D9A RID: 19866
	private HScene hScene;

	// Token: 0x04004D9B RID: 19867
	private HSceneManager hSceneManager;
}
