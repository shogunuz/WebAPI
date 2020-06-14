using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    public class WorkerDetail
    {
        protected int _id;
        protected string _fio;
        protected string _position;


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
