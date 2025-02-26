using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000A58 RID: 2648
public static class Utils
{
	// Token: 0x06004E99 RID: 20121 RVA: 0x001E26B4 File Offset: 0x001E0AB4
	public static void DestroyChildren(Transform parent)
	{
		int childCount = parent.childCount;
		GameObject[] array = new GameObject[childCount];
		for (int i = 0; i < childCount; i++)
		{
			array[i] = parent.GetChild(i).gameObject;
		}
		parent.DetachChildren();
		for (int j = 0; j < childCount; j++)
		{
			UnityEngine.Object.Destroy(array[j]);
		}
	}

	// Token: 0x06004E9A RID: 20122 RVA: 0x001E2710 File Offset: 0x001E0B10
	public static T StringToEnumType<T>(string value, T defaultValue)
	{
		T result;
		try
		{
			if (string.IsNullOrEmpty(value))
			{
				result = defaultValue;
			}
			else
			{
				result = (T)((object)Enum.Parse(typeof(T), value));
			}
		}
		catch (ArgumentException ex)
		{
			throw new UnityException(string.Concat(new string[]
			{
				ex.Message,
				Environment.NewLine,
				"failed to parse string [",
				value,
				"] -> enum type [",
				typeof(T).ToString(),
				"]"
			}));
		}
		return result;
	}

	// Token: 0x06004E9B RID: 20123 RVA: 0x001E27AC File Offset: 0x001E0BAC
	public static List<T> GetComponentsRecursive<T>(Transform t) where T : Component
	{
		List<T> list = new List<T>();
		T component = t.GetComponent<T>();
		if (component != null)
		{
			list.Add(component);
		}
		int i = 0;
		int childCount = t.childCount;
		while (i < childCount)
		{
			list.AddRange(Utils.GetComponentsRecursive<T>(t.GetChild(i)));
			i++;
		}
		return list;
	}

	// Token: 0x06004E9C RID: 20124 RVA: 0x001E280C File Offset: 0x001E0C0C
	public static T FindComponentInParents<T>(Transform t) where T : Component
	{
		T component = t.GetComponent<T>();
		if (component != null)
		{
			return component;
		}
		if (t.parent != null)
		{
			return Utils.FindComponentInParents<T>(t.parent);
		}
		return (T)((object)null);
	}

	// Token: 0x06004E9D RID: 20125 RVA: 0x001E2858 File Offset: 0x001E0C58
	public static void ConvertMeshIntoWireFrame(Mesh mesh)
	{
		MeshTopology topology = mesh.GetTopology(0);
		if (topology != MeshTopology.Triangles)
		{
			return;
		}
		int[] indices = mesh.GetIndices(0);
		int[] array = new int[indices.Length * 2];
		for (int i = 0; i < indices.Length / 3; i++)
		{
			int num = indices[i * 3];
			int num2 = indices[i * 3 + 1];
			int num3 = indices[i * 3 + 2];
			array[i * 6] = num;
			array[i * 6 + 1] = num2;
			array[i * 6 + 2] = num2;
			array[i * 6 + 3] = num3;
			array[i * 6 + 4] = num3;
			array[i * 6 + 5] = num;
		}
		mesh.SetIndices(array, MeshTopology.Lines, 0);
	}
}
