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
            SqlCommand comman = new SqlCommand("Insert into Products Values(@name,@unitPrice,@stockAmount)", _connection);

            comman.Parameters.AddWithValue("@name", product.Name);
            comman.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            comman.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            comman.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
            _connection.Close();
        }

        public void Update(Product product)
        {
            ConnetionControl();
            SqlCommand comman = new SqlCommand("UPDATE Products SET Name=@name, UnitPrice=@unitPrice, StockAmount=@stockAmount WHERE Id=@Id", _connection);

            comman.Parameters.AddWithValue("@name", product.Name);
            comman.Parameters.AddWithValue("@unitPrice", product.UnitPrice);
            comman.Parameters.AddWithValue("@stockAmount", product.StockAmount);
            comman.Parameters.AddWithValue("@Id", product.Id);
            comman.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
            _connection.Close();
        }

        public void Delete(int id)
        {
            ConnetionControl();
            SqlCommand comman = new SqlCommand("Delete From Products WHERE Id = @Id", _connection);

            comman.Parameters.AddWithValue("@Id", id);
            comman.ExecuteNonQuery(); //Çalıştırır ve etkilenen kayıt sayısını döndürür.
            _connection.Close();
        }

        public void ConnetionControl()
        {
            if (_connection.State == ConnectionState.Closed) //Önce kontrol ediyoruz. Eğer açıksa tekrar açmak sıkıntı yaratabilir.
            {
                _connection.Open();
            }
        }
    }
}
