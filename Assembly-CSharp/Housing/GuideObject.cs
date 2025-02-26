using System;
using System.Collections;
using System.Linq;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UniRx;
using UnityEngine;

namespace Housing
{
	// Token: 0x020008C4 RID: 2244
	public class GuideObject : MonoBehaviour
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06003AA2 RID: 15010 RVA: 0x0015712D File Offset: 0x0015552D
		// (set) Token: 0x06003AA3 RID: 15011 RVA: 0x00157135 File Offset: 0x00155535
		public Transform TransformTarget { get; private set; }

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06003AA4 RID: 15012 RVA: 0x0015713E File Offset: 0x0015553E
		// (set) Token: 0x06003AA5 RID: 15013 RVA: 0x00157146 File Offset: 0x00155546
		public ObjectCtrl ObjectCtrl { get; private set; }

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06003AA6 RID: 15014 RVA: 0x0015714F File Offset: 0x0015554F
		public GuideMove[] guideMove
		{
			[CompilerGenerated]
			get
			{
				return (from g in this.guide.Skip(1).Take(3)
				select g as GuideMove).ToArray<GuideMove>();
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06003AA7 RID: 15015 RVA: 0x0015718A File Offset: 0x0015558A
		public GuideBase[] guides
		{
			[CompilerGenerated]
			get
			{
				return this.guide;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06003AA8 RID: 15016 RVA: 0x00157192 File Offset: 0x00155592
		public GameObject objCenter
		{
			[CompilerGenerated]
			get
			{
				return this.m_objCenter;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06003AA9 RID: 15017 RVA: 0x0015719A File Offset: 0x0015559A
		// (set) Token: 0x06003AAA RID: 15018 RVA: 0x001571A7 File Offset: 0x001555A7
		public float scaleRate
		{
			get
			{
				return this._scaleRate.Value;
			}
			set
			{
				this._scaleRate.Value = value;
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06003AAB RID: 15019 RVA: 0x001571B5 File Offset: 0x001555B5
		// (set) Token: 0x06003AAC RID: 15020 RVA: 0x001571C2 File Offset: 0x001555C2
		public bool visible
		{
			get
			{
				return this._visible.Value;
			}
			set
			{
				this._visible.Value = value;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06003AAD RID: 15021 RVA: 0x001571D0 File Offset: 0x001555D0
		// (set) Token: 0x06003AAE RID: 15022 RVA: 0x001571DD File Offset: 0x001555DD
		public bool visibleOutside
		{
			get
			{
				return this._visibleOutside.Value;
			}
			set
			{
				this._visibleOutside.Value = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x001571EB File Offset: 0x001555EB
		// (set) Token: 0x06003AB0 RID: 15024 RVA: 0x001571F8 File Offset: 0x001555F8
		public bool visibleCenter
		{
			get
			{
				return this.rendererCenter.enabled;
			}
			set
			{
				this.rendererCenter.enabled = value;
			}
		}

		// Token: 0x06003AB1 RID: 15025 RVA: 0x00157208 File Offset: 0x00155608
		public void SetTarget(ObjectCtrl _objectCtrl)
		{
			if (_objectCtrl is OCFolder)
			{
				_objectCtrl = null;
			}
			this.ObjectCtrl = _objectCtrl;
			ObjectCtrl objectCtrl = this.ObjectCtrl;
			this.TransformTarget = ((objectCtrl != null) ? objectCtrl.GameObject.transform : null);
			this.TransformTarget.SafeProc(delegate(Transform _t)
			{
				base.transform.SetPositionAndRotation(_t.position, _t.rotation);
			});
			this.visible = (_objectCtrl != null);
		}

		// Token: 0x06003AB2 RID: 15026 RVA: 0x0015726F File Offset: 0x0015566F
		public void SetScale()
		{
		}

		// Token: 0x06003AB3 RID: 15027 RVA: 0x00157274 File Offset: 0x00155674
		public void SetLayer(GameObject _object, int _layer)
		{
			if (_object == null)
			{
				return;
			}
			_object.layer = _layer;
			IEnumerator enumerator = _object.transform.GetEnumerator();
			try
			{
				while (enumerator.MoveNext())
				{
					object obj = enumerator.Current;
					Transform transform = (Transform)obj;
					this.SetLayer(transform.gameObject, _layer);
				}
			}
			finally
			{
				IDisposable disposable;
				if ((disposable = (enumerator as IDisposable)) != null)
				{
					disposable.Dispose();
				}
			}
		}

		// Token: 0x06003AB4 RID: 15028 RVA: 0x001572F4 File Offset: 0x001556F4
		public void SetVisibleCenter(bool _value)
		{
			this.m_objCenter.SetActive(_value);
		}

		// Token: 0x06003AB5 RID: 15029 RVA: 0x00157304 File Offset: 0x00155704
		private void Awake()
		{
			foreach (GuideBase guideBase in this.guide)
			{
				guideBase.Init(this);
			}
			this.visibleCenter = true;
			if (this.mainCamera == null)
			{
				this.mainCamera = Camera.main;
			}
			this._scaleRate.Subscribe(delegate(float _f)
			{
				this.SetScale();
			});
			this._visible.Subscribe(delegate(bool _b)
			{
				foreach (GuideBase guideBase2 in this.guide)
				{
					guideBase2.Draw = (_b & this.visibleOutside);
				}
				Singleton<CraftScene>.Instance.UICtrl.ManipulateUICtrl.Visible = (_b & this.visibleOutside);
			});
			this._visibleOutside.Subscribe(delegate(bool _b)
			{
				foreach (GuideBase guideBase2 in this.guide)
				{
					guideBase2.Draw = (_b & this.visible);
				}
				Singleton<CraftScene>.Instance.UICtrl.ManipulateUICtrl.Visible = (_b & this.visible);
			});
		}

		// Token: 0x06003AB6 RID: 15030 RVA: 0x001573A4 File Offset: 0x001557A4
		private void Update()
		{
			float t = Mathf.InverseLerp(this.minDis, this.maxDis, Vector3.Distance(this.mainCamera.transform.position, base.transform.position));
			base.transform.localScale = Vector3.one * Mathf.Lerp(this.minScale, this.maxScale, t);
		}

		// Token: 0x06003AB7 RID: 15031 RVA: 0x0015740C File Offset: 0x0015580C
		private void LateUpdate()
		{
			this.TransformTarget.SafeProc(delegate(Transform _t)
			{
				base.transform.SetPositionAndRotation(_t.position, _t.rotation);
			});
			if (Singleton<GuideManager>.IsInstance() && Singleton<GuideManager>.Instance.TransformRoot)
			{
				this.roots[0].transform.rotation = Singleton<GuideManager>.Instance.TransformRoot.rotation;
			}
			else
			{
				this.roots[0].transform.eulerAngles = Vector3.zero;
			}
		}

		// Token: 0x040039E0 RID: 14816
		[SerializeField]
		private GameObject[] roots;

		// Token: 0x040039E1 RID: 14817
		[SerializeField]
		private GuideBase[] guide;

		// Token: 0x040039E2 RID: 14818
		[SerializeField]
		private GameObject m_objCenter;

		// Token: 0x040039E3 RID: 14819
		[SerializeField]
		private MeshRenderer rendererCenter;

		// Token: 0x040039E4 RID: 14820
		[SerializeField]
		[Header("サイズ補助")]
		private Camera mainCamera;

		// Token: 0x040039E5 RID: 14821
		[SerializeField]
		private float minDis = 20f;

		// Token: 0x040039E6 RID: 14822
		[SerializeField]
		private float maxDis = 60f;

		// Token: 0x040039E7 RID: 14823
		[SerializeField]
		private float minScale = 5f;

		// Token: 0x040039E8 RID: 14824
		[SerializeField]
		private float maxScale = 20f;

		// Token: 0x040039E9 RID: 14825
		[ReadOnly]
		[Header("サイズ関係")]
		public Vector3 min = Vector3.zero;

		// Token: 0x040039EA RID: 14826
		[ReadOnly]
		public Vector3 max = Vector3.zero;

		// Token: 0x040039EB RID: 14827
		private FloatReactiveProperty _scaleRate = new FloatReactiveProperty(1f);

		// Token: 0x040039EC RID: 14828
		private BoolReactiveProperty _visible = new BoolReactiveProperty(true);

		// Token: 0x040039ED RID: 14829
		private BoolReactiveProperty _visibleOutside = new BoolReactiveProperty(true);
	}
}
