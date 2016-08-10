using System.Collections.Generic;
using Akka.Configuration;
using Newtonsoft.Json.Linq;

namespace HocEye.Core
{
    public class HoconRetriever
    {
        private readonly IPathConstructor _pathConstructor;

        private Config _rootElement;

        public HoconRetriever(string hocon, IPathConstructor pathConstructor)
        {
            _pathConstructor = pathConstructor;

            _rootElement = ConfigurationFactory.ParseString(hocon);
        }

        public string GetHoconObject(IEnumerator<string> enumerator, int position)
        {
            var path = _pathConstructor.ConstructPathBackwards(enumerator, position);

            var value =  _rootElement.GetValue(path);

            return $"{path} : {value}";


        }
    }
}