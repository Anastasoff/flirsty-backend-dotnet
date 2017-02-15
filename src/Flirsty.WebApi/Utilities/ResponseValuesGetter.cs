using Flirsty.Domain.Entities.Enums;
using Newtonsoft.Json.Linq;
using System;

namespace Flirsty.WebApi.Utilities
{
    public interface IResponseValuesGetter
    {
        string TryGetValue(JObject user, string propertyName);

        Gender GetGender(string genderStr);
    }

    public class ResponseValuesGetter : IResponseValuesGetter
    {
        public string TryGetValue(JObject user, string propertyName)
        {
            JToken value;
            return user.TryGetValue(propertyName, out value) ? value.ToString() : null;
        }

        public Gender GetGender(string genderStr)
        {
            Gender gender;
            switch (genderStr)
            {
                case "male":
                    gender = Gender.Male;
                    break;

                case "female":
                    gender = Gender.Female;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(genderStr), "Not supported gender");
            }

            return gender;
        }
    }
}