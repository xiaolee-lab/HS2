using System;
using Exploder.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exploder.Demo
{
	// Token: 0x0200038F RID: 911
	public class DemoClickPartialExplode : MonoBehaviour
	{
		// Token: 0x06001018 RID: 4120 RVA: 0x0005A610 File Offset: 0x00058A10
		private void Start()
		{
			Application.targetFrameRate = 60;
			this.exploder = ExploderSingleton.Instance;
			foreach (GameObject obj in this.toCrack)
			{
				this.exploder.CrackObject(obj);
			}
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0005A65C File Offset: 0x00058A5C
		private void Update()
		{
			if (Input.GetMouseButtonDown(0))
			{
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit))
				{
					GameObject gameObject = raycastHit.collider.gameObject;
					this.exploder.ExplodePartial(gameObject, ray.direction, raycastHit.point, 1f, delegate(float ms, ExploderObject.ExplosionState state)
					{
					});
				}
			}
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0005A6DC File Offset: 0x00058ADC
		private void OnGUI()
		{
			if (GUI.Button(new Rect(10f, 10f, 100f, 30f), "Reset"))
			{
			}
			if (GUI.Button(new Rect(10f, 50f, 100f, 30f), "NextScene"))
			{
				SceneManager.LoadScene(1);
			}
		}

		// Token: 0x040011E4 RID: 4580
		public GameObject[] toCrack;

		// Token: 0x040011E5 RID: 4581
		private ExploderObject exploder;
	}
}
