using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Studio
{
	// Token: 0x02001340 RID: 4928
	public class BGMControl : MonoBehaviour
	{
		// Token: 0x0600A4EF RID: 42223 RVA: 0x004340EC File Offset: 0x004324EC
		private void OnClickRepeat()
		{
			Singleton<Studio>.Instance.bgmCtrl.repeat = ((Singleton<Studio>.Instance.bgmCtrl.repeat != BGMCtrl.Repeat.None) ? BGMCtrl.Repeat.None : BGMCtrl.Repeat.All);
			this.buttonRepeat.image.sprite = this.spriteRepeat[(Singleton<Studio>.Instance.bgmCtrl.repeat != BGMCtrl.Repeat.None) ? 1 : 0];
		}

		// Token: 0x0600A4F0 RID: 42224 RVA: 0x00434155 File Offset: 0x00432555
		private void OnClickStop()
		{
			Singleton<Studio>.Instance.bgmCtrl.Stop();
		}

		// Token: 0x0600A4F1 RID: 42225 RVA: 0x00434166 File Offset: 0x00432566
		private void OnClickPlay()
		{
			Singleton<Studio>.Instance.bgmCtrl.Play();
		}

		// Token: 0x0600A4F2 RID: 42226 RVA: 0x00434177 File Offset: 0x00432577
		private void OnClickPause()
		{
			Singleton<Studio>.Instance.bgmCtrl.Pause();
		}

		// Token: 0x0600A4F3 RID: 42227 RVA: 0x00434188 File Offset: 0x00432588
		private void OnClickExpansion()
		{
			this.objBottom.SetActive(!this.objBottom.activeSelf);
			this.buttonExpansion.image.sprite = this.spriteExpansion[(!this.objBottom.activeSelf) ? 0 : 1];
		}

		// Token: 0x0600A4F4 RID: 42228 RVA: 0x004341DC File Offset: 0x004325DC
		private void OnClickSelect(int _idx)
		{
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.bgmCtrl.no, out studioNode))
			{
				studioNode.select = false;
			}
			Singleton<Studio>.Instance.bgmCtrl.no = _idx;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.bgmCtrl.no, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A4F5 RID: 42229 RVA: 0x0043424C File Offset: 0x0043264C
		private void InitList()
		{
			for (int i = 0; i < this.transformRoot.childCount; i++)
			{
				UnityEngine.Object.Destroy(this.transformRoot.GetChild(i).gameObject);
			}
			this.transformRoot.DetachChildren();
			foreach (KeyValuePair<int, Info.LoadCommonInfo> keyValuePair in Singleton<Info>.Instance.dicBGMLoadInfo)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.objectNode);
				gameObject.transform.SetParent(this.transformRoot, false);
				StudioNode component = gameObject.GetComponent<StudioNode>();
				component.active = true;
				int idx = keyValuePair.Key;
				component.addOnClick = delegate()
				{
					this.OnClickSelect(idx);
				};
				component.text = keyValuePair.Value.name;
				this.dicNode.Add(idx, component);
			}
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.bgmCtrl.no, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A4F6 RID: 42230 RVA: 0x00434394 File Offset: 0x00432794
		private void Awake()
		{
			this.buttonRepeat.onClick.AddListener(new UnityAction(this.OnClickRepeat));
			this.buttonStop.onClick.AddListener(new UnityAction(this.OnClickStop));
			this.buttonPlay.onClick.AddListener(new UnityAction(this.OnClickPlay));
			this.buttonPause.onClick.AddListener(new UnityAction(this.OnClickPause));
			this.buttonExpansion.onClick.AddListener(new UnityAction(this.OnClickExpansion));
			this.InitList();
		}

		// Token: 0x0600A4F7 RID: 42231 RVA: 0x00434434 File Offset: 0x00432834
		private void OnEnable()
		{
			this.buttonRepeat.image.sprite = this.spriteRepeat[(Singleton<Studio>.Instance.bgmCtrl.repeat != BGMCtrl.Repeat.None) ? 1 : 0];
			foreach (KeyValuePair<int, StudioNode> keyValuePair in this.dicNode)
			{
				keyValuePair.Value.select = false;
			}
			StudioNode studioNode = null;
			if (this.dicNode.TryGetValue(Singleton<Studio>.Instance.bgmCtrl.no, out studioNode))
			{
				studioNode.select = true;
			}
		}

		// Token: 0x0600A4F8 RID: 42232 RVA: 0x004344F4 File Offset: 0x004328F4
		private void Update()
		{
			this.imagePlayNow.enabled = Singleton<Studio>.Instance.bgmCtrl.play;
		}

		// Token: 0x040081DE RID: 33246
		[SerializeField]
		private GameObject objectNode;

		// Token: 0x040081DF RID: 33247
		[SerializeField]
		private Transform transformRoot;

		// Token: 0x040081E0 RID: 33248
		[SerializeField]
		private Button buttonRepeat;

		// Token: 0x040081E1 RID: 33249
		[SerializeField]
		private Sprite[] spriteRepeat;

		// Token: 0x040081E2 RID: 33250
		[SerializeField]
		private Button buttonStop;

		// Token: 0x040081E3 RID: 33251
		[SerializeField]
		private Button buttonPlay;

		// Token: 0x040081E4 RID: 33252
		[SerializeField]
		private Image imagePlayNow;

		// Token: 0x040081E5 RID: 33253
		[SerializeField]
		private Button buttonPause;

		// Token: 0x040081E6 RID: 33254
		[SerializeField]
		private Button buttonExpansion;

		// Token: 0x040081E7 RID: 33255
		[SerializeField]
		private Sprite[] spriteExpansion;

		// Token: 0x040081E8 RID: 33256
		[SerializeField]
		private GameObject objBottom;

		// Token: 0x040081E9 RID: 33257
		private Dictionary<int, StudioNode> dicNode = new Dictionary<int, StudioNode>();
	}
}
