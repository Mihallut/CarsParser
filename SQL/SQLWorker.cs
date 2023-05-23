using Microsoft.Data.SqlClient;
using Parser.Subjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CarsParser.SQL
{
    public class SQLWorker
    {
        string _connectionString;
        public SQLWorker(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task WriteDataToDBAsync(List<Model> models)
        {


            string connectionString = "Server=MIKAEL;Database=IlCats;User Id=sa;Password=04dNxnW2i2;TrustServerCertificate=True;";

            // Создание подключения
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                await connection.OpenAsync();
                using (SqlCommand command = connection.CreateCommand())
                {
                    foreach (Model model in models)
                    {
                        string manufacturer_id = "1";
                        InsertModels(command, model, manufacturer_id);

                    }
                }



            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    await connection.CloseAsync();
                }
            }
        }

        private static void InsertModels(SqlCommand command, Model model, string manufacturer_id)
        {
            foreach (Submodel submodel in model.Submodels)
            {
                if (submodel.ProductionEndDate != null)
                {
                    command.CommandText = "INSERT INTO [dbo].[Models]\r\n" +
                    "([manufacturer_id]\r\n" +
                    ",[id]\r\n" +
                    ",[name]\r\n" +
                    ",[production_start_date]\r\n" +
                    ",[production_end_date]\r\n" +
                    ",[model_code])\r\n" +
                    "VALUES\r\n" +
                    "(@manufacturer_id\r\n" +
                    ",@id\r\n" +
                    ",@name\r\n" +
                    ",@production_start_date\r\n" +
                    ",@production_end_date\r\n" +
                    ",@model_code)";


                    command.Parameters.AddWithValue("@production_end_date", submodel.ProductionEndDate.Value.Date);
                }
                else
                {
                    command.CommandText = "INSERT INTO [dbo].[Models]\r\n" +
                    "([manufacturer_id]\r\n" +
                    ",[id]\r\n" +
                    ",[name]\r\n" +
                    ",[production_start_date]\r\n" +
                    ",[model_code])\r\n" +
                    "VALUES\r\n" +
                    "(@manufacturer_id\r\n" +
                    ",@id\r\n" +
                    ",@name\r\n" +
                    ",@production_start_date\r\n" +
                    ",@model_code)";
                }


                command.Parameters.AddWithValue("@name", model.Name);
                command.Parameters.AddWithValue("@manufacturer_id", manufacturer_id);
                command.Parameters.AddWithValue("@id", submodel.ID);
                command.Parameters.AddWithValue("@production_start_date", submodel.ProductionStartDate.Date);

                command.Parameters.AddWithValue("@model_code", submodel.ModelCode);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                InsertComplectation(command, model, submodel);

            }
        }

        private static void InsertComplectation(SqlCommand command, Model model, Submodel submodel)
        {
            foreach (Complectation complectation in submodel.Complectations)
            {

                string insertFirstPart = "INSERT INTO [dbo].[Complectations]\r\n" +
                                      "([model_id]\r\n" +
                                      ",[production_start_date]\r\n";
                string insertSecondPart = "VALUES\r\n" +
                                       "(@model_id\r\n" +
                                       ",@id\r\n" +
                                       ",@production_start_date\r\n";

                command.Parameters.AddWithValue("@model_id", model.Name);
                command.Parameters.AddWithValue("@id", complectation.Name);
                command.Parameters.AddWithValue("@production_start_date", complectation.ProductionStartDate.Date);

                if (submodel.ProductionEndDate != null)
                {
                    insertFirstPart += ",[production_end_date]\r\n";
                    insertSecondPart += ",@production_end_date\r\n";

                    command.Parameters.AddWithValue("@production_end_date", complectation.ProductionEndDate.Value.Date);
                }

                Type objectType = complectation.GetType();
                PropertyInfo[] properties = objectType.GetProperties();

                foreach (PropertyInfo property in properties)
                {

                    object value = property.GetValue(complectation);
                    if (!property.Name.Contains("ProductionEndDate") ||
                        !property.Name.Contains("ProductionStartDate") ||
                        !property.Name.Contains("Name"))
                        if (value != null)
                        {
                            insertFirstPart += $",[{property.Name}]";
                            insertSecondPart += $",@{property.Name}";
                            command.Parameters.AddWithValue($"@{property.Name}", value);
                        }
                }
                insertSecondPart += ")";
                insertSecondPart += ")";
                command.CommandText = insertFirstPart + insertSecondPart;
                command.ExecuteNonQuery();
                command.Parameters.Clear();
            }
        }
    }
}
