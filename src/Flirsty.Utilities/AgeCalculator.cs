using System;

namespace Flirsty.Utilities
{
    public static class AgeCalculator
    {
        public static int Calculate(DateTime today, DateTime? birthDate)
        {
            int age = 0;
            if (birthDate.HasValue)
            {
                age = today.Year - birthDate.Value.Year;
                if (birthDate > today.AddYears(-age))
                    age--;
            }

            return age;
        }
    }
}