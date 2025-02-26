using System;
using System.Collections.Generic;
using IllusionUtility.GetUtility;
using Manager;
using UniRx;
using UnityEngine;
using UnityEx;

// Token: 0x02000AB0 RID: 2736
public class HParticleCtrl
{
	// Token: 0x06005060 RID: 20576 RVA: 0x001F4A2C File Offset: 0x001F2E2C
	public void Init()
	{
		this.HSceneTables = Singleton<Manager.Resources>.Instance.HSceneTable;
		this.lstParticle = new List<HParticleCtrl.ParticleInfo>();
		for (int i = 0; i < this.HSceneTables.lstHParticleCtrl.Count; i++)
		{
			this.lstParticle.Add(new HParticleCtrl.ParticleInfo
			{
				assetPath = this.HSceneTables.lstHParticleCtrl[i].assetPath,
				file = this.HSceneTables.lstHParticleCtrl[i].file,
				manifest = this.HSceneTables.lstHParticleCtrl[i].manifest,
				numParent = this.HSceneTables.lstHParticleCtrl[i].numParent,
				nameParent = this.HSceneTables.lstHParticleCtrl[i].nameParent,
				pos = this.HSceneTables.lstHParticleCtrl[i].pos,
				rot = this.HSceneTables.lstHParticleCtrl[i].rot
			});
			GameObject gameObject = CommonLib.LoadAsset<GameObject>(this.lstParticle[i].assetPath, this.lstParticle[i].file, true, this.lstParticle[i].manifest);
			Singleton<HSceneManager>.Instance.hashUseAssetBundle.Add(this.lstParticle[i].assetPath);
			this.lstParticle[i].particle = gameObject.GetComponent<ParticleSystem>();
			this.lstParticle[i].particleCache.Item1 = gameObject;
			this.lstParticle[i].particleCache.Item1.SetActive(false);
			this.lstParticle[i].particleCache.Item2 = gameObject.transform;
			this.lstParticle[i].particleCache.Item2.SetParent(this.particlePlace, false);
			this.lstParticle[i].particleCache.Item2.localPosition = Vector3.zero;
			this.lstParticle[i].particleCache.Item2.localRotation = Quaternion.identity;
			this.lstParticle[i].particleCache.Item2.localScale = Vector3.one;
		}
		if (this.disposable != null)
		{
			this.disposable.Dispose();
			this.disposable = null;
		}
		this.disposable = Observable.EveryEndOfFrame().Subscribe(delegate(long _)
		{
			this.UpdatePosition();
		});
	}

	// Token: 0x06005061 RID: 20577 RVA: 0x001F4CC8 File Offset: 0x001F30C8
	private void UpdatePosition()
	{
		if (!this.bUpdatePos)
		{
			return;
		}
		for (int i = 0; i < this.lstParticle.Count; i++)
		{
			HParticleCtrl.ParticleInfo particleInfo = this.lstParticle[i];
			if (particleInfo != null && !(particleInfo.Parent == null) && !(particleInfo.particleCache.Item2 == null))
			{
				particleInfo.particleCache.Item2.SetParent(particleInfo.Parent, false);
				particleInfo.particleCache.Item2.localPosition = particleInfo.pos;
				particleInfo.particleCache.Item2.localRotation = Quaternion.Euler(particleInfo.rot);
				if (this.particlePlace != null)
				{
					particleInfo.particleCache.Item2.SetParent(this.particlePlace);
				}
				else
				{
					particleInfo.particleCache.Item2.parent = null;
				}
				particleInfo.particleCache.Item2.localScale = Vector3.one;
			}
		}
	}

	// Token: 0x06005062 RID: 20578 RVA: 0x001F4DD8 File Offset: 0x001F31D8
	public void ParticleLoad(GameObject _objBody, int _sex)
	{
		if (_objBody == null)
		{
			return;
		}
		for (int i = 0; i < this.lstParticle.Count; i++)
		{
			if (this.lstParticle[i].numParent == _sex)
			{
				this.objParent = _objBody.transform.FindLoop(this.lstParticle[i].nameParent);
				if (!(this.objParent == null))
				{
					this.lstParticle[i].particleCache.Item1.SetActive(true);
					this.lstParticle[i].particle.Stop();
					this.lstParticle[i].Parent = this.objParent.transform;
					this.lstParticle[i].particleCache.Item2.SetParent(this.objParent.transform, false);
					this.lstParticle[i].particleCache.Item2.localPosition = this.lstParticle[i].pos;
					this.lstParticle[i].particleCache.Item2.localRotation = Quaternion.Euler(this.lstParticle[i].rot);
					if (this.particlePlace != null)
					{
						this.lstParticle[i].particleCache.Item2.SetParent(this.particlePlace);
					}
					else
					{
						this.lstParticle[i].particleCache.Item2.parent = null;
					}
					this.lstParticle[i].particleCache.Item2.localScale = Vector3.one;
				}
			}
		}
		this.bUpdatePos = true;
	}

