using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

// Token: 0x02000EF8 RID: 3832
public class SaveLoadUI : Singleton<SaveLoadUI>
{
	// Token: 0x06007D1B RID: 32027 RVA: 0x00358338 File Offset: 0x00356738
	private void Start()
	{
		this.nNowPageCnt = 1;
		this.nMaxPageCnt = 1;
		this.NowPageCnt.text = string.Format("{0}", this.nNowPageCnt);
		this.widthnum = 0;
		this.heightnum = 0;
		if (this.load != null)
		{
			this.load.onClick.AddListener(new UnityAction(this.LoadSetUp));
		}
		if (this.close != null)
		{
			this.close.onClick.AddListener(new UnityAction(this.Close));
		}
		if (this.loadCancel != null)
		{
			this.loadCancel.onClick.AddListener(new UnityAction(this.LoadCancel));
		}
		if (this.loadEndOK != null)
		{
			this.loadEndOK.onClick.AddListener(new UnityAction(this.LoadEndOkDel));
		}
		if (this.saveEndOK != null)
		{
			this.saveEndOK.onClick.AddListener(new UnityAction(this.SaveEndOkDel));
		}
		if (this.prevPage != null)
		{
			this.prevPage.onClick.AddListener(delegate()
			{
				this.ChangePage(0);
			});
		}
		if (this.nextPage != null)
		{
			this.nextPage.onClick.AddListener(delegate()
			{
				this.ChangePage(1);
			});
		}
	}

