using System.ComponentModel.DataAnnotations;

namespace Lab4.Models
{
    public class Learner
    {
        public Learner()
        {
            Enrollments = new HashSet<Enrollment>();
        }
        public int LearnerID { get; set; }
      
        public string LastName { get; set; }
      
        public string FirstMidName { get; set; }
        
        public DateTime EnrollmentDate { get; set; }
        
        public int MajorID { get; set; }
        public virtual Major? Major { get; set; } //--------------- them co the co gia tri null
        public virtual ICollection<Enrollment> Enrollments { get; set; } //truy xuat khi can thiet , khong can lay tu dau
    }
}
