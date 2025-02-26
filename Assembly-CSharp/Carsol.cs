using System;
using System.Collections.Generic;
using AIProject;
using Cinemachine;
using UnityEngine;

// Token: 0x02000ED0 RID: 3792
public class Carsol : MonoBehaviour
{
	// Token: 0x06007C12 RID: 31762 RVA: 0x00345120 File Offset: 0x00343520
	public void Init(Vector3 Pos, float gridsize)
	{
		this.Cam = CinemachineCore.Instance.GetActiveBrain(0).OutputCamera;
		this.carsolObjs = new CarsolObj[10];
		int i = 0;
		while (i < this.carsolObjs.Length)
		{
			this.carsolObjs[i].m_Obj = this.gameObjects[i];
			switch (i)
			{
			case 0:
				this.carsolObjs[i].m_CarsolAreaNumX = 1;
				this.carsolObjs[i].m_CarsolAreaNumZ = 1;
				break;
			case 1:
				this.carsolObjs[i].m_CarsolAreaNumX = 1;
				this.carsolObjs[i].m_CarsolAreaNumZ = 2;
				break;
			case 2:
				this.carsolObjs[i].m_CarsolAreaNumX = 2;
				this.carsolObjs[i].m_CarsolAreaNumZ = 1;
				break;
			case 3:
				this.carsolObjs[i].m_CarsolAreaNumX = 1;
				this.carsolObjs[i].m_CarsolAreaNumZ = 5;
				break;
			case 4:
				this.carsolObjs[i].m_CarsolAreaNumX = 2;
				this.carsolObjs[i].m_CarsolAreaNumZ = 2;
				break;
			case 5:
				this.carsolObjs[i].m_CarsolAreaNumX = 4;
				this.carsolObjs[i].m_CarsolAreaNumZ = 4;
				break;
			case 6:
			case 7:
			case 8:
				goto IL_195;
			case 9:
				this.carsolObjs[i].m_CarsolAreaNumX = 2;
				this.carsolObjs[i].m_CarsolAreaNumZ = 3;
				break;
			default:
				goto IL_195;
			}
			IL_1C0:
			i++;
			continue;
			IL_195:
			this.carsolObjs[i].m_CarsolAreaNumX = (this.carsolObjs[i].m_CarsolAreaNumZ = 0);
			goto IL_1C0;
		}
		this.fGridSize = gridsize;
		this.nDirection = 0;
		this.bRayFromMouseHit = false;
		this.MinPos = Pos;
		this.MinPos.x = this.MinPos.x - this.fGridSize / 2f;
		this.MinPos.y = this.MinPos.y + (this.fGridSize + 1f);
		this.MinPos.z = this.MinPos.z - this.fGridSize / 2f;
		this.nKind = 0;
		this.bMouseOperate = true;
		this.bMoveLimit = false;
		this.cursoldirInitPos = this.cursoldir.transform.localPosition;
	}

