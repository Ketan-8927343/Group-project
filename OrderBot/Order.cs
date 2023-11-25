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
        SET size = $size
        WHERE phone = $phone
    ";
                //commandUpdate.Parameters.AddWithValue("$size", Size);
                //commandUpdate.Parameters.AddWithValue("$phone", Phone);
                int nRows = commandUpdate.ExecuteNonQuery();
                if (nRows == 0)
                {
                    var commandInsert = connection.CreateCommand();
                    commandInsert.CommandText =
                    @"
            INSERT INTO orders(size, phone)
            VALUES($size, $phone)
        ";
                    //commandInsert.Parameters.AddWithValue("$size", Size);
                    //commandInsert.Parameters.AddWithValue("$phone", Phone);
                    int nRowsInserted = commandInsert.ExecuteNonQuery();

                }
            }

        }
    }
}