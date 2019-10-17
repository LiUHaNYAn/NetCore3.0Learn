using System;
using System.Data;
using System.Data.Common;
using System.Reflection.Emit;
using NetCore3._0Learn.WebApp.Data.Repository;

namespace NetCore3._0Learn.WebApp.Data.Common
{
    public class DataConvert
    {
        public static DataTable ConvertDataReaderToDataTable(DbDataReader reader)
        {
            try
            {
                DataTable objDataTable = new DataTable();
                int intFieldCount = reader.FieldCount;
                for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
                {
                    objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
                }
                objDataTable.BeginLoadData();

                object[] objValues = new object[intFieldCount];
                while (reader.Read())
                {
                    reader.GetValues(objValues);
                    objDataTable.LoadDataRow(objValues, true);
                }
                reader.Close();
                objDataTable.EndLoadData();

                return objDataTable;
            }
            catch (Exception ex)
            {
                throw new Exception("Convert Error!", ex);
            }
        }

        public static DataSet ConvertDataReaderToDataSet(DbDataReader reader)
        {
            var dataset=new DataSet();
            try
            {
                var counter = 1;
                do 
                {
                    DataTable objDataTable = new DataTable();
                    objDataTable.TableName = "table" + counter;
                    int intFieldCount = reader.FieldCount;
                    for (int intCounter = 0; intCounter < intFieldCount; ++intCounter)
                    {
                        objDataTable.Columns.Add(reader.GetName(intCounter), reader.GetFieldType(intCounter));
                    }
                    objDataTable.BeginLoadData();

                    object[] objValues = new object[intFieldCount];
                    while (reader.Read())
                    {
                        reader.GetValues(objValues);
                        objDataTable.LoadDataRow(objValues, true);
                    }
                 
                    objDataTable.EndLoadData();
                    dataset.Tables.Add(objDataTable);
                    counter++;
                }while(reader.NextResult());
                reader.Close();
                return dataset;
            }
            catch (Exception ex)
            {
                throw new Exception("Convert Error!", ex);
            }
        }
    }
}