	// Token: 0x06007C13 RID: 31763 RVA: 0x003453A0 File Offset: 0x003437A0
	public void MoveCarsol(int nDirection = -1)
	{
		if (this.Vcam.isControlNow)
		{
			return;
		}
		Vector3 vector = base.transform.position;
		float num = this.fGridSize / 2f;
		float[] array = new float[2];
		if (nDirection < 0)
		{
			Vector3 mousePosition = Input.mousePosition;
			mousePosition.x = Mathf.Clamp(mousePosition.x, 0f, (float)Screen.width);
			mousePosition.y = Mathf.Clamp(mousePosition.y, 0f, (float)Screen.height);
			Ray ray = this.Cam.ScreenPointToRay(mousePosition);
			int num2 = Physics.RaycastNonAlloc(ray, this.HitInfo, 100f);
			for (int i = 0; i < num2; i++)
			{
				if (this.HitInfo[i].collider.gameObject.GetComponent<GridInfo>() != null)
				{
					this.HitGrid.Add(this.HitInfo[i]);
				}
			}
			this.HitGrid.Sort((RaycastHit a, RaycastHit b) => a.distance.CompareTo(b.distance));
			this.bRayFromMouseHit = (this.HitGrid.Count > 0);
			if (!this.bRayFromMouseHit)
			{
				return;
			}
			vector = this.HitGrid[0].point;
		}
		else
		{
			Physics.RaycastNonAlloc(base.transform.position + new Vector3(0f, 1f, 0f), Vector3.down, this.HitInfo, 100f);
			for (int j = 0; j < this.HitInfo.Length; j++)
			{
				if (this.HitInfo[j].collider.gameObject.GetComponent<GridInfo>() != null)
				{
					this.HitGrid.Add(this.HitInfo[j]);
				}
			}
			this.HitGrid.Sort((RaycastHit a, RaycastHit b) => a.distance.CompareTo(b.distance));
			vector = this.HitGrid[0].point;
			switch (nDirection)
			{
			case 0:
				if (!this.bMoveLimit)
				{
					vector.z += num;
				}
				else
				{
					vector.z += this.fGridSize;
				}
				break;
			case 1:
				if (!this.bMoveLimit)
				{
					vector.x += num;
				}
				else
				{
					vector.x += this.fGridSize;
				}
				break;
			case 2:
				if (!this.bMoveLimit)
				{
					vector.z -= num;
				}
				else
				{
					vector.z -= this.fGridSize;
				}
				break;
			case 3:
				if (!this.bMoveLimit)
				{
					vector.x -= num;
				}
				else
				{
					vector.x -= this.fGridSize;
				}
				break;
			}
		}
		Vector3 vector2;
		if (!this.bMoveLimit)
		{
			array[0] = (array[1] = num);
			vector2 = vector - this.MinPos;
			vector2.x = array[0] * (float)Mathf.RoundToInt(vector2.x / array[0]);
			vector2.z = array[1] * (float)Mathf.RoundToInt(vector2.z / array[1]);
			vector2.x += this.MinPos.x;
			vector2.z += this.MinPos.z;
		}
		else
		{
			if (this.nDirection == 0 || this.nDirection == 4)
			{
				array[0] = num * (float)this.carsolObjs[this.nKind].m_CarsolAreaNumX;
				array[1] = num * (float)this.carsolObjs[this.nKind].m_CarsolAreaNumZ;
			}
			else if (this.nDirection == 2 || this.nDirection == 6)
			{
				array[0] = num * (float)this.carsolObjs[this.nKind].m_CarsolAreaNumZ;
				array[1] = num * (float)this.carsolObjs[this.nKind].m_CarsolAreaNumX;
			}
			vector2 = vector - (this.MinPos + new Vector3(array[0], 0f, array[1]));
			vector2.x = this.fGridSize * (float)Mathf.RoundToInt(vector2.x / this.fGridSize);
			vector2.z = this.fGridSize * (float)Mathf.RoundToInt(vector2.z / this.fGridSize);
			vector2.x += this.MinPos.x + array[0];
			vector2.z += this.MinPos.z + array[1];
		}
		GridInfo component = this.HitGrid[0].collider.GetComponent<GridInfo>();
		int nTargetFloorCnt = Singleton<CraftCommandListBaseObject>.Instance.nTargetFloorCnt;
		int num3 = 0;
		int[] stackWallOnSmallGrid = component.GetStackWallOnSmallGrid(0, nTargetFloorCnt);
		for (int k = 0; k < stackWallOnSmallGrid.Length; k++)
		{
			if (stackWallOnSmallGrid[k] != -1)
			{
				num3++;
			}
		}
		vector.x = vector2.x;
		vector.z = vector2.z;
		this.HitGrid.Clear();
		if (!Physics.Raycast(vector + new Vector3(0f, 1f, 0f), Vector3.down, 100f))
		{
			return;
		}
		base.transform.position = vector;
	}

	// Token: 0x06007C14 RID: 31764 RVA: 0x00345983 File Offset: 0x00343D83
	public void SetDirection(int dir)
	{
		this.nDirection = dir;
	}

	// Token: 0x06007C15 RID: 31765 RVA: 0x0034598C File Offset: 0x00343D8C
	public int GetDirection()
	{
		return this.nDirection;
	}

