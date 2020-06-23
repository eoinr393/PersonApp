using PersonWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace PersonWebAPI.Controllers
{
    public class PersonController : ApiController
    {
        //private List<Person> personList = new List<Person>();

        // GET api/<controller>
        public IEnumerable<Person> Get()
        {
            List<Person> personList = new List<Person>();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlCommand = "select [id], [firstname], [lastname] from dbo.person";

            try
            {
                connection.Open();

                using (SqlDataReader reader = new SqlCommand(sqlCommand, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {                        
                        personList.Add(new Person() { id = reader.GetInt32(0), firstName = reader.GetString(1), lastName = reader.GetString(2) });                            
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                //Log Excpetion
            }

            return personList;
           
        }

        // GET api/<controller>/5
        public Person Get(int id)
        {
            Person person = new Person();

            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string sqlCommand = $"select [id], [firstname], [lastname] from dbo.person where id = {id}";

            try
            {
                connection.Open();

                using (SqlDataReader reader = new SqlCommand(sqlCommand, connection).ExecuteReader())
                {
                    while (reader.Read())
                    {
                        person.id = reader.GetInt32(0);
                        person.firstName = reader.GetString(1);
                        person.lastName = reader.GetString(2);
                    }
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                //Log Excpetion
            }

            return person;
        }

        // POST api/<controller>
        public void Post([FromBody]Person person)
        {
            if (person != null && !string.IsNullOrWhiteSpace(person.firstName) && !string.IsNullOrWhiteSpace(person.lastName))
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlCommand = $"insert into person ([firstname],[lastname]) VALUES ('{person.firstName}', '{person.lastName}')";

                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        int rows = command.ExecuteNonQuery();                        
                    };
                    
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //Log Excpetion
                }
            }
        }

        // PUT api/<controller>/5
        public void Put([FromBody]Person person)
        {
            if (person != null && person.id > 0 && !string.IsNullOrWhiteSpace(person.firstName) && !string.IsNullOrWhiteSpace(person.lastName)) 
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlCommand = $"update person set firstname = '{person.firstName}', lastname = '{person.lastName}' where id = {person.id}";

                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        int rows = command.ExecuteNonQuery();
                    };

                    connection.Close();
                }
                catch (Exception ex)
                {
                    //Log Excpetion
                }
            }            
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
            if(id > 0)
            {
                string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection connection = new SqlConnection(connectionString);

                string sqlCommand = $"delete from person where id = {id}";

                try
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(sqlCommand, connection))
                    {
                        int rows = command.ExecuteNonQuery();
                    };

                    command.Dispose();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    //Log Excpetion
                }
            }

        }
    }
}