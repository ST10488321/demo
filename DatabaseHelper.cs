using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace demo
{
    public class DatabaseHelper
    {

        private string connectionString = "Server=localhost;Database=cyberchat;Uid=root;Pwd=your_password;";
        private string currentUsername;

        public void SetUsername(string username)
        {
            currentUsername = username;
        }

        public void AddTask(string title, string description, DateTime? reminderDate)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = @"INSERT INTO tasks (username, title, description, reminder_date)
                               VALUES (@username, @title, @description, @reminder)";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUsername);
                    cmd.Parameters.AddWithValue("@title", title);
                    cmd.Parameters.AddWithValue("@description", (object)description ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@reminder", (object)reminderDate ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<TaskItem> GetTasks()
        {
            var tasks = new List<TaskItem>();
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "SELECT id, title, description, reminder_date, is_completed FROM tasks WHERE username = @username ORDER BY created_at DESC";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@username", currentUsername);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            tasks.Add(new TaskItem
                            {
                                Id = reader.GetInt32("id"),
                                Title = reader.GetString("title"),
                                Description = reader.IsDBNull(reader.GetOrdinal("description")) ? "" : reader.GetString("description"),
                                ReminderDate = reader.IsDBNull(reader.GetOrdinal("reminder_date")) ? (DateTime?)null : reader.GetDateTime("reminder_date"),
                                IsCompleted = reader.GetBoolean("is_completed")
                            });
                        }
                    }
                }
            }
            return tasks;
        }

        public void MarkTaskComplete(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "UPDATE tasks SET is_completed = TRUE WHERE id = @id AND username = @username";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@username", currentUsername);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteTask(int id)
        {
            using (var conn = new MySqlConnection(connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM tasks WHERE id = @id AND username = @username";
                using (var cmd = new MySqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@username", currentUsername);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }

    public class TaskItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? ReminderDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
    
