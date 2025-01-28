using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration;
using Npgsql;
using WebApplication4.Models;


namespace YourProject.DAL
{
    public class UserRepository
    {
        private readonly string _connectionString;

        public UserRepository(IConfiguration configuration)
        {
            _connectionString =configuration.GetConnectionString("DefaultConnection");
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "SELECT * FROM public.\"User\"";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            users.Add(new User
                            {
                                Id = reader.GetInt32(reader.GetOrdinal("Id")),
                                Name = reader.GetString(reader.GetOrdinal("Name")),
                                Email = reader.GetString(reader.GetOrdinal("Email"))

                            });
                        }
                    }
                }
            }

            return users;
        }

        public void AddUser(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "INSERT INTO public.\"User\" (\"Id\",\"Name\", \"Email\") VALUES (@Id, @Name, @Email);";

                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.ExecuteNonQuery();
                }
            }
        }


        public void UpdateUser(User user)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "UPDATE public.\"User\" SET \"Name\" = @Name, \"Email\" = @Email WHERE \"Id\" = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@Name", user.Name);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteUser(int id)
        {
            using (var connection = new NpgsqlConnection(_connectionString))
            {
                connection.Open();
                var query = "DELETE FROM public.\"User\" WHERE \"Id\" = @Id";
                using (var command = new NpgsqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
