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
    public class EvaluationRepository
    {
        
            public static Evaluation GetEvaluation(Student student, Activity activity)
            {
                Evaluation evaluation = null;
                string sql = $"SELECT * FROM Evaluations" +
                    $" WHERE IdStudents = {student.Id} AND IdActivities = {activity.Id}";
                DB.OpenConnection();
                var reader = DB.GetDataReader(sql);
                if (reader.HasRows)
                {
                    reader.Read();
                    evaluation = CreateObject(reader);
                    reader.Close();
                }
                DB.CloseConnection();
                return evaluation;
            }

        public static void InsertEvaluation(Student student, Activity activity, Teacher teacher, int points)
        {
            string sql = $"INSERT INTO Evaluations (IdActivities, IdStudents, IdTeachers, EvaluationDate, Points) VALUES ({activity.Id}, {student.Id}, {teacher.Id}, GETDATE(), {points})";
            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        public static void UpdateEvaluation(Evaluation evaluation, Teacher teacher, int points)
        {
            string sql = $"UPDATE Evaluations SET IdTeachers = {teacher.Id}, Points = {points}, EvaluationDate = GetDate() WHERE IdActivities = {evaluation.Activity.Id} AND IdStudents = {evaluation.Student.Id}";
            DB.OpenConnection();
            DB.ExecuteCommand(sql);
            DB.CloseConnection();
        }

        private static Evaluation CreateObject(SqlDataReader dr)
            {
                return new Evaluation
                {
                    Activity = ActivityRepository.GetActivity(
                        int.Parse(dr["IdActivities"].ToString())
                    ),
                    Student = StudentRepository.GetStudent(
                        int.Parse(dr["IdStudents"].ToString())
                    ),
                    Evaluator = TeacherRepository.GetTeacher(
                        int.Parse(dr["IdTeachers"].ToString())
                    ),
                    EvaluationDateTime = DateTime.Parse(dr["EvaluationDate"].ToString()),
                    Points = int.Parse(dr["Points"].ToString())
                };
            }
        }
    }

