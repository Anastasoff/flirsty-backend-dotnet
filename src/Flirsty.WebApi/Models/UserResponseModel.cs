using Flirsty.Domain.Entities.Enums;
using Flirsty.Domain.Entities.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Flirsty.WebApi.Models
{
    public class UserResponseModel
    {
        public UserResponseModel()
        {
            FriendsList = new List<UserResponseModel>();
        }

        [Required]
        public string ServerId { get; set; }

        [Required]
        public string Email { get; set; }

        public PublicInfo PublicInfo { get; set; }

        public Location Location { get; set; }

        public DateTime? BirthDate { get; set; }

        public Gender Gender { get; set; }

        public Gender? LookingFor { get; set; }

        public int Seconds { get; set; }

        public ICollection<UserResponseModel> FriendsList { get; set; }
    }
}