	// Token: 0x06007D1C RID: 32028 RVA: 0x003584BC File Offset: 0x003568BC
	private void LoadSetUp()
	{
		this.saveloadPanel.SetActive(true);
		this.nNowPageCnt = 1;
		this.NowPageCnt.text = string.Format("{0}", this.nNowPageCnt);
		this.saveFiles = Directory.GetFiles(Application.dataPath + "/in-house/Scripts/Game/Scene/Map/Craft/SaveData", "*.png").ToList<string>();
		float x = this.savedata.GetComponent<RectTransform>().sizeDelta.x;
		float y = this.savedata.GetComponent<RectTransform>().sizeDelta.y;
		float x2 = this.saveDataArea.sizeDelta.x;
		float y2 = this.saveDataArea.sizeDelta.y;
		this.widthnum = Mathf.FloorToInt(x2 / x);
		this.heightnum = Mathf.FloorToInt(y2 / y);
		this.nMaxPageCnt = this.saveFiles.Count / (this.widthnum * this.heightnum);
		if (this.saveFiles.Count % (this.widthnum * this.heightnum) != 0)
		{
			this.nMaxPageCnt++;
		}
		if (this.saveFiles.Count == 0)
		{
			this.nMaxPageCnt = 1;
		}
		for (int i = 0; i < this.saveFiles.Count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.savedata);
			gameObject.transform.SetParent(this.loadPanel, false);
			Vector3 localPosition = gameObject.transform.localPosition;
			localPosition.x += x * (float)(i % this.widthnum);
			localPosition.y -= y * (float)(i / this.widthnum % this.heightnum);
			gameObject.transform.localPosition = localPosition;
			gameObject.GetComponentInChildren<RawImage>().texture = PngAssist.LoadTexture(this.saveFiles[i]);
			int ID = i;
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.loadCheckPanelSetUp(ID);
			});
			this.savedatas.Add(gameObject);
		}
		this.SaveDataChangeActive();
		this.prevPage.interactable = false;
		this.nextPage.interactable = true;
		if (this.nNowPageCnt == this.nMaxPageCnt)
		{
			this.nextPage.interactable = false;
		}
	}

	// Token: 0x06007D1D RID: 32029 RVA: 0x00358731 File Offset: 0x00356B31
	private void loadCheckPanelSetUp(int saveId)
	{
		this.nSaveID = saveId;
		this.loadCheckPanel.SetActive(true);
	}

	// Token: 0x06007D1E RID: 32030 RVA: 0x00358748 File Offset: 0x00356B48
	private void Close()
	{
		this.saveloadPanel.SetActive(false);
		foreach (GameObject obj in this.savedatas)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.savedatas.Clear();
	}

	// Token: 0x06007D1F RID: 32031 RVA: 0x003587BC File Offset: 0x00356BBC
	private void LoadCancel()
	{
		this.loadCheckPanel.SetActive(false);
	}

	// Token: 0x06007D20 RID: 32032 RVA: 0x003587CA File Offset: 0x00356BCA
	private void LoadEndOkDel()
	{
		this.loadEnd.SetActive(false);
		this.LoadCancel();
	}

	// Token: 0x06007D21 RID: 32033 RVA: 0x003587DE File Offset: 0x00356BDE
	private void SaveEndOkDel()
	{
		this.saveEnd.SetActive(false);
	}

	// Token: 0x06007D22 RID: 32034 RVA: 0x003587EC File Offset: 0x00356BEC
	private void ChangePage(int direction)
	{
		if (direction != 0)
		{
			if (direction == 1)
			{
				this.nNowPageCnt++;
				if (this.nNowPageCnt >= this.nMaxPageCnt)
				{
					this.nextPage.interactable = false;
					this.nNowPageCnt = this.nMaxPageCnt;
				}
				else if (!this.nextPage.interactable)
				{
					this.nextPage.interactable = true;
				}
				this.prevPage.interactable = true;
			}
		}
		else
		{
			this.nNowPageCnt--;
			if (this.nNowPageCnt <= 1)
			{
				this.prevPage.interactable = false;
				this.nNowPageCnt = 1;
			}
			else if (!this.prevPage.interactable)
			{
				this.prevPage.interactable = true;
			}
			this.nextPage.interactable = true;
		}
		this.NowPageCnt.text = string.Format("{0}", this.nNowPageCnt);
		this.SaveDataChangeActive();
	}

	// Token: 0x06007D23 RID: 32035 RVA: 0x003588FC File Offset: 0x00356CFC
	private void SaveDataChangeActive()
	{
		for (int i = 0; i < this.savedatas.Count; i++)
		{
			if (i / this.widthnum / this.heightnum == this.nNowPageCnt - 1)
			{
				this.savedatas[i].SetActive(true);
			}
			else
			{
				this.savedatas[i].SetActive(false);
			}
		}
	}

	// Token: 0x04006522 RID: 25890
	public const string savePath = "/in-house/Scripts/Game/Scene/Map/Craft/SaveData";

	// Token: 0x04006523 RID: 25891
	public Button save;

	// Token: 0x04006524 RID: 25892
	public Button load;

	// Token: 0x04006525 RID: 25893
	public Button close;

	// Token: 0x04006526 RID: 25894
	public Button dataLoad;

	// Token: 0x04006527 RID: 25895
	public Button loadCancel;

	// Token: 0x04006528 RID: 25896
	public Button loadEndOK;

	// Token: 0x04006529 RID: 25897
	public Button saveEndOK;

	// Token: 0x0400652A RID: 25898
	public Button prevPage;

	// Token: 0x0400652B RID: 25899
	public Button nextPage;

	// Token: 0x0400652C RID: 25900
	public Text NowPageCnt;

	// Token: 0x0400652D RID: 25901
	public GameObject savedata;

	// Token: 0x0400652E RID: 25902
	public GameObject saveloadPanel;

	// Token: 0x0400652F RID: 25903
	public Transform loadPanel;

	// Token: 0x04006530 RID: 25904
	public GameObject loadCheckPanel;

	// Token: 0x04006531 RID: 25905
	public GameObject loadEnd;

	// Token: 0x04006532 RID: 25906
	public GameObject saveEnd;

	// Token: 0x04006533 RID: 25907
	public RectTransform saveDataArea;

	// Token: 0x04006534 RID: 25908
	public int nSaveID;

	// Token: 0x04006535 RID: 25909
	public List<string> saveFiles;

	// Token: 0x04006536 RID: 25910
	private List<GameObject> savedatas = new List<GameObject>();

	// Token: 0x04006537 RID: 25911
	private int nNowPageCnt;

	// Token: 0x04006538 RID: 25912
	private int nMaxPageCnt;

	// Token: 0x04006539 RID: 25913
	private int widthnum;

	// Token: 0x0400653A RID: 25914
	private int heightnum;
}
