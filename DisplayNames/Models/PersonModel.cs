using System;

namespace DisplayNames.Models
{
    public class PersonModel
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }

        private string _website;
        public string Website
        {
            get 
            { 
                return _website;
            }
            set 
            { 
                if (value.StartsWith("http://"))
                {
                    _website = value;
                }
                else
                {
                    _website = $"http://{value}"; 
                }
            }
        }
        
    }
}