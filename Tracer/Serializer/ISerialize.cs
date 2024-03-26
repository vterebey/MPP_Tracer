using System.Collections.Generic;

namespace TracerImplementation.Serializer
{
    public interface ISerialize
    {
        string SerializeResult(List<ThreadDetails> threadsResult);
       
    }
}
