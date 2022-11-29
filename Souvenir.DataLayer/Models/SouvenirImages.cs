using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Souvenir.DataLayer
{
    public class SouvenirImages
    {
        [Key]
        public long ImageId { get; set; }
        public int SouvenirId { get; set; }
        public string ImageName { get; set; }


        // relations

        public Souvenirs Souvenir { get; set; }

        public SouvenirImages()
        {

        }

    }
}
