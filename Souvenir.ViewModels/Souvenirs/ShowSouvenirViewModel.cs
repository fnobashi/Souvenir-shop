using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace Souvenir.ViewModels.Souvenirs
{
    public class ShowSouvenirViewModel
    {
        public int SouvenirId { get; set; }
        public string UserId { get; set; }
        public long OrderID { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public long Price { get; set; }
        public string MainImage { get; set; }
        public List<SouvenirOtherImagesDto> OtherImages { get; set; }
        public bool  IsAddToCartResult { get; set; }
        public int Inventory  { get; set; }

    }

}
