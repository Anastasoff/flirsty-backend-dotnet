using System.ComponentModel.DataAnnotations;

namespace Flirsty.WebApi.Models
{
    public class GoogleRequestModel
    {
        [Required]
        public string ClientId { get; set; }

        [Required]
        public string Code { get; set; }
    }
}