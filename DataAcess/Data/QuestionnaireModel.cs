using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcess.Data
{
    public class QuestionnaireModel
    {
        [Key]
        public int QuestionnaireiD { get; set; }
        [Required]
        public int QuestionOne { get; set; }
        [Required]
        public int QuestionTwo{ get; set; }
        [Required]
        public int QuestionThree { get; set; }
        [Required]
        public int QuestionFour { get; set; }
        [Required]
        public int QuestionFive { get; set; }
        [Required]
        public int QuestionSix { get; set; }
        public int Points { get; set; }
        [Required]
        public string Opinion { get; set; }
        public int StudentId { get; set; }
        public int KeeperId { get; set; }
        public virtual Keeper Keeper { get; set; }
        public virtual Student Student { get; set; }
    }
}
