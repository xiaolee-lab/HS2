using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Housing
{
	// Token: 0x020008C8 RID: 2248
	public class GuideManager : Singleton<GuideManager>
	{
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06003ACB RID: 15051 RVA: 0x00157B7D File Offset: 0x00155F7D
		// (set) Token: 0x06003ACC RID: 15052 RVA: 0x00157B85 File Offset: 0x00155F85
		public Transform TransformRoot { get; set; }

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06003ACD RID: 15053 RVA: 0x00157B8E File Offset: 0x00155F8E
		// (set) Token: 0x06003ACE RID: 15054 RVA: 0x00157B96 File Offset: 0x00155F96
		public Vector3 GridArea { get; private set; } = new Vector3(200f, 100f, 200f);

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003ACF RID: 15055 RVA: 0x00157BA0 File Offset: 0x00155FA0
		public GuideObject GuideObject
		{
			[CompilerGenerated]
			get
			{
				GuideObject result;
				if ((result = this.m_guideObject) == null)
				{
					result = (this.m_guideObject = this.objManipulator.GetComponent<GuideObject>());
				}
				return result;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06003AD0 RID: 15056 RVA: 0x00157BCE File Offset: 0x00155FCE
		// (set) Token: 0x06003AD1 RID: 15057 RVA: 0x00157BD6 File Offset: 0x00155FD6
		public bool IsGuide { get; set; }

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06003AD2 RID: 15058 RVA: 0x00157BDF File Offset: 0x00155FDF
		// (set) Token: 0x06003AD3 RID: 15059 RVA: 0x00157BEC File Offset: 0x00155FEC
		public bool VisibleGrid
		{
			get
			{
				return this.meshRenderer.enabled;
			}
			set
			{
				this.meshRenderer.enabled = value;
			}
		}

		// Token: 0x06003AD4 RID: 15060 RVA: 0x00157BFC File Offset: 0x00155FFC
		public void Init(Vector3 _gridArea)
		{
			this.GridArea = _gridArea;
			this.meshRenderer.transform.localScale = new Vector3(_gridArea.x * 0.1f, 1f, _gridArea.z * 0.1f);
			Material material = this.meshRenderer.material;
			material.mainTextureScale = new Vector2(_gridArea.x, _gridArea.z);
			this.meshRenderer.material = material;
		}

		// Token: 0x06003AD5 RID: 15061 RVA: 0x00157C78 File Offset: 0x00156078
		public bool CorrectPos(ObjectCtrl _objectCtrl, ref Vector3 _pos)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			_objectCtrl.GetLocalMinMax(_pos, _objectCtrl.Rotation, this.TransformRoot, ref zero, ref zero2);
			Vector3 vector = this.GridArea * 0.5f;
			Vector3 zero3 = Vector3.zero;
			bool flag = false;
			if (-vector.x > zero.x)
			{
				zero3.x = vector.x + zero.x;
				flag |= true;
			}
			else if (vector.x < zero2.x)
			{
				zero3.x = zero2.x - vector.x;
				flag |= true;
			}
			if (0f > zero.y)
			{
				zero3.y = zero.y;
				flag |= true;
			}
			else if (this.GridArea.y < zero2.y)
			{
				zero3.y = zero2.y - this.GridArea.y;
				flag |= true;
			}
			if (-vector.z > zero.z)
			{
				zero3.z = vector.z + zero.z;
				flag |= true;
			}
			else if (vector.z < zero2.z)
			{
				zero3.z = zero2.z - vector.z;
				flag |= true;
			}
			_pos -= this.TransformRoot.TransformVector(zero3);
			return flag;
		}

		// Token: 0x06003AD6 RID: 15062 RVA: 0x00157E18 File Offset: 0x00156218
		public bool CheckRot(ObjectCtrl _objectCtrl)
		{
			Vector3 zero = Vector3.zero;
			Vector3 zero2 = Vector3.zero;
			_objectCtrl.GetLocalMinMax(_objectCtrl.Position, _objectCtrl.Rotation, this.TransformRoot, ref zero, ref zero2);
			Vector3Int vector3Int = new Vector3Int(this.FloorToInt(zero.x), this.FloorToInt(zero.y), this.FloorToInt(zero.z));
			Vector3Int vector3Int2 = new Vector3Int(this.FloorToInt(zero2.x), this.FloorToInt(zero2.y), this.FloorToInt(zero2.z));
			Vector3 vector = this.GridArea * 0.5f;
			return -vector.x <= (float)vector3Int.x && vector.x >= (float)vector3Int2.x && 0 <= vector3Int.y && this.GridArea.y >= (float)vector3Int2.y && -vector.z <= (float)vector3Int.z && vector.z >= (float)vector3Int2.z;
		}

		// Token: 0x06003AD7 RID: 15063 RVA: 0x00157F40 File Offset: 0x00156340
		private int FloorToInt(float _value)
		{
			bool flag = _value < 0f;
			return Mathf.FloorToInt(Mathf.Abs(_value)) * ((!flag) ? 1 : -1);
		}

		// Token: 0x06003AD8 RID: 15064 RVA: 0x00157F6F File Offset: 0x0015636F
		public bool NoCameraCtrl()
		{
			return this.IsGuide;
		}

		// Token: 0x06003AD9 RID: 15065 RVA: 0x00157F78 File Offset: 0x00156378
		private void OnSelect(ObjectCtrl[] _objectCtrls)
		{
			ObjectCtrl target = null;
			if (!_objectCtrls.IsNullOrEmpty<ObjectCtrl>())
			{
				target = _objectCtrls.FirstOrDefault((ObjectCtrl _oc) => _oc is OCItem);
			}
			this.GuideObject.SetTarget(target);
		}

		// Token: 0x06003ADA RID: 15066 RVA: 0x00157FC2 File Offset: 0x001563C2
		private void Start()
		{
			Selection instance = Singleton<Selection>.Instance;
			instance.onSelectFunc = (Action<ObjectCtrl[]>)Delegate.Combine(instance.onSelectFunc, new Action<ObjectCtrl[]>(this.OnSelect));
			this.GuideObject.visible = false;
		}

		// Token: 0x040039FC RID: 14844
		[SerializeField]
		[Header("基本設計")]
		private MeshCollider meshCollider;

		// Token: 0x040039FD RID: 14845
		[SerializeField]
		private MeshRenderer meshRenderer;

		// Token: 0x040039FE RID: 14846
		[SerializeField]
		[Header("操作軸")]
		private GameObject objManipulator;

		// Token: 0x04003A01 RID: 14849
		private GuideObject m_guideObject;
	}
}