	// Token: 0x06007C16 RID: 31766 RVA: 0x00345994 File Offset: 0x00343D94
	public List<RaycastHit[]> CheckCarsol(Quaternion CarsolRot, Quaternion rotation, int kind, ref BoxRange range, Vector3? pos = null)
	{
		Quaternion rotation2 = rotation * Quaternion.Inverse(CarsolRot);
		List<RaycastHit[]> list = new List<RaycastHit[]>();
		switch (kind)
		{
		case 6:
		{
			Vector3[] array = new Vector3[2];
			array[0].x = this.fGridSize;
			array[0].y = this.fGridSize;
			array[0].z = this.fGridSize * 2f;
			array[1].x = this.fGridSize;
			array[1].y = this.fGridSize;
			array[1].z = this.fGridSize;
			for (int i = 0; i < 2; i++)
			{
				array[i] /= 2f;
				array[i] *= 0.9f;
			}
			Transform[] componentsInChildren = this.carsolObjs[kind].m_Obj.GetComponentsInChildren<Transform>();
			Vector3[] array2 = new Vector3[2];
			if (pos != null)
			{
				for (int j = 0; j < componentsInChildren.Length; j++)
				{
					componentsInChildren[j].position += pos.Value - base.gameObject.transform.position;
				}
			}
			array2[0] = componentsInChildren[1].position + (componentsInChildren[2].position - componentsInChildren[1].position) / 2f;
			array2[1] = componentsInChildren[3].position;
			for (int k = 0; k < 2; k++)
			{
				array2[k] -= this.carsolObjs[kind].m_Obj.transform.position;
			}
			for (int l = 0; l < 2; l++)
			{
				array2[l] = rotation2 * array2[l];
				array2[l] += this.carsolObjs[kind].m_Obj.transform.position;
				list.Add(Physics.BoxCastAll(array2[l], array[l], Vector3.down, rotation, 100f));
			}
			BoxRange[] array3 = new BoxRange[2];
			for (int m = 0; m < 2; m++)
			{
				Carsol.GetBoxCastRange(array2[m], array[m], rotation, ref array3[m]);
				if (m == 0)
				{
					range = array3[0];
				}
				range.MinX = Mathf.Min(range.MinX, array3[m].MinX);
				range.MinZ = Mathf.Min(range.MinZ, array3[m].MinZ);
				range.MaxX = Mathf.Max(range.MaxX, array3[m].MaxX);
				range.MaxZ = Mathf.Max(range.MaxZ, array3[m].MaxZ);
			}
			break;
		}
		case 7:
		{
			Vector3[] array4 = new Vector3[3];
			array4[0].x = this.fGridSize;
			array4[0].y = this.fGridSize;
			array4[0].z = this.fGridSize * 2f;
			array4[1].x = this.fGridSize;
			array4[1].y = this.fGridSize;
			array4[1].z = this.fGridSize;
			array4[2] = array4[0];
			for (int n = 0; n < 3; n++)
			{
				array4[n] /= 2f;
				array4[n] *= 0.9f;
			}
			Transform[] componentsInChildren2 = this.carsolObjs[kind].m_Obj.GetComponentsInChildren<Transform>();
			Vector3[] array5 = new Vector3[3];
			if (pos != null)
			{
				for (int num = 0; num < componentsInChildren2.Length; num++)
				{
					componentsInChildren2[num].position += pos.Value - base.gameObject.transform.position;
				}
			}
			array5[0] = componentsInChildren2[1].position + (componentsInChildren2[2].position - componentsInChildren2[1].position) / 2f;
			array5[1] = componentsInChildren2[3].position;
			array5[2] = componentsInChildren2[4].position + (componentsInChildren2[5].position - componentsInChildren2[4].position) / 2f;
			for (int num2 = 0; num2 < 3; num2++)
			{
				array5[num2] -= this.carsolObjs[kind].m_Obj.transform.position;
			}
			for (int num3 = 0; num3 < 3; num3++)
			{
				array5[num3] = rotation2 * array5[num3];
				array5[num3] += this.carsolObjs[kind].m_Obj.transform.position;
				list.Add(Physics.BoxCastAll(array5[num3], array4[num3], Vector3.down, rotation, 100f));
			}
			BoxRange[] array3 = new BoxRange[3];
			for (int num4 = 0; num4 < 3; num4++)
			{
				Carsol.GetBoxCastRange(array5[num4], array4[num4], rotation, ref array3[num4]);
				if (num4 == 0)
				{
					range = array3[0];
				}
				range.MinX = Mathf.Min(range.MinX, array3[num4].MinX);
				range.MinZ = Mathf.Min(range.MinZ, array3[num4].MinZ);
				range.MaxX = Mathf.Max(range.MaxX, array3[num4].MaxX);
				range.MaxZ = Mathf.Max(range.MaxZ, array3[num4].MaxZ);
			}
			break;
		}
		case 8:
		{
			Vector3[] array6 = new Vector3[3];
			array6[0].x = this.fGridSize;
			array6[0].y = this.fGridSize;
			array6[0].z = this.fGridSize * 2f;
			array6[1] = array6[0];
			array6[2] = array6[1];
			for (int num5 = 0; num5 < 3; num5++)
			{
				array6[num5] /= 2f;
				array6[num5] *= 0.9f;
			}
			Transform[] componentsInChildren3 = this.carsolObjs[kind].m_Obj.GetComponentsInChildren<Transform>();
			Vector3[] array7 = new Vector3[3];
			if (pos != null)
			{
				for (int num6 = 0; num6 < componentsInChildren3.Length; num6++)
				{
					componentsInChildren3[num6].position += pos.Value - base.gameObject.transform.position;
				}
			}
			array7[0] = componentsInChildren3[1].position + (componentsInChildren3[2].position - componentsInChildren3[1].position) / 2f;
			array7[1] = componentsInChildren3[3].position + (componentsInChildren3[4].position - componentsInChildren3[3].position) / 2f;
			array7[2] = componentsInChildren3[5].position + (componentsInChildren3[6].position - componentsInChildren3[5].position) / 2f;
			for (int num7 = 0; num7 < 3; num7++)
			{
				array7[num7] -= this.carsolObjs[kind].m_Obj.transform.position;
			}
			for (int num8 = 0; num8 < 3; num8++)
			{
				array7[num8] = rotation2 * array7[num8];
				array7[num8] += this.carsolObjs[kind].m_Obj.transform.position;
				list.Add(Physics.BoxCastAll(array7[num8], array6[num8], Vector3.down, rotation, 100f));
			}
			BoxRange[] array3 = new BoxRange[3];
			for (int num9 = 0; num9 < 3; num9++)
			{
				Carsol.GetBoxCastRange(array7[num9], array6[num9], rotation, ref array3[num9]);
				if (num9 == 0)
				{
					range = array3[0];
				}
				range.MinX = Mathf.Min(range.MinX, array3[num9].MinX);
				range.MinZ = Mathf.Min(range.MinZ, array3[num9].MinZ);
				range.MaxX = Mathf.Max(range.MaxX, array3[num9].MaxX);
				range.MaxZ = Mathf.Max(range.MaxZ, array3[num9].MaxZ);
			}
			break;
		}
		default:
		{
			Vector3 vector;
			vector.x = this.fGridSize * (float)this.carsolObjs[kind].m_CarsolAreaNumX;
			vector.y = this.fGridSize;
			vector.z = this.fGridSize * (float)this.carsolObjs[kind].m_CarsolAreaNumZ;
			vector /= 2f;
			vector *= 0.9f;
			Vector3 vector2 = this.carsolObjs[kind].m_Obj.transform.position;
			if (pos != null)
			{
				vector2 += pos.Value - base.gameObject.transform.position;
			}
			list.Add(Physics.BoxCastAll(vector2, vector, Vector3.down, rotation, 100f));
			Carsol.GetBoxCastRange(vector2, vector, rotation, ref range);
			break;
		}
		}
		return list;
	}

