using System;
using System.Collections.Generic;
using System.Linq;
using Exploder.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exploder.Demo
{
	// Token: 0x0200038E RID: 910
	public class DemoClickExplode : MonoBehaviour
	{
		// Token: 0x0600100D RID: 4109 RVA: 0x0005A3A8 File Offset: 0x000587A8
		private void Start()
		{
			Application.targetFrameRate = 60;
			this.Exploder = ExploderSingleton.Instance;
			if (this.Exploder.DontUseTag)
			{
				UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(Explodable));
				List<GameObject> list = new List<GameObject>(array.Length);
				list.AddRange(from Explodable ex in array
				where ex
				select ex.gameObject);
				this.DestroyableObjects = list.ToArray();
			}
			else
			{
				this.DestroyableObjects = GameObject.FindGameObjectsWithTag("Exploder");
			}
		}

		// Token: 0x0600100E RID: 4110 RVA: 0x0005A461 File Offset: 0x00058861
		private bool IsExplodable(GameObject obj)
		{
			if (this.Exploder.DontUseTag)
			{
				return obj.GetComponent<Explodable>() != null;
			}
			return obj.CompareTag(ExploderObject.Tag);
		}

		// Token: 0x0600100F RID: 4111 RVA: 0x0005A48C File Offset: 0x0005888C
		private void Update()
		{
			if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1))
			{
				Ray ray;
				if (this.Camera)
				{
					ray = this.Camera.ScreenPointToRay(Input.mousePosition);
				}
				else
				{
					ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				}
				RaycastHit raycastHit;
				if (Physics.Raycast(ray, out raycastHit))
				{
					GameObject gameObject = raycastHit.collider.gameObject;
					if (this.IsExplodable(gameObject))
					{
						if (Input.GetMouseButtonDown(0))
						{
							this.ExplodeObject(gameObject);
						}
						else
						{
							this.ExplodeAfterCrack(gameObject);
						}
					}
				}
			}
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0005A529 File Offset: 0x00058929
		private void ExplodeObject(GameObject obj)
		{
			this.Exploder.ExplodeObject(obj, new ExploderObject.OnExplosion(this.OnExplosion));
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0005A543 File Offset: 0x00058943
		private void OnExplosion(float time, ExploderObject.ExplosionState state)
		{
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0005A545 File Offset: 0x00058945
		private void OnCracked(float time, ExploderObject.ExplosionState state)
		{
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0005A547 File Offset: 0x00058947
		private void ExplodeAfterCrack(GameObject obj)
		{
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0005A54C File Offset: 0x0005894C
		private void OnGUI()
		{
			if (GUI.Button(new Rect(10f, 10f, 100f, 30f), "Reset") && !this.Exploder.DestroyOriginalObject)
			{
				foreach (GameObject obj in this.DestroyableObjects)
				{
					ExploderUtils.SetActiveRecursively(obj, true);
				}
				ExploderUtils.SetActive(this.Exploder.gameObject, true);
			}
			if (GUI.Button(new Rect(10f, 50f, 100f, 30f), "NextScene"))
			{
				SceneManager.LoadScene(0);
			}
		}

		// Token: 0x040011DF RID: 4575
		private GameObject[] DestroyableObjects;

		// Token: 0x040011E0 RID: 4576
		private ExploderObject Exploder;

		// Token: 0x040011E1 RID: 4577
		public Camera Camera;
	}
}
