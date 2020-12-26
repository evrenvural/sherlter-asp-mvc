using Shelter.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shelter.Models.ViewModel
{
    public class DogDetailViewModel
    {
        public Dog Dog { get; set; }
        public bool WasRequested { get; set; }
    }
}
