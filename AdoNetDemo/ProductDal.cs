using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNetDemo
{
    public class ProductDal //Açılımı Dal-> Data Accsess Layer
    {
        //_connetion'un başına _ koyduk blobal olduğunu ya da dışarda olduğunu belitmek için.
        SqlConnection _connection = new SqlConnection("Server=.;Database=eTrade;Trusted_Connection=True;");
        public void ConnetionControl()
        {
            if (_connection.State == ConnectionState.Closed) //Önce kontrol ediyoruz. Eğer açıksa tekrar açmak sıkıntı yaratabilir.
            {
                _connection.Open();
            }
        }

        public List<Product> GetAll()
        {
            ConnetionControl();
            SqlCommand command = new SqlCommand("Select * from Products", _connection);
            SqlDataReader reader = command.ExecuteReader();//SQL'de F5'e basmak ile aynı. Dönderdiği değer ise SqlDataReader'tipinde.
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToInt32(reader["UnitPrice"])
                };
                products.Add(product);
            }
            reader.Close();
            _connection.Close();
            return products;
        }
        public void Add(Product product)
        {
            ConnetionControl();
            SqlCommand command = new SqlCommand("Insert into Products Values(@name,@unitPrice,@stockAmount)", _connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
            _connection.Close();
        }
        public void Update(Product product)
        {
            ConnetionControl();
            SqlCommand command = new SqlCommand("UPDATE Products SET Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount WHERE Id=@Id", _connection);
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.Parameters.AddWithValue("@Id", product.Id);
            command.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
            _connection.Close();
        }
        public void Delete(int id)
        {
            ConnetionControl();
            SqlCommand command = new SqlCommand("Delete From Products WHERE Id = @Id", _connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
            _connection.Close();
        }



        public List<Product> GetAll_SP()
        {
            ConnetionControl();
            SqlCommand command = new SqlCommand("SP_ListProduct", _connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader = command.ExecuteReader();//SQL'de F5'e basmak ile aynı. Dönderdiği değer ise SqlDataReader'tipinde.
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product product = new Product()
                {
                    Id = Convert.ToInt32(reader["Id"]),
                    Name = reader["Name"].ToString(),
                    StockAmount = Convert.ToInt32(reader["StockAmount"]),
                    UnitPrice = Convert.ToInt32(reader["UnitPrice"])
                };
                products.Add(product);
            }
            reader.Close();
            _connection.Close();
            return products;

        } //Listeleme için örnek
        public void Add_SP(Product product) // Ekleme / Düzelme / Silme için örnek.
        {
            ConnetionControl();
            SqlCommand command = new SqlCommand("SP_AddProduct", _connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@name", product.Name);
            command.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            command.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            command.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
            _connection.Close();
        }
    }
}
