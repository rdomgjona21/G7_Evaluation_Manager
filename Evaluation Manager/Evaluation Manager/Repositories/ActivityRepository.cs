using DBLayer;
using Evaluation_Manager.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_Manager.Repositories
{
    public class ActivityRepository
    {
        public class StudentRepository
        {
            public static Activity GetActivity(int id)
            {
                Activity activity = null;
                string sql = $"SELECT * FROM Activities WHERE Id = {id}";
                DB.OpenConnection();
                var reader = DB.GetDataReader(sql);
                if (reader.HasRows)
                {
                    reader.Read();
                    activity = CreateObject(reader);
                    reader.Close();
                }
                DB.CloseConnection();
                return activity;
            }

            public static List<Activity> GetActivities()
            {
                List<Activity> activities = new List<Activity>();
                string sql = "SELECT * FROM Students";
                DB.OpenConnection();
                var reader = DB.GetDataReader(sql);

                while (reader.Read())
                {
                    Activity activity = CreateObject(reader);
                    activities.Add(activity);
                }
                reader.Close();
                DB.CloseConnection();

                return activities;

            }

            private static Activity CreateObject(SqlDataReader reader)
            {
                int id = int.Parse(reader["Id"].ToString());
                string firstName = reader["FirstName"].ToString();
                string lastName = reader["LastName"].ToString();
                int.TryParse(reader["Grade"].ToString(), out int grade);

                var activity = new Activity
                {
                    Id = id,
                    FirstName = firstName,
                    LastName = lastName,
                    Grade = grade

                };
                return activity;
            }
        }
    }
}
