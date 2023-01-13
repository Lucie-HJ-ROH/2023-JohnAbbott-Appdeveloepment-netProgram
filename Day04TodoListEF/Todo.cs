using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day04TodoListEF
{
    public class Todo
    {
        public Todo() { }

        public Todo( string task, int difficulty, DateTime dueDate, StatusEnum status)
        {
            
            Task = task;
            Difficulty = difficulty;
            DueDate = dueDate;
            Status = status;
            // Status = (StatusEnum)Enum.Parse(typeof(StatusEnum),status);
        }


        public int Id { get; set; }

        //TASK
        private string _task;

        [Required]
        [StringLength(100)]
        public string Task
        {
            get
            {
                return _task;
            }
            set
            {
                // Regex.IsMatch(task, @"^([a-zA-Z0-9./,;\-+()*!'""\s])+$")
                if (value.Length < 1 || value.Length > 100)
                {
                    throw new ArgumentException("Maximum task size exceeded: must be up to 100 characters long");
                }
                _task = value;
            }
        }

        private int _difficulty;
        [Required]
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                if (value > 5 || value < 1)
                {
                    throw new ArgumentException("Difficulty must fall in 1-5 range");
                }
                _difficulty = value;
            }
        }

        //DUE DATE

        private DateTime _dueDate;


        [Required]
        [DataType(DataType.Date)]
        // [DisplayFormat(ApplyFormatInEditMode = true)] //, DataFormatString = "{yyyy/MM/0:dd}")]
        public DateTime DueDate
        {
            get
            {
                return _dueDate;
            }
            set
            {
                if (value.Year < 1900 || value.Year > 2099)
                {
                    throw new ArgumentException("Invalid year. Must be between 1900-2099.");
                }
                _dueDate = value;
            }
        }

        //STATUS
        public enum StatusEnum
        {
            Pending = 0,
            Done = 1,
            Delegated = 2
        }

        [Required]
        [EnumDataType(typeof(StatusEnum))]
        public StatusEnum Status { get; set; }


    }
}
