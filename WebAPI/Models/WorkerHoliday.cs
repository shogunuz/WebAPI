using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class WorkerHoliday
    {
        private int _id;
        private int _idForHoliday;
        private string _fio;
        private string _position;
        public DateTime _dateStart;
        public DateTime _dateEnd;


        [Key]
        public int IdForH
        {
            get => _idForHoliday;
            set => _idForHoliday = value;
        }

        [Column(TypeName = "int")]
        [Required]
        public int PMId
        {
            get => _id;
            set => _id = value;
        }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string FIO 
        {
            get => _fio;
            set => _fio = value; 
        }

        [Column(TypeName ="nvarchar(20)")]
        [Required]
        public string Position 
        { 
            get => _position; 
            set => _position = value; 
        }


        [Column(TypeName = "nvarchar(150)")]
        [Required]
        public DateTime DateStart
        {
            get => _dateStart;
            set => _dateStart = value;
        }

        [Column(TypeName = "nvarchar(150)")]
        [Required]
        public DateTime DateEnd
        {
            get => _dateEnd;
            set => _dateEnd = value;
        }


    }
}