	// Token: 0x06007C17 RID: 31767 RVA: 0x003465A8 File Offset: 0x003449A8
	private static void GetBoxCastRange(Vector3 Pos, Vector3 RaySize, Quaternion rotation, ref BoxRange range)
	{
		Vector3[] array = new Vector3[4];
		array[0].x = Pos.x - RaySize.x;
		array[0].y = Pos.y - RaySize.y;
		array[0].z = Pos.z - RaySize.z;
		array[1].x = Pos.x + RaySize.x;
		array[1].y = Pos.y - RaySize.y;
		array[1].z = Pos.z - RaySize.z;
		array[2].x = Pos.x - RaySize.x;
		array[2].y = Pos.y - RaySize.y;
		array[2].z = Pos.z + RaySize.z;
		array[3].x = Pos.x + RaySize.x;
		array[3].y = Pos.y - RaySize.y;
		array[3].z = Pos.z + RaySize.z;
		for (int i = 0; i < array.Length; i++)
		{
			array[i] -= Pos;
			array[i] = rotation * array[i];
			array[i] += Pos;
		}
		range.MinX = Mathf.Min(array[0].x, Mathf.Min(array[1].x, Mathf.Min(array[2].x, array[3].x)));
		range.MinZ = Mathf.Min(array[0].z, Mathf.Min(array[1].z, Mathf.Min(array[2].z, array[3].z)));
		range.MaxX = Mathf.Max(array[0].x, Mathf.Max(array[1].x, Mathf.Max(array[2].x, array[3].x)));
		range.MaxZ = Mathf.Max(array[0].z, Mathf.Max(array[1].z, Mathf.Max(array[2].z, array[3].z)));
	}

