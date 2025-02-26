using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Exploder.Demo
{
	// Token: 0x0200038A RID: 906
	public class TargetManager : MonoBehaviour
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000FFD RID: 4093 RVA: 0x00059F11 File Offset: 0x00058311
		public static TargetManager Instance
		{
			get
			{
				return TargetManager.instance;
			}
		}

		// Token: 0x06000FFE RID: 4094 RVA: 0x00059F18 File Offset: 0x00058318
		private void Awake()
		{
			TargetManager.instance = this;
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00059F20 File Offset: 0x00058320
		private void Start()
		{
			ExploderUtils.SetActive(this.CrosshairGun.gameObject, true);
			ExploderUtils.SetActive(this.CrosshairHand.gameObject, true);
			ExploderUtils.SetActive(this.PanelText.gameObject, true);
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x00059F58 File Offset: 0x00058358
		private void Update()
		{
			Ray ray = this.MouseLookCamera.mainCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));
			UnityEngine.Debug.DrawRay(ray.origin, ray.direction * 10f, Color.red, 0f);
			this.CrosshairGun.color = Color.white;
			this.TargetObject = null;
			this.TargetType = TargetType.None;
			this.TargetPosition = Vector3.zero;
			List<RaycastHit> list = new List<RaycastHit>(Physics.RaycastAll(ray, float.PositiveInfinity));
			GameObject gameObject = null;
			if (list.Count > 0)
			{
				list.Sort((RaycastHit a, RaycastHit b) => (this.MouseLookCamera.transform.position - a.point).sqrMagnitude.CompareTo((this.MouseLookCamera.transform.position - b.point).sqrMagnitude));
				gameObject = list[0].collider.gameObject;
				this.TargetPosition = list[0].point;
			}
			if (gameObject != null)
			{
				this.TargetObject = gameObject;
				if (this.IsDestroyableObject(this.TargetObject))
				{
					this.TargetType = TargetType.DestroyableObject;
				}
				else if (this.IsUseObject(this.TargetObject))
				{
					UseObject component = this.TargetObject.GetComponent<UseObject>();
					if (component && (this.MouseLookCamera.transform.position - component.transform.position).sqrMagnitude < component.UseRadius * component.UseRadius)
					{
						this.TargetType = TargetType.UseObject;
					}
				}
				else
				{
					this.TargetType = TargetType.Default;
				}
			}
			switch (this.TargetType)
			{
			case TargetType.DestroyableObject:
				this.CrosshairHand.enabled = false;
				this.CrosshairGun.enabled = true;
				this.CrosshairGun.color = Color.red;
				break;
			case TargetType.UseObject:
				this.CrosshairGun.enabled = false;
				this.CrosshairHand.enabled = true;
				this.PanelText.enabled = true;
				this.PanelText.text = this.TargetObject.GetComponent<UseObject>().HelperText;
				break;
			case TargetType.Default:
			case TargetType.None:
				this.CrosshairHand.enabled = false;
				this.CrosshairGun.enabled = true;
				this.CrosshairGun.color = Color.white;
				this.PanelText.enabled = false;
				break;
			}
			if (Input.GetKeyDown(KeyCode.E) && this.TargetType == TargetType.UseObject)
			{
				UseObject component2 = this.TargetObject.GetComponent<UseObject>();
				if (component2)
				{
					component2.Use();
				}
			}
		}

		// Token: 0x06001001 RID: 4097 RVA: 0x0005A1E4 File Offset: 0x000585E4
		private bool IsDestroyableObject(GameObject obj)
		{
			return obj.CompareTag("Exploder") || (obj.transform.parent && this.IsDestroyableObject(obj.transform.parent.gameObject));
		}

		// Token: 0x06001002 RID: 4098 RVA: 0x0005A230 File Offset: 0x00058630
		private bool IsUseObject(GameObject obj)
		{
			return obj.CompareTag("UseObject") || (obj.transform.parent && this.IsDestroyableObject(obj.transform.parent.gameObject));
		}

		// Token: 0x040011D1 RID: 4561
		private static TargetManager instance;

		// Token: 0x040011D2 RID: 4562
		public GameObject TargetObject;

		// Token: 0x040011D3 RID: 4563
		public TargetType TargetType;

		// Token: 0x040011D4 RID: 4564
		public Vector3 TargetPosition;

		// Token: 0x040011D5 RID: 4565
		public Image CrosshairGun;

		// Token: 0x040011D6 RID: 4566
		public Image CrosshairHand;

		// Token: 0x040011D7 RID: 4567
		public ExploderMouseLook MouseLookCamera;

		// Token: 0x040011D8 RID: 4568
		public Text PanelText;
	}
}
