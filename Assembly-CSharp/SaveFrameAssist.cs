using System;
using AIChara;
using Illusion.Extensions;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000A1A RID: 2586
public class SaveFrameAssist : MonoBehaviour
{
	// Token: 0x17000E79 RID: 3705
	// (get) Token: 0x06004CEC RID: 19692 RVA: 0x001D8E13 File Offset: 0x001D7213
	// (set) Token: 0x06004CED RID: 19693 RVA: 0x001D8E20 File Offset: 0x001D7220
	public bool forceBackFrameHide
	{
		get
		{
			return this._forceBackFrameHide.Value;
		}
		set
		{
			this._forceBackFrameHide.Value = value;
		}
	}

	// Token: 0x17000E7A RID: 3706
	// (get) Token: 0x06004CEE RID: 19694 RVA: 0x001D8E2E File Offset: 0x001D722E
	// (set) Token: 0x06004CEF RID: 19695 RVA: 0x001D8E3B File Offset: 0x001D723B
	public bool backFrameDraw
	{
		get
		{
			return this._backFrameDraw.Value;
		}
		set
		{
			this._backFrameDraw.Value = value;
		}
	}

	// Token: 0x17000E7B RID: 3707
	// (get) Token: 0x06004CF0 RID: 19696 RVA: 0x001D8E49 File Offset: 0x001D7249
	// (set) Token: 0x06004CF1 RID: 19697 RVA: 0x001D8E56 File Offset: 0x001D7256
	public bool frontFrameDraw
	{
		get
		{
			return this._frontFrameDraw.Value;
		}
		set
		{
			this._frontFrameDraw.Value = value;
		}
	}

	// Token: 0x06004CF2 RID: 19698 RVA: 0x001D8E64 File Offset: 0x001D7264
	public void ForgetLastName()
	{
		this.lastFrameBackName = string.Empty;
		this.lastFrameFrontName = string.Empty;
	}

	// Token: 0x06004CF3 RID: 19699 RVA: 0x001D8E7C File Offset: 0x001D727C
	public bool Initialize()
	{
		this.ChangeSaveFrameBack(1, true);
		this.ChangeSaveFrameFront(1, true);
		return true;
	}

	// Token: 0x06004CF4 RID: 19700 RVA: 0x001D8E94 File Offset: 0x001D7294
	public bool ChangeSaveFrameBack(byte changeNo, bool listUpdate = true)
	{
		if (listUpdate)
		{
			string str = "cardframe/Back";
			string[] searchPattern = new string[]
			{
				"*.png"
			};
			this.dirFrameBack.lstFile.Clear();
			this.dirFrameBack.CreateFolderInfoEx(DefaultData.Path + str, searchPattern, true);
			this.dirFrameBack.CreateFolderInfoEx(UserData.Path + str, searchPattern, false);
			this.dirFrameBack.SortName(true);
		}
		int fileCount = this.dirFrameBack.GetFileCount();
		if (fileCount == 0)
		{
			return false;
		}
		int num = this.dirFrameBack.GetIndexFromFileName(this.lastFrameBackName);
		if (num == -1)
		{
			num = 0;
		}
		else if (changeNo == 0)
		{
			num = (num + 1) % fileCount;
		}
		else if (changeNo == 1)
		{
			num = (num + fileCount - 1) % fileCount;
		}
		Texture value = PngAssist.LoadTexture(this.dirFrameBack.lstFile[num].FullPath);
		if (this.rendBack && this.rendBack.material)
		{
			Texture texture = this.rendBack.material.GetTexture(ChaShader.MainTex);
			if (texture)
			{
				UnityEngine.Object.Destroy(texture);
			}
			this.rendBack.material.SetTexture(ChaShader.MainTex, value);
		}
		this.lastFrameBackName = this.dirFrameBack.lstFile[num].FileName;
		return true;
	}

	// Token: 0x06004CF5 RID: 19701 RVA: 0x001D9000 File Offset: 0x001D7400
	public bool ChangeSaveFrameFront(byte changeNo, bool listUpdate = true)
	{
		if (listUpdate)
		{
			string str = "cardframe/Front";
			string[] searchPattern = new string[]
			{
				"*.png"
			};
			this.dirFrameFront.lstFile.Clear();
			this.dirFrameFront.CreateFolderInfoEx(DefaultData.Path + str, searchPattern, true);
			this.dirFrameFront.CreateFolderInfoEx(UserData.Path + str, searchPattern, false);
			this.dirFrameFront.SortName(true);
		}
		int fileCount = this.dirFrameFront.GetFileCount();
		if (fileCount == 0)
		{
			return false;
		}
		int num = this.dirFrameFront.GetIndexFromFileName(this.lastFrameFrontName);
		if (num == -1)
		{
			num = 0;
		}
		else if (changeNo == 0)
		{
			num = (num + 1) % fileCount;
		}
		else if (changeNo == 1)
		{
			num = (num + fileCount - 1) % fileCount;
		}
		Texture texture = PngAssist.LoadTexture(this.dirFrameFront.lstFile[num].FullPath);
		if (this.riFront.texture)
		{
			UnityEngine.Object.Destroy(this.riFront.texture);
		}
		this.riFront.texture = texture;
		this.lastFrameFrontName = this.dirFrameFront.lstFile[num].FileName;
		return true;
	}

