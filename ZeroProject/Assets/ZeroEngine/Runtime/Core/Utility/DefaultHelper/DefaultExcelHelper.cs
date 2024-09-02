/****************************************************
  文件：DefaultExcelHelper.cs
  作者：聪头
  邮箱：1322080797@qq.com
  日期：2024年08月31日 17:24:14
  功能：
*****************************************************/

using System.Collections.Generic;
using System.IO;
using OfficeOpenXml;

namespace ZeroEngine
{
    /// <summary>
    /// 默认 Excel文件 辅助器。表格索引从1开始！！！
    /// </summary>
    public class DefaultExcelPackageHelper : Utility.Excel.IExcelPackageHelper
    {
        private ExcelPackage _excelPackage;
        private List<Utility.Excel.IExcelSheetHelper> _workBook;

        public DefaultExcelPackageHelper()
        {
            _workBook = new List<Utility.Excel.IExcelSheetHelper>();
        }

        public int SheetCount => _workBook.Count - 1;
        
        public Utility.Excel.IExcelPackageHelper Load(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                Log.Error($"Excel文件加载失败，找不到文件: {path}");
                return null;
            }
            FileInfo fileInfo = new FileInfo(path);
            _excelPackage = new ExcelPackage(fileInfo);
            
            _workBook.Add(null); //sheetIdx从1开始索引，0占位
            for (int i = 1; i <= _excelPackage.Workbook.Worksheets.Count; i++)
            {
                ExcelWorksheet sheet = _excelPackage.Workbook.Worksheets[i];
                DefaultExcelSheetHelper sheetHelper = new DefaultExcelSheetHelper(this, sheet);
                _workBook.Add(sheetHelper);
            }
            return this;
        }
        
        public bool Save()
        {
            _excelPackage.Save();
            _workBook.Clear();
            _excelPackage = null;
            return true;
        }

        public bool DontSave()
        {
            _workBook.Clear();
            _excelPackage = null;
            return true;
        }

        public Utility.Excel.IExcelSheetHelper LoadAndOpen(string path, int sheetIdx)
        {
            return Load(path).OpenExcelSheet(sheetIdx);
        }

        public Utility.Excel.IExcelSheetHelper LoadAndOpen(string path, string sheetName)
        {
            return Load(path).OpenExcelSheet(sheetName);
        }

        public Utility.Excel.IExcelSheetHelper OpenExcelSheet(int sheetIdx)
        {
            if (sheetIdx < 1 || sheetIdx > SheetCount)
            {
                Log.Error($"OpenExcelSheet Failed! Out of Array Index: {sheetIdx}!");
                return null;
            }
            return _workBook[sheetIdx];
        }

        public Utility.Excel.IExcelSheetHelper OpenExcelSheet(string sheetName)
        {
            for (int i = 1; i <= _workBook.Count; i++)
            {
                if (_workBook[i].SheetName.Equals(sheetName))
                {
                    return _workBook[i];
                }
            }
            Log.Error($"OpenExcelSheet Failed! Not Found: {sheetName}");
            return null;
        }

        public Utility.Excel.IExcelSheetHelper AddSheet(string sheetName)
        {
            ExcelWorksheet sheet = _excelPackage.Workbook.Worksheets.Add(sheetName);
            DefaultExcelSheetHelper sheetHelper = new DefaultExcelSheetHelper(this, sheet);
            _workBook.Add(sheetHelper);
            return sheetHelper;
        }

        public bool RemoveSheet(int sheetIdx)
        {
            if (sheetIdx < 1 || sheetIdx > _workBook.Count)
            {
                Log.Error($"OpenExcelSheet Failed! Out of Array Index: {sheetIdx}!");
                return false;
            }
            _excelPackage.Workbook.Worksheets.Delete(sheetIdx);
            _workBook.RemoveAt(sheetIdx);
            return true;
        }

        public bool RemoveSheet(string sheetName)
        {
            for (int i = 1; i <= _workBook.Count; i++)
            {
                if (_workBook[i].SheetName.Equals(sheetName))
                {
                    return RemoveSheet(i);
                }
            }
            Log.Error($"OpenExcelSheet Failed! Not Found: {sheetName}");
            return false;
        }
    }

    public class DefaultExcelSheetHelper : Utility.Excel.IExcelSheetHelper
    {
        private readonly ExcelWorksheet _excelWorksheet;

        public DefaultExcelSheetHelper(Utility.Excel.IExcelPackageHelper excelPackage, ExcelWorksheet excelWorksheet)
        {
            ExcelPackage = excelPackage;
            _excelWorksheet = excelWorksheet;
        }

        public ExcelWorksheet ExcelWorksheet => _excelWorksheet;
        public int MaxRow => _excelWorksheet.Dimension.End.Row;
        public int MaxColumn => _excelWorksheet.Dimension.End.Column;
        public int SheetIdx => _excelWorksheet.Index;
        public string SheetName => _excelWorksheet.Name;
        public Utility.Excel.IExcelPackageHelper ExcelPackage { get; }

        public object ReadValue(int row, int column)
        {
            if (row > MaxRow || column > MaxColumn) return null;
            return _excelWorksheet.GetValue(row, column);
        }

        public T ReadValue<T>(int row, int column)
        {
            if (row > MaxRow || column > MaxColumn) return default(T);
            return _excelWorksheet.GetValue<T>(row, column);
        }

        public Utility.Excel.IExcelSheetHelper WriteValue(int row, int column, object val)
        {
            _excelWorksheet.SetValue(row, column, val);
            return this;
        }
    }
}