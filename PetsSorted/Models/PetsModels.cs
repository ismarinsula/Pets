using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace PetsSorted.Models
{
    public class PetOwner
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        public short Age { get; set; }
        public IList<Pet> Pets { get; set; }
    }

    public class Pet
    {
        public string Name{ get; set; }
        public string Type { get; set; }
    }

    public class PetOwnerGender
    {
        public string Gender { get; set; }
        public IList<Pet> Pets { get; set; }
    }

    
}