using Evaluation_Manager.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evaluation_Manager.Models
{
    public class Teacher : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }

        internal bool CheckPassword(string password)
        {
            return Password == password;
        }

        internal void PerformEvaluation(Student selectedStudent, Activity activity, int points)
        {
            var evaluation = EvaluationRepository.GetEvaluation(selectedStudent, activity);
            if (evaluation == null)
            {
                EvaluationRepository.InsertEvaluation(selectedStudent, activity, this, points);
            }
            else
            {
                EvaluationRepository.UpdateEvaluation(evaluation, this, points);
            }
        }
    }
}
