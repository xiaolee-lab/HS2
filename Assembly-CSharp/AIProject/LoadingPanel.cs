using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Manager;
using ReMotion;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using UnityEx;

namespace AIProject
{
	// Token: 0x02000F9F RID: 3999
	public class LoadingPanel : MonoBehaviour
	{
		// Token: 0x17001D30 RID: 7472
		// (get) Token: 0x0600854B RID: 34123 RVA: 0x00374FB8 File Offset: 0x003733B8
		public CanvasGroup CanvasGroup
		{
			[CompilerGenerated]
			get
			{
				return this._canvasGroup;
			}
		}

		// Token: 0x0600854C RID: 34124 RVA: 0x00374FC0 File Offset: 0x003733C0
		private void Awake()
		{
			foreach (Image image in this._tipsImage)
			{
				CanvasGroup component = image.GetComponent<CanvasGroup>();
				if (component != null)
				{
					this._tipsImageCGList.Add(component);
				}
			}
		}

		// Token: 0x0600854D RID: 34125 RVA: 0x0037500B File Offset: 0x0037340B
		public void Play()
		{
			if (this._waveAnim != null)
			{
				this._waveAnim.PlayAnim(true);
			}
			this._disposable = Observable.FromCoroutine(() => this.SlideShowCoroutine(true), false).Subscribe<Unit>();
		}

		// Token: 0x0600854E RID: 34126 RVA: 0x00375047 File Offset: 0x00373447
		public void Stop()
		{
			if (this._waveAnim != null)
			{
				this._waveAnim.Stop();
			}
			if (this._disposable != null)
			{
				this._disposable.Dispose();
			}
		}

		// Token: 0x0600854F RID: 34127 RVA: 0x00375080 File Offset: 0x00373480
		private IEnumerator SlideShowCoroutine(bool isLoop)
		{
			int id = this.GetUseSPID();
			AssetBundleInfo abInfo = Singleton<Game>.Instance.LoadingSpriteABList[id];
			int index = 0;
			this._tipsImage[index].sprite = LoadingPanel.LoadSpriteAsset(abInfo.assetbundle, abInfo.asset, abInfo.manifest);
			this._tipsImageCGList[index].alpha = 1f;
			while (isLoop)
			{
				index++;
				if (index >= this._tipsImage.Length)
				{
					index = 0;
				}
				id = this.GetUseSPID();
				abInfo = Singleton<Game>.Instance.LoadingSpriteABList[id];
				this._tipsImage[index].sprite = LoadingPanel.LoadSpriteAsset(abInfo.assetbundle, abInfo.asset, abInfo.manifest);
				this._tipsImageCGList[index].alpha = 0f;
				this._tipsImageCGList[index].transform.SetAsLastSibling();
				yield return Observable.Timer(TimeSpan.FromSeconds((double)this._interval), Scheduler.MainThreadIgnoreTimeScale).ToYieldInstruction<long>();
				yield return ObservableEasing.Linear(this._duration, true).FrameTimeInterval(true).Do(delegate(TimeInterval<float> x)
				{
					this.SetCanvasGroupAlpha(index, x.Value);
				}).ToYieldInstruction<TimeInterval<float>>();
			}
			yield break;
		}

		// Token: 0x06008550 RID: 34128 RVA: 0x003750A2 File Offset: 0x003734A2
		private void SetCanvasGroupAlpha(int index, float alpha)
		{
			this._tipsImageCGList[index].alpha = alpha;
		}

		// Token: 0x06008551 RID: 34129 RVA: 0x003750B8 File Offset: 0x003734B8
		private int GetUseSPID()
		{
			List<int> list = ListPool<int>.Get();
			foreach (KeyValuePair<int, AssetBundleInfo> keyValuePair in Singleton<Game>.Instance.LoadingSpriteABList)
			{
				list.Add(keyValuePair.Key);
			}
			int result = -1;
			while (list.Count > 0)
			{
				int num = list[UnityEngine.Random.Range(0, list.Count)];
				if (this._prevID != num)
				{
					result = num;
					this._prevID = num;
					break;
				}
				list.Remove(num);
			}
			ListPool<int>.Release(list);
			return result;
		}

		// Token: 0x06008552 RID: 34130 RVA: 0x00375178 File Offset: 0x00373578
		public static Sprite LoadSpriteAsset(string assetBundleName, string assetName, string manifestName)
		{
			manifestName = ((!manifestName.IsNullOrEmpty()) ? manifestName : null);
			if (AssetBundleCheck.IsSimulation)
			{
				manifestName = string.Empty;
			}
			if (!AssetBundleCheck.IsFile(assetBundleName, assetName))
			{
				string text = string.Format("読み込みエラー\r\nassetBundleName：{0}\tassetName：{1}", assetBundleName, assetName);
				return null;
			}
			AssetBundleLoadAssetOperation assetBundleLoadAssetOperation = AssetBundleManager.LoadAsset(assetBundleName, assetName, typeof(Sprite), (!manifestName.IsNullOrEmpty()) ? manifestName : null);
			Sprite sprite = assetBundleLoadAssetOperation.GetAsset<Sprite>();
			if (sprite == null)
			{
				Texture2D asset = assetBundleLoadAssetOperation.GetAsset<Texture2D>();
				if (asset == null)
				{
					return null;
				}
				sprite = Sprite.Create(asset, new Rect(0f, 0f, (float)asset.width, (float)asset.height), Vector2.zero);
			}
			return sprite;
		}

		// Token: 0x04006BD2 RID: 27602
		[SerializeField]
		private CanvasGroup _canvasGroup;

		// Token: 0x04006BD3 RID: 27603
		[SerializeField]
		private ProgressWave _waveAnim;

		// Token: 0x04006BD4 RID: 27604
		[SerializeField]
		private Image[] _tipsImage;

		// Token: 0x04006BD5 RID: 27605
		private List<CanvasGroup> _tipsImageCGList = new List<CanvasGroup>();

		// Token: 0x04006BD6 RID: 27606
		[SerializeField]
		private float _duration;

		// Token: 0x04006BD7 RID: 27607
		[SerializeField]
		private float _interval;

		// Token: 0x04006BD8 RID: 27608
		private IDisposable _disposable;

		// Token: 0x04006BD9 RID: 27609
		private int _prevID = -1;
	}
}
