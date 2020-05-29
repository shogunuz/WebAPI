using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Models
{
    public class WorkerDetail
    {
        private int _id;
        private string _fio;
        private string _position;


        [Key]
        public int PMId 
        {
            get => _id; 
            set => _id = value; 
        }

        [Column(TypeName = "nvarchar(35)")]
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

      
       
    }
}
