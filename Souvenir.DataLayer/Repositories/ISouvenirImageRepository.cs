using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Souvenir.DataLayer
{
    public interface ISouvenirImageRepository
    {
        List<SouvenirImages> GetAllImages();
        IEnumerable<SouvenirImages> GetImageBySouvenirID(int souvenirID);  
        SouvenirImages GetImage(long ImgId);
        bool SaveImageFileName(SouvenirImages img);
        bool DeleteImageFileName(SouvenirImages img);
        bool UpdateImageFileName(SouvenirImages img);

    }
}
