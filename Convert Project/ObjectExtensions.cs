using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Convert_Project
{
    public static class ObjectExtensions
    {
        public static T Clone<T>(this object obj)
        {
            PropertyInfo[] props = obj.GetType().GetProperties();
            FieldInfo[] fields = obj.GetType().GetFields();
            
            T instance = Activator.CreateInstance<T>();

            foreach (PropertyInfo prop in props)
            {
                if (prop.SetMethod == null)
                    continue;
                
                object value = prop.GetValue(obj);
                prop.SetValue(instance, value);
            }

            foreach (FieldInfo field in fields)
            {
                object value = field.GetValue(obj);
                field.SetValue(instance, value);
            }

            return instance;
        }
        
        public static string ToTable<T>(this IEnumerable<T> items,
            string[] columnHeaders,
            params Func<T, object>[] selectors)
        {
            if (columnHeaders.Length != selectors.Length)
                throw new Exception("ToTable requires that column headers and selectors be the same length");

            var values = new string[items.Count() + 1, selectors.Length];
            
            // Create the headers
            for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
                values[0, colIndex] = columnHeaders[colIndex];
            
            // Fill the table with data
            for (int rowIndex = 1; rowIndex < values.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
                {
                    values[rowIndex, colIndex] = selectors[colIndex]
                        .Invoke(items.ElementAt(rowIndex-1))
                        .ToString();
                }
            }

            return ToStringTable(values);
        }

        static string ToStringTable(this string[,] values)
        {
            int[] maxColumnsWidth = GetMaxColumnWidth(values);
            var headerSplitter = new string('-', maxColumnsWidth
                .Sum(i => i + 3) - 1);

            var builder = new StringBuilder();

            for (int rowIndex = 0; rowIndex < values.GetLength(0); rowIndex++)
            {
                for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
                {
                    // Print Cell
                    string cell = values[rowIndex, colIndex];

                    cell = cell.PadRight(maxColumnsWidth[colIndex]);
                    builder.Append(" | ");
                    builder.Append(cell);
                }

                builder.Append(" | ");
                builder.AppendLine();

                if (rowIndex == 0)
                {
                    builder.AppendFormat(" |{0}| ", headerSplitter);
                    builder.AppendLine();
                }
            }

            return builder.ToString();
        }
        
        static int[] GetMaxColumnWidth(string[,] values)
        {
            var maxColumnWidth = new int[values.GetLength(1)];

            for (int colIndex = 0; colIndex < values.GetLength(1); colIndex++)
            {
                for (int rowIndex = 0; rowIndex < values.GetLength(0); rowIndex++)
                {
                    int newLength = values[rowIndex, colIndex].Length;
                    int oldLength = maxColumnWidth[colIndex];

                    if (newLength > oldLength)
                        maxColumnWidth[colIndex] = newLength;
                }
            }
            
            return maxColumnWidth;
        }
    }
}