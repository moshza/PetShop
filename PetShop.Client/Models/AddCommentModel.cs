using PetShop.data.Models;
using System.ComponentModel.DataAnnotations;

namespace PetShop.Client.Models
{
    public class AddCommentModel
    {
       
        public Animal Animal { get; set; }
        public string Content { get; set; }
    }
}
