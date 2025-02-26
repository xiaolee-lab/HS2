using System;
using UnityEngine;
using UnityEngine.UI;

namespace MeshBrush.Examples
{
	// Token: 0x020003ED RID: 1005
	public class RuntimeExample : MonoBehaviour
	{
		// Token: 0x060011D8 RID: 4568 RVA: 0x0006AF21 File Offset: 0x00069321
		private void Start()
		{
			this.mainCamera = Camera.main.transform;
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x0006AF34 File Offset: 0x00069334
		private void Update()
		{
			this.meshbrushInstance.radius = this.radiusSlider.value;
			this.meshbrushInstance.scatteringRange = new Vector2(this.scatteringSlider.value, this.scatteringSlider.value);
			this.meshbrushInstance.densityRange = new Vector2(this.densitySlider.value, this.densitySlider.value);
			RaycastHit brushLocation;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out brushLocation))
			{
				this.circleBrush.position = brushLocation.point;
				this.circleBrush.forward = -brushLocation.normal;
				this.circleBrush.localScale = new Vector3(this.meshbrushInstance.radius, this.meshbrushInstance.radius, 1f);
				if (Input.GetKey(this.meshbrushInstance.paintKey))
				{
					this.meshbrushInstance.PaintMeshes(brushLocation);
				}
				if (Input.GetKey(this.meshbrushInstance.deleteKey))
				{
					this.meshbrushInstance.DeleteMeshes(brushLocation);
				}
				if (Input.GetKey(this.meshbrushInstance.randomizeKey))
				{
					this.meshbrushInstance.RandomizeMeshes(brushLocation);
				}
			}
		}

		// Token: 0x0400140C RID: 5132
		[SerializeField]
		private MeshBrush meshbrushInstance;

		// Token: 0x0400140D RID: 5133
		[SerializeField]
		private Transform circleBrush;

		// Token: 0x0400140E RID: 5134
		[SerializeField]
		private Slider radiusSlider;

		// Token: 0x0400140F RID: 5135
		[SerializeField]
		private Slider scatteringSlider;

		// Token: 0x04001410 RID: 5136
		[SerializeField]
		private Slider densitySlider;

		// Token: 0x04001411 RID: 5137
		private Transform mainCamera;
	}
}
