using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class DataTransformer : EditorWindow
{
#if UNITY_EDITOR
    // [MenuItem("Tools/ParseExcel %#K")]
    [MenuItem("Tools/ParseExcel _F4")] // 추가 단축키: Control + K
    public static void ParseExcelDataToJson()
    {
        Debug.Log("Complete DataTransformer");
    }

    #region Helpers

    static void ParseExcelDataToJson<Loader, LoaderData>(string filename) where Loader : new() where LoaderData : new()
    {
        var loader = new Loader();
        var field = loader.GetType().GetFields()[0];
        field.SetValue(loader, ParseExcelDataToList<LoaderData>(filename));

        var jsonStr = JsonConvert.SerializeObject(loader, Formatting.Indented);
        File.WriteAllText($"{Application.dataPath}/@Resources/Data/JsonData/{filename}Data.json", jsonStr);
        AssetDatabase.Refresh();
    }

    static List<LoaderData> ParseExcelDataToList<LoaderData>(string filename) where LoaderData : new()
    {
        var loaderDatas = new List<LoaderData>();

        var lines = File.ReadAllText($"{Application.dataPath}/@Resources/Data/ExcelData/{filename}Data.csv").Trim()
            .Split("\n");

        var rows = new List<string[]>();

        var innerFieldCount = 0;
        for (var l = 1; l < lines.Length; l++)
        {
            var row = lines[l].Replace("\r", "").Split(',');
            rows.Add(row);
        }

        for (var r = 0; r < rows.Count; r++)
        {
            if (rows[r].Length == 0)
                continue;
            if (string.IsNullOrEmpty(rows[r][0]))
                continue;

            innerFieldCount = 0;
            //Dragon 파생클래스를 GetField하면 파생클래스 변수 -> 부모 변수로 되어 있음. 순서 변경
            var loaderData = new LoaderData();
            var loaderDataType = typeof(LoaderData);
            var bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            var fields = GetFieldsInBase(loaderDataType, bindingFlags);

            int nextIndex;
            for (nextIndex = r + 1; nextIndex < rows.Count; nextIndex++)
            {
                if (string.IsNullOrEmpty(rows[nextIndex][0]) == false)
                    break;
            }

            for (var f = 0; f < fields.Count; f++)
            {
                var field = loaderData.GetType().GetField(fields[f].Name);
                var type = field.FieldType;

                if (type.IsGenericType)
                {
                    var valueType = type.GetGenericArguments()[0];
                    var genericListType = typeof(List<>).MakeGenericType(valueType);
                    var genericList = Activator.CreateInstance(genericListType) as IList;

                    for (var i = r; i < nextIndex; i++)
                    {
                        if (string.IsNullOrEmpty(rows[i][f + innerFieldCount]))
                            continue;
                        Debug.Log($"filename = {filename} ,  {field} -> {rows[i][f]}");
                        {
                            var isCustomClass = valueType.IsClass && !valueType.IsPrimitive &&
                                                valueType != typeof(string);

                            if (isCustomClass)
                            {
                                var fieldInstance = Activator.CreateInstance(valueType);

                                var fieldType = fieldInstance.GetType();
                                var fieldInfos = fieldType.GetFields(BindingFlags.Public | BindingFlags.Instance);

                                for (var k = 0; k < fieldInfos.Length; k++)
                                {
                                    var innerField = valueType.GetFields()[k];
                                    var str = rows[i][f + innerFieldCount + k];
                                    var convertedValue = ConvertValue(str, innerField.FieldType);
                                    if (convertedValue != null)
                                    {
                                        innerField.SetValue(fieldInstance, convertedValue);
                                    }
                                }

                                string nextStr = null;
                                if (i + 1 < rows.Count)
                                {
                                    if (f + innerFieldCount < rows[i + 1].Length)
                                    {
                                        //DataId가 null이면 리스트가 아직 끝난게 아님
                                        if (string.IsNullOrEmpty(rows[i + 1][0]))
                                            nextStr = rows[i + 1][f + innerFieldCount];
                                    }
                                }
                                if (string.IsNullOrEmpty(nextStr))
                                {
                                    innerFieldCount = fieldInfos.Length - 1;
                                }
                                else if (i + 1 == nextIndex)
                                    innerFieldCount = fieldInfos.Length - 1;

                                genericList.Add(fieldInstance);

                                // field.SetValue(loaderData, fieldInstance);
                            }
                            else
                            {
                                var value = ConvertValue(rows[i][f], valueType);
                                genericList.Add(value);
                            }
                        }
                    }

                    if (genericList != null)
                    {
                        field.SetValue(loaderData, genericList);
                    }
                }
                else
                {
                    Debug.Log($"filename = {filename} ,  {field} -> {rows[r][f]}");
                    if (rows[r][f].Contains("780"))
                    {
                        Debug.Log($"filename = {filename} ,  {field} -> {rows[r][f]}");
                    }

                    var isCustomClass = field.FieldType.IsClass && !field.FieldType.IsPrimitive &&
                                        field.FieldType != typeof(string);
                    if (isCustomClass)
                    {
                        var fieldInstance = Activator.CreateInstance(field.FieldType);

                        var fieldType = fieldInstance.GetType();
                        var fieldInfos = fieldType.GetFields(BindingFlags.Public | BindingFlags.Instance);

                        for (var i = 0; i < fieldInfos.Length; i++)
                        {
                            var innerField = field.FieldType.GetFields()[i];
                            var value = rows[r][f + innerFieldCount + i];
                            var convertedValue = ConvertValue(value, innerField.FieldType);
                            if (convertedValue != null)
                            {
                                innerField.SetValue(fieldInstance, convertedValue);
                            }

                        }
                        innerFieldCount = fieldInfos.Length - 1;
                        field.SetValue(loaderData, fieldInstance);
                    }
                    else
                    {
                        //기타필드 처리
                        var value = ConvertValue(rows[r][f], field.FieldType);
                        if (value != null)
                        {
                            field.SetValue(loaderData, value);
                        }
                    }
                }
            }
            loaderDatas.Add(loaderData);
        }

        return loaderDatas;
    }

    static object ConvertValue(string value, Type type)
    {
        if (string.IsNullOrEmpty(value))
            return null;

        var converter = TypeDescriptor.GetConverter(type);
        return converter.ConvertFromString(value);
    }

    static object ConvertList(string value, Type type)
    {
        if (string.IsNullOrEmpty(value))
        {
            return null;
        }

        // Reflection
        var valueType = type.GetGenericArguments()[0];
        var genericListType = typeof(List<>).MakeGenericType(valueType);
        var genericList = Activator.CreateInstance(genericListType) as IList;

        // Parse Excel
        var list = value.Split('&').Select(x => ConvertValue(x, valueType)).ToList();

        foreach (var item in list)
            genericList.Add(item);

        return genericList;
    }

    static IList ParseCsvDataToList(string csvData, Type itemType)
    {
        var listType = typeof(List<>).MakeGenericType(itemType);
        var list = Activator.CreateInstance(listType) as IList;

        if (string.IsNullOrEmpty(csvData)) return list;

        var items = csvData.Split('\n');
        foreach (var item in items)
        {
            var obj = Activator.CreateInstance(itemType);
            var props = item.Split(',');

            var fields = itemType.GetFields(BindingFlags.Public | BindingFlags.Instance);
            for (var i = 0; i < fields.Length && i < props.Length; i++)
            {
                var field = fields[i];
                var value = Convert.ChangeType(props[i], field.FieldType);
                field.SetValue(obj, value);
            }

            list.Add(obj);
        }

        return list;
    }

    public static List<FieldInfo> GetFieldsInBase(Type type, BindingFlags bindingFlags)
    {
        var fields = new List<FieldInfo>();
        var fieldNames = new HashSet<string>(); //중복방지
        var stack = new Stack<Type>();

        while (type != null && type != typeof(object))
        {
            stack.Push(type);
            type = type.BaseType;
        }

        while (stack.Count > 0)
        {
            var currentType = stack.Pop();
            foreach (var field in currentType.GetFields(bindingFlags))
            {
                if (fieldNames.Add(field.Name))
                {
                    fields.Add(field);
                }
            }
        }

        return fields;
    }

    #endregion

#endif
}
