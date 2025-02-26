using System;
using UniRx;
using UnityEngine;

namespace Studio
{
	// Token: 0x020011D7 RID: 4567
	public class HandAnimeCtrl : MonoBehaviour
	{
		// Token: 0x17001FC7 RID: 8135
		// (get) Token: 0x06009613 RID: 38419 RVA: 0x003DFBAE File Offset: 0x003DDFAE
		// (set) Token: 0x06009614 RID: 38420 RVA: 0x003DFBB6 File Offset: 0x003DDFB6
		public int sex { get; private set; }

		// Token: 0x17001FC8 RID: 8136
		// (get) Token: 0x06009615 RID: 38421 RVA: 0x003DFBBF File Offset: 0x003DDFBF
		// (set) Token: 0x06009616 RID: 38422 RVA: 0x003DFBCC File Offset: 0x003DDFCC
		public int ptn
		{
			get
			{
				return this._ptn.Value;
			}
			set
			{
				this._ptn.Value = value;
			}
		}

		// Token: 0x17001FC9 RID: 8137
		// (get) Token: 0x06009617 RID: 38423 RVA: 0x003DFBDA File Offset: 0x003DDFDA
		// (set) Token: 0x06009618 RID: 38424 RVA: 0x003DFBE2 File Offset: 0x003DDFE2
		public int max { get; private set; }

		// Token: 0x17001FCA RID: 8138
		// (get) Token: 0x06009619 RID: 38425 RVA: 0x003DFBEB File Offset: 0x003DDFEB
		// (set) Token: 0x0600961A RID: 38426 RVA: 0x003DFBF3 File Offset: 0x003DDFF3
		public bool isInit { get; private set; }

		// Token: 0x0600961B RID: 38427 RVA: 0x003DFBFC File Offset: 0x003DDFFC
		public void Init(int _sex)
		{
			if (this.isInit)
			{
				return;
			}
			this.max = Singleton<Info>.Instance.dicHandAnime[(int)this.hand].Count;
			this._ptn.Subscribe(delegate(int _)
			{
				this.LoadAnime();
			});
			this.sex = _sex;
			this.ptn = 0;
			this.isInit = true;
		}

		// Token: 0x0600961C RID: 38428 RVA: 0x003DFC60 File Offset: 0x003DE060
		private void LoadAnime()
		{
			Info.HandAnimeInfo handAnimeInfo = null;
			if (!Singleton<Info>.Instance.dicHandAnime[(int)this.hand].TryGetValue(this.ptn, out handAnimeInfo))
			{
				base.enabled = false;
				return;
			}
			if (this.bundlePath != handAnimeInfo.bundlePath || this.fileName != handAnimeInfo.fileName)
			{
				RuntimeAnimatorController runtimeAnimatorController = CommonLib.LoadAsset<RuntimeAnimatorController>(handAnimeInfo.bundlePath, handAnimeInfo.fileName, false, string.Empty);
				if (runtimeAnimatorController != null)
				{
					this.bundlePath = handAnimeInfo.bundlePath;
					this.fileName = handAnimeInfo.fileName;
					this.animator.runtimeAnimatorController = runtimeAnimatorController;
				}
				AssetBundleManager.UnloadAssetBundle(handAnimeInfo.bundlePath, false, null, false);
			}
			base.enabled = true;
			this.animator.Play(handAnimeInfo.clip);
		}

		// Token: 0x0600961D RID: 38429 RVA: 0x003DFD34 File Offset: 0x003DE134
		private void OnEnable()
		{
			this.animator.enabled = true;
		}

		// Token: 0x0600961E RID: 38430 RVA: 0x003DFD42 File Offset: 0x003DE142
		private void OnDisable()
		{
			this.animator.enabled = false;
		}

		// Token: 0x040078BD RID: 30909
		[SerializeField]
		private HandAnimeCtrl.HandKind hand;

		// Token: 0x040078BE RID: 30910
		[SerializeField]
		private Animator animator;

		// Token: 0x040078C0 RID: 30912
		private IntReactiveProperty _ptn = new IntReactiveProperty(-1);

		// Token: 0x040078C3 RID: 30915
		private string bundlePath = string.Empty;

		// Token: 0x040078C4 RID: 30916
		private string fileName = string.Empty;

		// Token: 0x020011D8 RID: 4568
		public enum HandKind
		{
			// Token: 0x040078C6 RID: 30918
			Left,
			// Token: 0x040078C7 RID: 30919
			Right
		}
	}
}