	// Token: 0x06005063 RID: 20579 RVA: 0x001F4FB0 File Offset: 0x001F33B0
	public bool ReleaseObject(int _sex)
	{
		for (int i = 0; i < this.lstParticle.Count; i++)
		{
			if (this.lstParticle[i] != null)
			{
				if (this.lstParticle[i].numParent == _sex)
				{
					UnityEngine.Object.Destroy(this.lstParticle[i].particleCache.Item1);
					this.lstParticle[i].particle = null;
					this.lstParticle[i].particleCache.Item1 = null;
					this.lstParticle[i].particleCache.Item2 = null;
				}
			}
		}
		return true;
	}

	// Token: 0x06005064 RID: 20580 RVA: 0x001F5064 File Offset: 0x001F3464
	public bool ReleaseObject()
	{
		for (int i = 0; i < this.lstParticle.Count; i++)
		{
			if (this.lstParticle[i] != null)
			{
				if (!(this.lstParticle[i].particle == null))
				{
					UnityEngine.Object.Destroy(this.lstParticle[i].particleCache.Item1);
					this.lstParticle[i].particle = null;
					this.lstParticle[i].particleCache.Item1 = null;
					this.lstParticle[i].particleCache.Item2 = null;
				}
			}
		}
		this.bUpdatePos = false;
		if (this.disposable != null)
		{
			this.disposable.Dispose();
			this.disposable = null;
		}
		return true;
	}

	// Token: 0x06005065 RID: 20581 RVA: 0x001F5144 File Offset: 0x001F3544
	public bool RePlaceObject()
	{
		for (int i = 0; i < this.lstParticle.Count; i++)
		{
			if (this.lstParticle[i] != null)
			{
				if (!(this.lstParticle[i].particle == null))
				{
					if (this.lstParticle[i].particleCache.Item2.parent != this.particlePlace)
					{
						this.lstParticle[i].particleCache.Item1.SetActive(false);
						this.lstParticle[i].particleCache.Item2.SetParent(this.particlePlace, false);
						this.lstParticle[i].particleCache.Item2.localPosition = Vector3.zero;
						this.lstParticle[i].particleCache.Item2.localRotation = Quaternion.identity;
						this.lstParticle[i].particleCache.Item2.localScale = Vector3.one;
					}
				}
			}
		}
		return true;
	}

	// Token: 0x06005066 RID: 20582 RVA: 0x001F5270 File Offset: 0x001F3670
	public bool Play(int _particle)
	{
		if (this.lstParticle.Count <= _particle)
		{
			return false;
		}
		if (this.lstParticle[_particle].particle == null)
		{
			return false;
		}
		this.lstParticle[_particle].particle.Simulate(0f);
		this.lstParticle[_particle].particle.Play();
		return true;
	}

	// Token: 0x06005067 RID: 20583 RVA: 0x001F52E0 File Offset: 0x001F36E0
	public bool IsPlaying(int _particle)
	{
		return this.lstParticle.Count > _particle && !(this.lstParticle[_particle].particle == null) && this.lstParticle[_particle].particle.isPlaying;
	}

	// Token: 0x040049D9 RID: 18905
	public Transform particlePlace;

	// Token: 0x040049DA RID: 18906
	private List<HParticleCtrl.ParticleInfo> lstParticle = new List<HParticleCtrl.ParticleInfo>();

	// Token: 0x040049DB RID: 18907
	private GameObject objParent;

	// Token: 0x040049DC RID: 18908
	private Manager.Resources.HSceneTables HSceneTables;

	// Token: 0x040049DD RID: 18909
	private IDisposable disposable;

	// Token: 0x040049DE RID: 18910
	private bool bUpdatePos;

	// Token: 0x02000AB1 RID: 2737
	public class ParticleInfo
	{
		// Token: 0x040049DF RID: 18911
		public string assetPath;

		// Token: 0x040049E0 RID: 18912
		public string file;

		// Token: 0x040049E1 RID: 18913
		public string manifest;

		// Token: 0x040049E2 RID: 18914
		public int numParent;

		// Token: 0x040049E3 RID: 18915
		public string nameParent;

		// Token: 0x040049E4 RID: 18916
		public Vector3 pos;

		// Token: 0x040049E5 RID: 18917
		public Vector3 rot;

		// Token: 0x040049E6 RID: 18918
		public ParticleSystem particle;

		// Token: 0x040049E7 RID: 18919
		public UnityEx.ValueTuple<GameObject, Transform> particleCache;

		// Token: 0x040049E8 RID: 18920
		public Transform Parent;
	}
}
