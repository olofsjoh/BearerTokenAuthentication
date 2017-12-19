using System.Collections.Generic;

namespace BearerTokenAuthentication.Model
{
    public class CarModel
    {
        public CarModel(IEnumerable<string> cars, Dictionary<string, string> token)
        {
            Cars = cars;
            Token = token;
        }

        public IEnumerable<string> Cars { get; private set; }
        public Dictionary<string, string> Token { get; private set; }
    }
}
