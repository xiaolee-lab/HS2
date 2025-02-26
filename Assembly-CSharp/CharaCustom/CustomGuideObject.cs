using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CharaCustom
{
	// Token: 0x02000A15 RID: 2581
	public class CustomGuideObject : MonoBehaviour
	{
		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06004CCB RID: 19659 RVA: 0x001D81E9 File Offset: 0x001D65E9
		// (set) Token: 0x06004CCC RID: 19660 RVA: 0x001D81F1 File Offset: 0x001D65F1
		public int ctrlAxisType { get; set; }

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06004CCD RID: 19661 RVA: 0x001D81FA File Offset: 0x001D65FA
		// (set) Token: 0x06004CCE RID: 19662 RVA: 0x001D8202 File Offset: 0x001D6602
		public float scaleRate
		{
			get
			{
				return this.m_ScaleRate;
			}
			set
			{
				if (this.m_ScaleRate != value)
				{
					this.m_ScaleRate = value;
					this.UpdateScale();
				}
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06004CCF RID: 19663 RVA: 0x001D821D File Offset: 0x001D661D
		// (set) Token: 0x06004CD0 RID: 19664 RVA: 0x001D8225 File Offset: 0x001D6625
		public float scaleRot
		{
			get
			{
				return this.m_ScaleRot;
			}
			set
			{
				if (this.m_ScaleRot != value)
				{
					this.m_ScaleRot = value;
					this.UpdateScale();
				}
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06004CD1 RID: 19665 RVA: 0x001D8240 File Offset: 0x001D6640
		// (set) Token: 0x06004CD2 RID: 19666 RVA: 0x001D8248 File Offset: 0x001D6648
		public float scaleSelect
		{
			get
			{
				return this.m_ScaleSelect;
			}
			set
			{
				if (this.m_ScaleSelect != value)
				{
					this.m_ScaleSelect = value;
					this.UpdateScale();
				}
			}
		}

		// Token: 0x06004CD3 RID: 19667 RVA: 0x001D8263 File Offset: 0x001D6663
		public void SetMode(int _mode)
		{
			this.roots.SetActiveToggle(_mode);
		}

		// Token: 0x06004CD4 RID: 19668 RVA: 0x001D8274 File Offset: 0x001D6674
		public void UpdateScale()
		{
			this.roots.GetObject(0).transform.localScale = Vector3.one * this.m_ScaleRate * this.scaleAxis;
			this.roots.GetObject(1).transform.localScale = Vector3.one * 15f * this.m_ScaleRate * 1.1f * this.m_ScaleRot * this.scaleAxis;
		}

		// Token: 0x06004CD5 RID: 19669 RVA: 0x001D8304 File Offset: 0x001D6704
		public void SetLayer(GameObject _object, int _layer)
		{
			if (_object == null)
			{
				return;
			}
			_object.layer = _layer;
			Transform transform = _object.transform;
			int childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				this.SetLayer(transform.GetChild(i).gameObject, _layer);
			}
		}

		// Token: 0x06004CD6 RID: 19670 RVA: 0x001D8358 File Offset: 0x001D6758
		private void Awake()
		{
			List<CustomGuideBase> list = new List<CustomGuideBase>();
			for (int i = 0; i < 2; i++)
			{
				list.AddRange(this.roots.GetObject(i).GetComponentsInChildren<CustomGuideBase>().ToList<CustomGuideBase>());
			}
			CustomGuideBase[] array = list.ToArray();
			for (int j = 0; j < array.Length; j++)
			{
				array[j].guideObject = this;
			}
			this.SetMode(0);
			this.UpdateScale();
		}

		// Token: 0x06004CD7 RID: 19671 RVA: 0x001D83CC File Offset: 0x001D67CC
		private void LateUpdate()
		{
			base.transform.localPosition = base.transform.InverseTransformVector(this.amount.position);
			this.roots.GetObject(1).SafeProcObject(delegate(GameObject o)
			{
				o.transform.localRotation = Quaternion.Euler(this.amount.rotation);
			});
		}

		// Token: 0x0400465F RID: 18015
		public CustomGuideObject.Amount amount = new CustomGuideObject.Amount();

		// Token: 0x04004660 RID: 18016
		public bool isDrag;

		// Token: 0x04004662 RID: 18018
		[Range(0.01f, 3f)]
		public float scaleAxis = 2f;

		// Token: 0x04004663 RID: 18019
		[Range(0.01f, 3f)]
		public float speedMove = 1f;

		// Token: 0x04004664 RID: 18020
		public CameraControl_Ver2 ccv2;

		// Token: 0x04004665 RID: 18021
		[SerializeField]
		protected ObjectCategoryBehaviour roots;

		// Token: 0x04004666 RID: 18022
		protected float m_ScaleRate = 5f;

		// Token: 0x04004667 RID: 18023
		protected float m_ScaleRot = 0.05f;

		// Token: 0x04004668 RID: 18024
		protected float m_ScaleSelect = 0.1f;

		// Token: 0x02000A16 RID: 2582
		public class Amount
		{
			// Token: 0x04004669 RID: 18025
			public Vector3 position = Vector3.zero;

			// Token: 0x0400466A RID: 18026
			public Vector3 rotation = Vector3.zero;
		}
	}
}
