using System.Numerics;
using Microsoft.Data.Sqlite;

namespace OrderBot
{
    public class Order : ISQLModel
    {
        private string _assist = String.Empty;
        private string _concern = String.Empty;
        private string _routies = String.Empty;
        private string _ingredient = String.Empty;
        private string _skintype = String.Empty;
        private string _allergies = String.Empty;
        private string _spf = String.Empty;
        private string _mask = String.Empty;
        private string _eyeconcern = String.Empty;


        public string Assist
        {
            get => _assist;
            set => _assist = value;
        }

        public string Concern
        {
            get => _concern;
            set => _concern = value;
        }

        public string Routins
        {
            get => _routies;
            set => _routies = value;
        }
        public string Ingredient
        {
            get => _ingredient;
            set => _ingredient = value;
        }
        public string SkinType
        {
            get => _skintype;
            set => _skintype = value;
        }
        public string SPF
        {
            get => _spf;
            set => _spf = value;
        }
        public string Mask
        {
            get => _mask;
            set => _mask = value;
        }
        public string EyeConcern
        {
            get => _eyeconcern;
            set => _eyeconcern = value;
        }

        public string Allergies
        {
            get => _allergies;
            set => _allergies = value;
        }




        public void Save()
        {
            using (var connection = new SqliteConnection(DB.GetConnectionString()))
            {
                connection.Open();

                var commandUpdate = connection.CreateCommand();
                commandUpdate.CommandText =
                @"
        UPDATE orders
        SET assist = $assist,
        concern = $concern,
        routies = $routies,
        ingredient = $ingredient,
        skintype = $skintype,
        allergies = $allergies,
        spf = $spf,
        mask = $mask,
        eyeconcern = $eyeconcern Where assist = $assist";
                commandUpdate.Parameters.AddWithValue("$assist", Assist);
                commandUpdate.Parameters.AddWithValue("$concern", Concern);
                commandUpdate.Parameters.AddWithValue("$routies", Routins);
                commandUpdate.Parameters.AddWithValue("$ingredient", Ingredient);
                commandUpdate.Parameters.AddWithValue("$skintype", SkinType);
                commandUpdate.Parameters.AddWithValue("$allergies", Allergies);
                commandUpdate.Parameters.AddWithValue("$spf", SPF);
                commandUpdate.Parameters.AddWithValue("$mask", Mask);
                commandUpdate.Parameters.AddWithValue("$eyeconcern", EyeConcern);

                int nRows = commandUpdate.ExecuteNonQuery();
                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(assist, concern,routies,ingredient,skintype,allergies,spf,mask,eyeconcern)
            VALUES($assist, $concern,$routies,$ingredient,$skintype,$allergies,$spf,$mask,$eyeconcern)
        ";
                    commandInsert.Parameters.AddWithValue("$assist", Assist);
                    commandInsert.Parameters.AddWithValue("$concern", Concern);
                    commandInsert.Parameters.AddWithValue("$routies", Routins);
                    commandInsert.Parameters.AddWithValue("$ingredient", Ingredient);
                    commandInsert.Parameters.AddWithValue("$skintype", SkinType);
                    commandInsert.Parameters.AddWithValue("$allergies", Allergies);
                    commandInsert.Parameters.AddWithValue("$spf", SPF);
                    commandInsert.Parameters.AddWithValue("$mask", Mask);
                    commandInsert.Parameters.AddWithValue("$eyeconcern", EyeConcern);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                    //}
                }

            }
        }
    }
}