	// Token: 0x06004CF6 RID: 19702 RVA: 0x001D9138 File Offset: 0x001D7538
	public string GetNowPositionStringBack()
	{
		int fileCount = this.dirFrameBack.GetFileCount();
		if (fileCount == 0)
		{
			return "ファイルがありません";
		}
		int indexFromFileName = this.dirFrameBack.GetIndexFromFileName(this.lastFrameBackName);
		return string.Format("{0:000} / {1:000}", indexFromFileName + 1, fileCount);
	}

	// Token: 0x06004CF7 RID: 19703 RVA: 0x001D9188 File Offset: 0x001D7588
	public string GetNowPositionStringFront()
	{
		int fileCount = this.dirFrameFront.GetFileCount();
		if (fileCount == 0)
		{
			return "ファイルがありません";
		}
		int indexFromFileName = this.dirFrameFront.GetIndexFromFileName(this.lastFrameFrontName);
		return string.Format("{0:000} / {1:000}", indexFromFileName + 1, fileCount);
	}

	// Token: 0x06004CF8 RID: 19704 RVA: 0x001D91D7 File Offset: 0x001D75D7
	public bool SetActiveSaveFrameTop(bool active)
	{
		if (null == this.objSaveFrameTop)
		{
			return false;
		}
		this.objSaveFrameTop.SetActiveIfDifferent(active);
		return true;
	}

	// Token: 0x06004CF9 RID: 19705 RVA: 0x001D91FC File Offset: 0x001D75FC
	public bool ChangeSaveFrameTexture(int bf, Texture tex)
	{
		if (null == this.objSaveFrameTop)
		{
			return false;
		}
		if (bf == 0)
		{
			if (this.rendBack && this.rendBack.material)
			{
				Texture texture = this.rendBack.material.GetTexture(ChaShader.MainTex);
				if (texture)
				{
					UnityEngine.Object.Destroy(texture);
				}
				this.rendBack.material.SetTexture(ChaShader.MainTex, tex);
			}
		}
		else
		{
			if (this.riFront.texture)
			{
				UnityEngine.Object.Destroy(this.riFront.texture);
			}
			this.riFront.texture = tex;
		}
		return true;
	}

	// Token: 0x06004CFA RID: 19706 RVA: 0x001D92BC File Offset: 0x001D76BC
	private void Start()
	{
		this._forceBackFrameHide.Subscribe(delegate(bool hide)
		{
			if (hide)
			{
				if (null != this.objSaveBack)
				{
					this.objSaveBack.SetActiveIfDifferent(false);
				}
				this.backFrameCam.enabled = false;
			}
			else
			{
				if (null != this.objSaveBack)
				{
					this.objSaveBack.SetActiveIfDifferent(this.backFrameDraw);
				}
				this.backFrameCam.enabled = this.backFrameDraw;
			}
		});
		this._backFrameDraw.Subscribe(delegate(bool visible)
		{
			bool flag = !this.forceBackFrameHide && visible;
			if (null != this.objSaveBack)
			{
				this.objSaveBack.SetActiveIfDifferent(flag);
			}
			this.backFrameCam.enabled = flag;
		});
		this._frontFrameDraw.Subscribe(delegate(bool visible)
		{
			if (null != this.objSaveFront)
			{
				this.objSaveFront.SetActiveIfDifferent(visible);
			}
			this.frontFrameCam.enabled = visible;
		});
	}

	// Token: 0x04004675 RID: 18037
	private FolderAssist dirFrameBack = new FolderAssist();

	// Token: 0x04004676 RID: 18038
	private FolderAssist dirFrameFront = new FolderAssist();

	// Token: 0x04004677 RID: 18039
	private string lastFrameBackName = string.Empty;

	// Token: 0x04004678 RID: 18040
	private string lastFrameFrontName = string.Empty;

	// Token: 0x04004679 RID: 18041
	[SerializeField]
	private GameObject objSaveFrameTop;

	// Token: 0x0400467A RID: 18042
	[SerializeField]
	private GameObject objSaveBack;

	// Token: 0x0400467B RID: 18043
	[SerializeField]
	private GameObject objSaveFront;

	// Token: 0x0400467C RID: 18044
	[SerializeField]
	private Renderer rendBack;

	// Token: 0x0400467D RID: 18045
	[SerializeField]
	private RawImage riFront;

	// Token: 0x0400467E RID: 18046
	public Camera backFrameCam;

	// Token: 0x0400467F RID: 18047
	public Camera frontFrameCam;

	// Token: 0x04004680 RID: 18048
	public BoolReactiveProperty _forceBackFrameHide = new BoolReactiveProperty(false);

	// Token: 0x04004681 RID: 18049
	public BoolReactiveProperty _backFrameDraw = new BoolReactiveProperty(false);

	// Token: 0x04004682 RID: 18050
	public BoolReactiveProperty _frontFrameDraw = new BoolReactiveProperty(false);
}
