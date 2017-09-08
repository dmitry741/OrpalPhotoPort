using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace OrpalPhotoPort.WcfImageProcessing
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IOrpalImageProcessing
    {

        [OperationContract]
        string GetData(int value);       
    }

    //[DataContract]
    //class OrpalBitmap
    //{
    //    // TODO: 8th August
    //    int[,] m_image = null;
    //    int m_width, m_height;
    //}
}
