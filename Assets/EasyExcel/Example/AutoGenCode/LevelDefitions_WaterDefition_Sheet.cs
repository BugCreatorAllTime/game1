﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by EasyExcel.
//     Runtime Version: 4.0
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using UnityEngine;
using EasyExcel;

namespace EasyExcelGenerated
{
	[Serializable]
	public class WaterDefition : EERowData
	{
		[EEKeyField]
		[SerializeField]
		private int _level;
		public int level { get { return _level; } }

		[SerializeField]
		private int _tubeCount;
		public int tubeCount { get { return _tubeCount; } }

		[SerializeField]
		private int _colorCount;
		public int colorCount { get { return _colorCount; } }

		[SerializeField]
		private int _emptyTubeCount;
		public int emptyTubeCount { get { return _emptyTubeCount; } }

		[SerializeField]
		private int _threeColorCount;
		public int threeColorCount { get { return _threeColorCount; } }

		[SerializeField]
		private int _twoColorCount;
		public int twoColorCount { get { return _twoColorCount; } }

		[SerializeField]
		private int _oneColorCount;
		public int oneColorCount { get { return _oneColorCount; } }


		public WaterDefition()
		{
		}

#if UNITY_EDITOR
		public WaterDefition(List<List<string>> sheet, int row, int column)
		{
			TryParse(sheet[row][column++], out _level);
			TryParse(sheet[row][column++], out _tubeCount);
			TryParse(sheet[row][column++], out _colorCount);
			TryParse(sheet[row][column++], out _emptyTubeCount);
			TryParse(sheet[row][column++], out _threeColorCount);
			TryParse(sheet[row][column++], out _twoColorCount);
			TryParse(sheet[row][column++], out _oneColorCount);
		}
#endif
		public override void OnAfterSerialized()
		{
		}
	}

	public class LevelDefitions_WaterDefition_Sheet : EERowDataCollection
	{
		[SerializeField]
		private List<WaterDefition> elements = new List<WaterDefition>();

		public override void AddData(EERowData data)
		{
			elements.Add(data as WaterDefition);
		}

		public override int GetDataCount()
		{
			return elements.Count;
		}

		public override EERowData GetData(int index)
		{
			return elements[index];
		}

		public override void OnAfterSerialized()
		{
			foreach (var element in elements)
				element.OnAfterSerialized();
		}
	}
}
