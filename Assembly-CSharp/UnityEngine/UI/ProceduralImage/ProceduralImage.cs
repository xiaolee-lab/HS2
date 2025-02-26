using System;

namespace UnityEngine.UI.ProceduralImage
{
	// Token: 0x0200062E RID: 1582
	[ExecuteInEditMode]
	[AddComponentMenu("UI/Procedural Image")]
	public class ProceduralImage : Image
	{
		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x000D6EB4 File Offset: 0x000D52B4
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x000D6EBC File Offset: 0x000D52BC
		public float BorderWidth
		{
			get
			{
				return this.borderWidth;
			}
			set
			{
				this.borderWidth = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x0600259F RID: 9631 RVA: 0x000D6ECB File Offset: 0x000D52CB
		// (set) Token: 0x060025A0 RID: 9632 RVA: 0x000D6ED3 File Offset: 0x000D52D3
		public float FalloffDistance
		{
			get
			{
				return this.falloffDistance;
			}
			set
			{
				this.falloffDistance = value;
				this.SetVerticesDirty();
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060025A1 RID: 9633 RVA: 0x000D6EE4 File Offset: 0x000D52E4
		// (set) Token: 0x060025A2 RID: 9634 RVA: 0x000D6F35 File Offset: 0x000D5335
		protected ProceduralImageModifier Modifier
		{
			get
			{
				if (this.modifier == null)
				{
					this.modifier = base.GetComponent<ProceduralImageModifier>();
					if (this.modifier == null)
					{
						this.ModifierType = typeof(FreeModifier);
					}
				}
				return this.modifier;
			}
			set
			{
				this.modifier = value;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060025A3 RID: 9635 RVA: 0x000D6F3E File Offset: 0x000D533E
		// (set) Token: 0x060025A4 RID: 9636 RVA: 0x000D6F4B File Offset: 0x000D534B
		public System.Type ModifierType
		{
			get
			{
				return this.Modifier.GetType();
			}
			set
			{
				if (base.GetComponent<ProceduralImageModifier>() != null)
				{
					Object.DestroyImmediate(base.GetComponent<ProceduralImageModifier>());
				}
				base.gameObject.AddComponent(value);
				this.Modifier = base.GetComponent<ProceduralImageModifier>();
				this.SetAllDirty();
			}
		}

		// Token: 0x060025A5 RID: 9637 RVA: 0x000D6F88 File Offset: 0x000D5388
		protected override void OnEnable()
		{
			base.OnEnable();
			this.Init();
		}

		// Token: 0x060025A6 RID: 9638 RVA: 0x000D6F98 File Offset: 0x000D5398
		private void Init()
		{
			base.preserveAspect = false;
			if (base.sprite == null)
			{
				base.sprite = EmptySprite.Get();
			}
			if (ProceduralImage.materialInstance == null)
			{
				ProceduralImage.materialInstance = new Material(Shader.Find("UI/Procedural UI Image"));
			}
			this.material = ProceduralImage.materialInstance;
		}

		// Token: 0x060025A7 RID: 9639 RVA: 0x000D6FF8 File Offset: 0x000D53F8
		private Vector4 FixRadius(Vector4 vec)
		{
			Rect rect = base.rectTransform.rect;
			vec = new Vector4(Mathf.Max(vec.x, 0f), Mathf.Max(vec.y, 0f), Mathf.Max(vec.z, 0f), Mathf.Max(vec.w, 0f));
			float d = Mathf.Min(new float[]
			{
				rect.width / (vec.x + vec.y),
				rect.width / (vec.z + vec.w),
				rect.height / (vec.x + vec.w),
				rect.height / (vec.z + vec.y),
				1f
			});
			return vec * d;
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x000D70DF File Offset: 0x000D54DF
		protected override void OnPopulateMesh(VertexHelper toFill)
		{
			base.OnPopulateMesh(toFill);
			this.EncodeAllInfoIntoVertices(toFill, this.CalculateInfo());
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x000D70F8 File Offset: 0x000D54F8
		private ProceduralImageInfo CalculateInfo()
		{
			if (base.sprite == null)
			{
				base.sprite = EmptySprite.Get();
			}
			Rect pixelAdjustedRect = base.GetPixelAdjustedRect();
			Vector3[] array = new Vector3[4];
			base.rectTransform.GetWorldCorners(array);
			float num = Vector3.Distance(array[1], array[2]) / pixelAdjustedRect.width;
			num /= Mathf.Max(0f, this.falloffDistance);
			Vector4 a = this.FixRadius(this.Modifier.CalculateRadius(pixelAdjustedRect));
			float num2 = Mathf.Min(pixelAdjustedRect.width, pixelAdjustedRect.height);
			ProceduralImageInfo result = new ProceduralImageInfo(pixelAdjustedRect.width + this.falloffDistance, pixelAdjustedRect.height + this.falloffDistance, this.falloffDistance, num, a / num2, this.borderWidth / num2 * 2f);
			return result;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x000D71E0 File Offset: 0x000D55E0
		private void EncodeAllInfoIntoVertices(VertexHelper vh, ProceduralImageInfo info)
		{
			UIVertex vertex = default(UIVertex);
			Vector2 uv = new Vector2(info.width, info.height);
			Vector2 uv2 = new Vector2(this.EncodeFloats_0_1_16_16(info.radius.x, info.radius.y), this.EncodeFloats_0_1_16_16(info.radius.z, info.radius.w));
			Vector2 uv3 = new Vector2((info.borderWidth != 0f) ? Mathf.Clamp01(info.borderWidth) : 1f, info.pixelSize);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref vertex, i);
				vertex.position += (vertex.uv0 - new Vector3(0.5f, 0.5f)) * info.fallOffDistance;
				vertex.uv1 = uv;
				vertex.uv2 = uv2;
				vertex.uv3 = uv3;
				vh.SetUIVertex(vertex, i);
			}
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x000D7308 File Offset: 0x000D5708
		private float EncodeFloats_0_1_16_16(float a, float b)
		{
			Vector2 rhs = new Vector2(1f, 1.5259022E-05f);
			return Vector2.Dot(new Vector2(Mathf.Floor(a * 65534f) / 65535f, Mathf.Floor(b * 65534f) / 65535f), rhs);
		}

		// Token: 0x04002562 RID: 9570
		[SerializeField]
		private float borderWidth;

		// Token: 0x04002563 RID: 9571
		private ProceduralImageModifier modifier;

		// Token: 0x04002564 RID: 9572
		private static Material materialInstance;

		// Token: 0x04002565 RID: 9573
		[SerializeField]
		private float falloffDistance = 1f;
	}
}
