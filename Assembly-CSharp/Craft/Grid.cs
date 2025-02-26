using System;
using UnityEngine;

namespace Craft
{
	// Token: 0x0200089E RID: 2206
	public class Grid : MonoBehaviour
	{
		// Token: 0x06003957 RID: 14679 RVA: 0x00151D4C File Offset: 0x0015014C
		public Vector3 GetNearestPointOnGrid(Vector3 _position)
		{
			_position = base.transform.InverseTransformPoint(_position);
			int num = Mathf.FloorToInt(_position.x / this.size);
			int num2 = Mathf.Max(Mathf.FloorToInt(_position.y / this.size), 0);
			int num3 = Mathf.FloorToInt(_position.z / this.size);
			Vector3 vector = new Vector3((float)num * this.size, (float)num2 * this.size, (float)num3 * this.size);
			vector += this.center * this.size;
			return base.transform.TransformPoint(vector);
		}

		// Token: 0x06003958 RID: 14680 RVA: 0x00151DEF File Offset: 0x001501EF
		public Vector2 GetGridPos(Vector3 _position)
		{
			return this.ConvertPos(this.GetNearestPointOnGrid(_position));
		}

		// Token: 0x06003959 RID: 14681 RVA: 0x00151E00 File Offset: 0x00150200
		public Vector2 ConvertPos(Vector3 _position)
		{
			Vector3 vector = base.transform.InverseTransformPoint(_position) - this.center * this.size;
			float num = this.width - vector.x - 1f;
			float num2 = this.height - vector.z - 1f;
			bool flag = 0f <= num && num <= this.width - 1f && 0f <= num2 && num2 <= this.height - 1f;
			return new Vector2((!flag) ? -1f : num, (!flag) ? -1f : num2);
		}

		// Token: 0x04003902 RID: 14594
		[SerializeField]
		private float size = 1f;

		// Token: 0x04003903 RID: 14595
		[SerializeField]
		private float height = 10f;

		// Token: 0x04003904 RID: 14596
		[SerializeField]
		private float width = 10f;

		// Token: 0x04003905 RID: 14597
		[SerializeField]
		private Vector3 center = new Vector3(0.5f, 0f, 0.5f);
	}
}
