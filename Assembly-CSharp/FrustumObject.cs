using System;
using System.Collections.Generic;
using System.Text;
using IllusionUtility.GetUtility;
using UnityEngine;

// Token: 0x020010BC RID: 4284
public class FrustumObject : CollisionCamera
{
	// Token: 0x06008EE5 RID: 36581 RVA: 0x003B6D77 File Offset: 0x003B5177
	private new void Start()
	{
		base.Start();
	}

	// Token: 0x06008EE6 RID: 36582 RVA: 0x003B6D80 File Offset: 0x003B5180
	private void Update()
	{
		base.SetCollision();
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(base.GetComponent<Camera>());
		foreach (GameObject gameObject in this.objDels)
		{
			List<GameObject> list = new List<GameObject>();
			gameObject.transform.parent.FindLoopAll(list);
			if (!(gameObject.GetComponent<Collider>() == null))
			{
				if (!(gameObject.GetComponent<Renderer>() == null))
				{
					foreach (GameObject gameObject2 in list)
					{
						if (gameObject2.GetComponent<Renderer>())
						{
							gameObject2.GetComponent<Renderer>().enabled = true;
						}
					}
					if (GeometryUtility.TestPlanesAABB(planes, gameObject.GetComponent<Collider>().bounds))
					{
						Vector3 targetPos = this.camCtrl.TargetPos;
						float num = Vector3.Distance(targetPos, base.transform.position);
						float num2 = Vector3.Distance(gameObject.GetComponent<Collider>().bounds.center, base.transform.position);
						if (num > num2)
						{
							foreach (GameObject gameObject3 in list)
							{
								if (gameObject3.GetComponent<Renderer>())
								{
									gameObject3.GetComponent<Renderer>().enabled = false;
								}
							}
						}
					}
				}
			}
		}
	}

	// Token: 0x06008EE7 RID: 36583 RVA: 0x003B6F30 File Offset: 0x003B5330
	private void OnGUI()
	{
		StringBuilder stringBuilder = new StringBuilder();
		float height = 1000f;
		int num = 0;
		foreach (GameObject gameObject in this.objDels)
		{
			if (gameObject.GetComponent<Renderer>() != null && (!gameObject.GetComponent<Renderer>().enabled || !gameObject.activeSelf))
			{
				num++;
			}
		}
		stringBuilder.Append("Count:" + num.ToString());
		stringBuilder.Append("\n");
		foreach (GameObject gameObject2 in this.objDels)
		{
			if (gameObject2.GetComponent<Renderer>() != null && (!gameObject2.GetComponent<Renderer>().enabled || !gameObject2.activeSelf))
			{
				stringBuilder.Append(gameObject2.name);
				stringBuilder.Append("\n");
			}
		}
		GUI.Box(new Rect(5f, 5f, 300f, height), string.Empty);
		GUI.Label(new Rect(10f, 5f, 1000f, height), stringBuilder.ToString());
	}

	// Token: 0x06008EE8 RID: 36584 RVA: 0x003B707C File Offset: 0x003B547C
	public object[] GetObjects(Vector3 position, float distance, float fov, Vector3 direction)
	{
		List<GameObject> list = new List<GameObject>();
		UnityEngine.Object[] array = UnityEngine.Object.FindObjectsOfType(typeof(GameObject));
		base.transform.position = position;
		base.transform.forward = direction;
		base.GetComponent<Camera>().fieldOfView = fov;
		base.GetComponent<Camera>().farClipPlane = distance;
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(base.GetComponent<Camera>());
		foreach (GameObject gameObject in array)
		{
			if (!(gameObject.GetComponent<Collider>() == null))
			{
				if (GeometryUtility.TestPlanesAABB(planes, gameObject.GetComponent<Collider>().bounds))
				{
					list.Add(gameObject);
				}
			}
		}
		return list.ToArray();
	}

	// Token: 0x06008EE9 RID: 36585 RVA: 0x003B713C File Offset: 0x003B553C
	private bool IsLook(Vector3 pos)
	{
		Vector3 vector = base.GetComponent<Camera>().WorldToViewportPoint(pos);
		return vector.x < -0.5f || vector.x > 1.5f || vector.y < -0.5f || vector.y > 1.5f;
	}
}