	// Token: 0x06007C18 RID: 31768 RVA: 0x00346878 File Offset: 0x00344C78
	public void SetMoveLimit(float itemId)
	{
		base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		this.nDirection = 0;
		if (itemId == 1f)
		{
			this.bMoveLimit = true;
		}
		else
		{
			this.bMoveLimit = false;
		}
	}

	// Token: 0x06007C19 RID: 31769 RVA: 0x003468CC File Offset: 0x00344CCC
	public void ChangeCarsol(int partsform)
	{
		this.nKind = partsform;
		base.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
		for (int i = 0; i < this.carsolObjs.Length; i++)
		{
			if (i == this.nKind)
			{
				this.carsolObjs[i].m_Obj.SetActive(true);
			}
			else
			{
				this.carsolObjs[i].m_Obj.SetActive(false);
			}
		}
	}

	// Token: 0x06007C1A RID: 31770 RVA: 0x00346957 File Offset: 0x00344D57
	public void ResetCursolDirPos()
	{
		this.cursoldir.transform.localPosition = this.cursoldirInitPos;
	}

	// Token: 0x06007C1B RID: 31771 RVA: 0x0034696F File Offset: 0x00344D6F
	public void SelectModeCarsolUnvisible()
	{
		this.carsolObjs[this.nKind].m_Obj.SetActive(this.carsolObjs[this.nKind].m_Obj.activeSelf ^ true);
	}

	// Token: 0x040063D0 RID: 25552
	public VirtualCameraController Vcam;

	// Token: 0x040063D1 RID: 25553
	public GameObject[] gameObjects;

	// Token: 0x040063D2 RID: 25554
	public bool bMouseOperate;

	// Token: 0x040063D3 RID: 25555
	public Canvas cursoldir;

	// Token: 0x040063D4 RID: 25556
	private Camera Cam;

	// Token: 0x040063D5 RID: 25557
	private float fGridSize;

	// Token: 0x040063D6 RID: 25558
	private Vector3 MinPos;

	// Token: 0x040063D7 RID: 25559
	private CarsolObj[] carsolObjs;

	// Token: 0x040063D8 RID: 25560
	private Vector3 cursoldirInitPos;

	// Token: 0x040063D9 RID: 25561
	private int nKind;

	// Token: 0x040063DA RID: 25562
	private int nDirection;

	// Token: 0x040063DB RID: 25563
	private bool bRayFromMouseHit;

	// Token: 0x040063DC RID: 25564
	private bool bMoveLimit;

	// Token: 0x040063DD RID: 25565
	private RaycastHit[] HitInfo = new RaycastHit[20];

	// Token: 0x040063DE RID: 25566
	private List<RaycastHit> HitGrid = new List<RaycastHit>();
}
