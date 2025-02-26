using System;
using System.Runtime.CompilerServices;
using UniRx;
using UnityEngine;

namespace AIProject
{
	// Token: 0x02000C2E RID: 3118
	public class StoryPointEffect : MonoBehaviour
	{
		// Token: 0x17001305 RID: 4869
		// (get) Token: 0x0600607D RID: 24701 RVA: 0x0028932A File Offset: 0x0028772A
		// (set) Token: 0x0600607E RID: 24702 RVA: 0x00289331 File Offset: 0x00287731
		public static bool Switch { get; set; }

		// Token: 0x17001306 RID: 4870
		// (get) Token: 0x0600607F RID: 24703 RVA: 0x00289339 File Offset: 0x00287739
		public bool IsActive
		{
			[CompilerGenerated]
			get
			{
				return this._particle != null && this._particleRenderer != null;
			}
		}

		// Token: 0x17001307 RID: 4871
		// (get) Token: 0x06006080 RID: 24704 RVA: 0x0028935B File Offset: 0x0028775B
		public bool IsPlaying
		{
			[CompilerGenerated]
			get
			{
				return this.IsActive && this._particle.isPlaying;
			}
		}

		// Token: 0x17001308 RID: 4872
		// (get) Token: 0x06006081 RID: 24705 RVA: 0x00289376 File Offset: 0x00287776
		// (set) Token: 0x06006082 RID: 24706 RVA: 0x0028937E File Offset: 0x0028777E
		public EventPoint Point { get; private set; }

		// Token: 0x06006083 RID: 24707 RVA: 0x00289388 File Offset: 0x00287788
		private void Awake()
		{
			if (this._particle == null)
			{
				this._particle = base.GetComponentInChildren<ParticleSystem>(true);
			}
			if (this._particleRenderer == null)
			{
				this._particleRenderer = ((this._particle != null) ? this._particle.GetComponent<ParticleSystemRenderer>() : null);
			}
		}

		// Token: 0x06006084 RID: 24708 RVA: 0x002893E3 File Offset: 0x002877E3
		private void Start()
		{
			Observable.EveryUpdate().TakeUntilDestroy(this).Subscribe(delegate(long _)
			{
				this.OnUpdate();
			});
		}

		// Token: 0x06006085 RID: 24709 RVA: 0x00289402 File Offset: 0x00287802
		private void OnEnable()
		{
			this.Show();
		}

		// Token: 0x06006086 RID: 24710 RVA: 0x0028940A File Offset: 0x0028780A
		private void OnDisable()
		{
			this.Hide();
		}

		// Token: 0x06006087 RID: 24711 RVA: 0x00289414 File Offset: 0x00287814
		private void OnUpdate()
		{
			this.Point = EventPoint.GetTargetPoint();
			bool flag = this.Point != null && StoryPointEffect.Switch && base.isActiveAndEnabled;
			if (flag)
			{
				if (this.DiffPoint(this.Point))
				{
					this.SetPosition(this.Point);
				}
				if (!this._play)
				{
					this.Play();
				}
			}
			else if (this._play)
			{
				this.Stop();
			}
		}

		// Token: 0x06006088 RID: 24712 RVA: 0x0028949C File Offset: 0x0028789C
		public void Play()
		{
			this._play = true;
			if (this.Point == null || this._particle == null)
			{
				return;
			}
			if (this.DiffPoint(this.Point))
			{
				this.SetPosition(this.Point);
			}
			this._particle.Play(true);
		}

		// Token: 0x06006089 RID: 24713 RVA: 0x002894FC File Offset: 0x002878FC
		public bool DiffPoint(EventPoint point)
		{
			return !(point == null) && (point.GroupID != this._groupID || point.PointID != this._pointID);
		}

		// Token: 0x0600608A RID: 24714 RVA: 0x00289534 File Offset: 0x00287934
		public void SetPosition(EventPoint point)
		{
			if (point == null)
			{
				this._groupID = (this._pointID = -1);
			}
			else
			{
				this._groupID = point.GroupID;
				this._pointID = point.PointID;
				Transform labelPoint = point.LabelPoint;
				base.transform.SetPositionAndRotation(labelPoint.position, labelPoint.rotation);
			}
		}

		// Token: 0x0600608B RID: 24715 RVA: 0x00289598 File Offset: 0x00287998
		public void Stop()
		{
			this._play = false;
			if (this._particle != null)
			{
				this._particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
			}
		}

		// Token: 0x0600608C RID: 24716 RVA: 0x002895BB File Offset: 0x002879BB
		public void Show()
		{
			if (this._particleRenderer == null)
			{
				return;
			}
			if (!this._particleRenderer.enabled)
			{
				this._particleRenderer.enabled = true;
			}
		}

		// Token: 0x0600608D RID: 24717 RVA: 0x002895EB File Offset: 0x002879EB
		public void Hide()
		{
			if (this._particleRenderer == null)
			{
				return;
			}
			if (this._particleRenderer.enabled)
			{
				this._particleRenderer.enabled = false;
			}
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x0028961C File Offset: 0x00287A1C
		public void FadeOutAndDestroy()
		{
			if (this._particle == null)
			{
				UnityEngine.Object.Destroy(base.gameObject);
				return;
			}
			this.Stop();
			Observable.EveryUpdate().SkipWhile((long _) => this._particle.IsAlive(true)).Take(1).Subscribe(delegate(long _)
			{
				UnityEngine.Object.Destroy(base.gameObject);
			});
		}

		// Token: 0x040055A7 RID: 21927
		[SerializeField]
		private ParticleSystem _particle;

		// Token: 0x040055A8 RID: 21928
		[SerializeField]
		private ParticleSystemRenderer _particleRenderer;

		// Token: 0x040055AA RID: 21930
		private bool _play;

		// Token: 0x040055AB RID: 21931
		private int _groupID = -1;

		// Token: 0x040055AC RID: 21932
		private int _pointID = -1;
	}
}
