using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBLayer;
using Evaluation_Manager.Models;

namespace Evaluation_Manager.Repositories
{
    public class StudentRepository
    {
        public static Activity GetStudent(int id)
        {
            Activity student = null;
            string sql = $"SELECT * FROM Students WHERE Id = {id}";
            DB.OpenConnection();
            var reader = DB.GetDataReader(sql);
            if (reader.HasRows)
            {
                reader.Read();
                student = CreateObject(reader);
                reader.Close();
            }
            DB.CloseConnection();
            return student;
        }

        public static List<Activity> GetStudents() 
        {   
            List<Activity> students = new List<Activity>();
            string sql = "SELECT * FROM Students";
            DB.OpenConnection();    
            var reader = DB.GetDataReader(sql); 

            while(reader.Read()) 
            {
                Activity student = CreateObject(reader);
                students.Add(student);
            }
            reader.Close();
            DB.CloseConnection() ;

            return students;
        
        }

        private static Activity CreateObject(SqlDataReader reader)
        {
            int id = int.Parse(reader["Id"].ToString());
            string firstName = reader["FirstName"].ToString();
            string lastName = reader["LastName"].ToString();
            int.TryParse(reader["Grade"].ToString(), out int grade);

            var student = new Activity
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Grade = grade

            };
            return student;
        }
    }
}
