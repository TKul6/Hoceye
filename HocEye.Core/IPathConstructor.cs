using System.Collections.Generic;

namespace HocEye.Core
{
    public interface IPathConstructor
    {
        string ConstructPathBackwards(IEnumerator<string> lines, int position);
    }
}